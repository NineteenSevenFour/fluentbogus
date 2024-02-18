using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Relation.Interface
{
  public interface IFluentBogusRelationManyToAny<TSource, TDep>
      where TSource : class
      where TDep : class
  {
    ICollection<TDep>? Dependency { get; }
    IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp> HasKey<TKeyProp>(Expression<Func<TSource, TKeyProp>> expression);
  }

  public interface IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp>
      where TSource : class
      where TDep : class
  {
    Expression<Func<TSource, TKeyProp>>? SourceKeyExpression { get; }
    IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression);
  }
}
