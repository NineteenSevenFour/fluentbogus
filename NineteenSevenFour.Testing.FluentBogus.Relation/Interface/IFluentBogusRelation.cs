using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Relation.Interface
{
  public interface IFluentBogusRelation
  {
    void Apply();
  }

  public interface IFluentBogusRelation<TSource>
      where TSource : class
  {
    TSource Source { get; }

    IFluentBogusRelationManyToAny<TSource, TDep> HasMany<TDep>(Expression<Func<TSource, ICollection<TDep>?>> expression)
        where TDep : class;

    IFluentBogusRelationOneToAny<TSource, TDep> HasOne<TDep>(Expression<Func<TSource, TDep?>> expression)
        where TDep : class;
  }
}
