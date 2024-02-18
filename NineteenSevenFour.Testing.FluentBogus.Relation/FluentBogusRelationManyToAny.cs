using NineteenSevenFour.Testing.Core.Extension;
using NineteenSevenFour.Testing.FluentBogus.Relation.Interface;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  public class FluentBogusRelationManyToAny<TSource, TDep> : FluentBogusRelation<TSource>, IFluentBogusRelationManyToAny<TSource, TDep>
      where TSource : class
      where TDep : class
  {
    public FluentBogusRelationManyToAny(TSource source) : base(source)
    {
    }

    public FluentBogusRelationManyToAny(TSource source, Expression<Func<TSource, ICollection<TDep>?>> expression) : this(source)
    {
      if (expression == null) throw new ArgumentNullException(nameof(expression));
      FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(expression));
      Dependency = expression.Compile().Invoke(source);
    }

    public FluentBogusRelationManyToAny(TSource source, ICollection<TDep>? dependency) : this(source)
    {
      Dependency = dependency;
    }

    /// <inheritdoc/>>
    public ICollection<TDep>? Dependency { get; private set; }

    /// <inheritdoc/>>
    public IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp> HasKey<TKeyProp>(Expression<Func<TSource, TKeyProp>> expression) => new FluentBogusRelationManyToAny<TSource, TDep, TKeyProp>(Source, Dependency, expression);
  }

  public class FluentBogusRelationManyToAny<TSource, TDep, TKeyProp> : FluentBogusRelationManyToAny<TSource, TDep>, IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp>
      where TSource : class
      where TDep : class
  {
    public FluentBogusRelationManyToAny(TSource source, ICollection<TDep>? dependency) : base(source, dependency)
    {
    }

    public FluentBogusRelationManyToAny(TSource source, ICollection<TDep>? dependency, Expression<Func<TSource, TKeyProp>>? sourceKeyExpression) : this(source, dependency)
    {
      if (sourceKeyExpression == null) throw new ArgumentNullException(nameof(sourceKeyExpression));
      FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(sourceKeyExpression));
      SourceKeyExpression = sourceKeyExpression;
    }

    /// <inheritdoc/>>
    public Expression<Func<TSource, TKeyProp>>? SourceKeyExpression { get; private set; }

    /// <inheritdoc/>>
    public IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression) => new FluentBogusRelationManyToOne<TSource, TDep, TKeyProp>(Source, Dependency, SourceKeyExpression, expression);
  }
}
