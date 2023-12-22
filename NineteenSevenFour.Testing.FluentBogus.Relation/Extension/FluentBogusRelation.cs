using NineteenSevenFour.Testing.FluentBogus.Relation.Interface;

using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Relation.Extension;

public static class FluentBogusRelation
{
  public static IFluentBogusRelationManyToAny<TSource, TDep> HasMany<TSource, TDep>(
      this TSource entity,
      Expression<Func<TSource, ICollection<TDep>?>> expression)
      where TSource : class
      where TDep : class => new FluentBogusRelation<TSource>(entity).HasMany(expression);

  public static IFluentBogusRelationOneToAny<TSource, TDep> HasOne<TSource, TDep>(
      this TSource entity,
      Expression<Func<TSource, TDep?>> expression)
      where TSource : class
      where TDep : class => new FluentBogusRelation<TSource>(entity).HasOne(expression);
}
