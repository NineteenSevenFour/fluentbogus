using NineteenSevenFour.Testing.Core.Extension;
using NineteenSevenFour.Testing.FluentBogus.Relation.Interface;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  public class FluentBogusRelationManyToOne<TSource, TDep, TKeyProp> : FluentBogusRelationManyToAny<TSource, TDep, TKeyProp>, IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp>
      where TSource : class
      where TDep : class
  {
    public FluentBogusRelationManyToOne(
        TSource source,
        ICollection<TDep>? dependency,
        Expression<Func<TSource, TKeyProp>>? sourceKeyExpression) : base(source, dependency, sourceKeyExpression)
    {
    }

    public FluentBogusRelationManyToOne(
        TSource source,
        ICollection<TDep>? dependency,
        Expression<Func<TSource, TKeyProp>>? sourceKeyExpression,
        Expression<Func<TDep, TSource?>>? withOneExpression) : this(source, dependency, sourceKeyExpression)
    {
      if (withOneExpression == null) throw new ArgumentNullException(nameof(withOneExpression));
      FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(withOneExpression));
      SourceRefExpression = withOneExpression;
    }

    /// <inheritdoc/>>
    public Expression<Func<TDep, TKeyProp>>? DependencyForeignKeyExpression { get; private set; }

    /// <inheritdoc/>>
    public Expression<Func<TDep, TSource?>>? SourceRefExpression { get; private set; }

    /// <inheritdoc/>>
    public IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithForeignKey(Expression<Func<TDep, TKeyProp>> expression)
    {
      if (expression == null) throw new ArgumentNullException(nameof(expression));
      FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
      DependencyForeignKeyExpression = expression;
      return this;
    }

    /// <inheritdoc/>>
    public void Apply()
    {
      if (SourceKeyExpression == null)
      {
        throw new InvalidOperationException("The Many to One relation is not setup properly. The Source Key must be defined using HasKey().");
      }
      if (DependencyForeignKeyExpression == null)
      {
        throw new InvalidOperationException("The Many to One relation is not setup properly. The dependency foreign key must be defined using WithForeignKey().");
      }

      if (Dependency != null && SourceRefExpression != null)
      {
        foreach (var item in Dependency)
        {
          if (item == null) continue;

          // Set Source reference on Dependency's item
          var sourceRef = SourceRefExpression.Compile().Invoke(item);
          if (sourceRef == null)
          {

            var sourceKey = SourceKeyExpression.Compile().Invoke(Source);
            var dependencyForeignKey = DependencyForeignKeyExpression.Compile().Invoke(item);

            // Set Dependency's item foreign key to Source key
            if (sourceKey != null)
            {
              FluentExpression.SetField(item, SourceRefExpression, Source);
              FluentExpression.SetField(item, DependencyForeignKeyExpression, sourceKey);
            }
            else
            {
              // TODO: Is this an actual use case ?
              throw new InvalidOperationException("The Many to One relation is not setup properly. Please review the relation definition as well as the entity definition.");
            }
          }
          else
          {
            // TODO: Is this an actual use case ?
            throw new ArgumentNullException(nameof(SourceKeyExpression), "The Source has already been defined.");
          }
        }
      }
    }
  }
}
