using NineteenSevenFour.Testing.Core.Extension;
using NineteenSevenFour.Testing.FluentBogus.Relation.Interface;

using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Relation;

public class FluentBogusRelationOneToMany<TSource, TDep, TKeyProp> : FluentBogusRelationOneToAny<TSource, TDep, TKeyProp>, IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp>
    where TSource : class
    where TDep : class
{
  public FluentBogusRelationOneToMany(
      TSource source,
      TDep? dependency,
      Expression<Func<TSource, TKeyProp?>>? sourceKeyExpression,
      Expression<Func<TSource, TKeyProp?>>? sourceForeignKeyExpression,
      Expression<Func<TDep, ICollection<TSource?>?>> sourceRefExpression) : base(source, dependency, sourceKeyExpression, sourceForeignKeyExpression)
  {
    if (sourceRefExpression == null) throw new ArgumentNullException(nameof(sourceRefExpression));
    FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(sourceRefExpression));
    SourceRefExpression = sourceRefExpression;
  }

  /// <inheritdoc/>>
  public Expression<Func<TDep, ICollection<TSource?>?>>? SourceRefExpression { get; private set; }

  /// <inheritdoc/>>
  public Expression<Func<TDep, TKeyProp?>>? WithKeyExpression { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> WithKey(Expression<Func<TDep, TKeyProp?>> expression)
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

      sourceRef ??= default;
      if (sourceRef?.Any() == false)
      {
        if (SourceForeignKeyExpression != null && WithKeyExpression != null)
        {
          var sourceForeignKey = SourceForeignKeyExpression.Compile().Invoke(Source);
          var withKey = WithKeyExpression.Compile().Invoke(Dependency);

          if (sourceForeignKey != null && withKey != null)
          {
            sourceRef.Add(Source);
            FluentExpression.SetField(Dependency, SourceRefExpression, sourceRef);
            FluentExpression.SetField(Source, SourceForeignKeyExpression, withKey);
          }
        }
        else
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
      }
    }
  }
}
