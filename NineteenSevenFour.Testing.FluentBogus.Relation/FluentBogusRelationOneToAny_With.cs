using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NineteenSevenFour.Testing.Core;

namespace NineteenSevenFour.Testing.FluentBogus.Relation;

public class FluentBogusRelationOneToAny<TSource, TDep, TKeyProp> : FluentBogusRelationOneToAny<TSource, TDep>, IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp>
  where TSource : class
  where TDep : class
{
  public FluentBogusRelationOneToAny(TSource source, TDep? dependency)
    : base(source, dependency)
  {
  }

  public FluentBogusRelationOneToAny(
    TSource source,
    TDep? dependency,
    Expression<Func<TSource, TKeyProp>>? sourceKeyExpression,
    Expression<Func<TSource, TKeyProp>>? sourceForeignKeyExpression)
    : this(source, dependency)
  {
    if (sourceKeyExpression != null)
    {
      FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(sourceKeyExpression));
      this.SourceKeyExpression = sourceKeyExpression;
    }

    if (sourceForeignKeyExpression != null)
    {
      FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(sourceForeignKeyExpression));
      this.SourceForeignKeyExpression = sourceForeignKeyExpression;
    }
  }

  /// <inheritdoc/>>
  public Expression<Func<TSource, TKeyProp>>? SourceKeyExpression { get; private set; }

  /// <inheritdoc/>>
  public Expression<Func<TSource, TKeyProp>>? SourceForeignKeyExpression { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> WithMany(Expression<Func<TDep, ICollection<TSource?>?>> expression) => new FluentBogusRelationOneToMany<TSource, TDep, TKeyProp>(this.Source, this.Dependency, this.SourceKeyExpression, this.SourceForeignKeyExpression, expression);

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression) => new FluentBogusRelationOneToOne<TSource, TDep, TKeyProp>(this.Source, this.Dependency, this.SourceKeyExpression, this.SourceForeignKeyExpression, expression);
}
