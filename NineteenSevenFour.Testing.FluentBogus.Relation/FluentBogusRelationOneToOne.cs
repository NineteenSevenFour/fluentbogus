using NineteenSevenFour.Testing.Core.Extension;
using NineteenSevenFour.Testing.FluentBogus.Relation.Interface;

using System;
using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  public class FluentBogusRelationOneToOne<TSource, TDep, TKeyProp> : FluentBogusRelationOneToAny<TSource, TDep, TKeyProp>, IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp>
  where TSource : class
  where TDep : class
  {
    public FluentBogusRelationOneToOne
        (TSource source,
        TDep? dependency,
        Expression<Func<TSource, TKeyProp>>? sourceKeyExpression,
        Expression<Func<TSource, TKeyProp>>? sourceForeignKeyExpression,
        Expression<Func<TDep, TSource?>> expression) : base(source, dependency, sourceKeyExpression, sourceForeignKeyExpression)
    {
      if (expression == null) throw new ArgumentNullException(nameof(expression));
      FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
      SourceRefExpression = expression;
    }

    /// <inheritdoc/>>
    public Expression<Func<TDep, TSource?>>? SourceRefExpression { get; private set; }

    /// <inheritdoc/>>
    public Expression<Func<TDep, TKeyProp>>? WithForeignKeyExpression { get; private set; }

    /// <inheritdoc/>>
    public Expression<Func<TDep, TKeyProp>>? WithKeyExpression { get; private set; }

    /// <inheritdoc/>>
    public IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithForeignKey(Expression<Func<TDep, TKeyProp>> expression)
    {
      if (expression == null) throw new ArgumentNullException(nameof(expression));
      FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
      WithForeignKeyExpression = expression;
      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithKey(Expression<Func<TDep, TKeyProp>> expression)
    {
      if (expression == null) throw new ArgumentNullException(nameof(expression));
      FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
      WithKeyExpression = expression;
      return this;
    }

    /// <inheritdoc/>>
    public void Apply()
    {
      if (Dependency != null && SourceRefExpression != null)
      {
        var sourceRef = SourceRefExpression.Compile().Invoke(Dependency);

        if (sourceRef == null)
        {
          if (SourceKeyExpression != null && WithForeignKeyExpression != null)
          {
            var sourceKey = SourceKeyExpression.Compile().Invoke(Source);
            var withForeignKey = WithForeignKeyExpression.Compile().Invoke(Dependency);

            if (sourceKey != null && withForeignKey != null)
            {
              FluentExpression.SetField(Dependency, SourceRefExpression, Source);
              FluentExpression.SetField(Dependency, WithForeignKeyExpression, sourceKey);
            }
          }
          else if (SourceForeignKeyExpression != null && WithKeyExpression != null)
          {
            var sourceForeignKey = SourceForeignKeyExpression.Compile().Invoke(Source);
            var withKey = WithKeyExpression.Compile().Invoke(Dependency);

            if (sourceForeignKey != null && withKey != null)
            {
              FluentExpression.SetField(Dependency, SourceRefExpression, Source);
              FluentExpression.SetField(Source, SourceForeignKeyExpression, withKey);
            }
          }
          else
          {
            if (SourceKeyExpression != null && WithForeignKeyExpression != null)
            {
              if (SourceKeyExpression != null)
              {
                throw new ArgumentNullException(nameof(SourceKeyExpression), "The Source Key must be defined using HasKey().");
              }
              if (WithForeignKeyExpression != null)
              {
                throw new ArgumentNullException(nameof(WithForeignKeyExpression), "The dependency key must be defined using WithForeignKey().");
              }
            }
            else if (SourceForeignKeyExpression != null && WithKeyExpression != null)
            {
              if (SourceForeignKeyExpression != null)
              {
                throw new ArgumentNullException(nameof(SourceForeignKeyExpression), "The Source Foreign Key must be defined using HasForeignKey().");
              }
              if (WithKeyExpression != null)
              {
                throw new ArgumentNullException(nameof(WithKeyExpression), "The dependency key must be defined using WithKey().");
              }
            }
            throw new InvalidOperationException("The relation configuration is incorrect. Check the use of HasKey(), HasForeignKey(), WithKey(), HasForeignKey()");
          }
        }
      }
    }
  }
}
