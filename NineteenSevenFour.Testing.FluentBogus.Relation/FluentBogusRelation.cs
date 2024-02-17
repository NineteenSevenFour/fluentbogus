using NineteenSevenFour.Testing.FluentBogus.Relation.Interface;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  public class FluentBogusRelation<TSource> : IFluentBogusRelation<TSource>
    where TSource : class
  {
    public TSource Source { get; }

    public FluentBogusRelation(TSource source)
    {
      Source = source ?? throw new ArgumentNullException(nameof(source));
    }

    /// <inheritdoc/>>
    public IFluentBogusRelationManyToAny<TSource, TDep> HasMany<TDep>(Expression<Func<TSource, ICollection<TDep>?>> expression)
      where TDep : class => new FluentBogusRelationManyToAny<TSource, TDep>(Source, expression);

    /// <inheritdoc/>>
    public IFluentBogusRelationOneToAny<TSource, TDep> HasOne<TDep>(Expression<Func<TSource, TDep?>> expression)
      where TDep : class => new FluentBogusRelationOneToAny<TSource, TDep>(Source, expression);
  }
}
