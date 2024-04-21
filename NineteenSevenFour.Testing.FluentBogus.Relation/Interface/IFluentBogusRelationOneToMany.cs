using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Relation.Interface
{
  public interface IFluentBogusRelationOneToAny<TSource, TDep>
      where TSource : class
      where TDep : class
  {
    TDep? Dependency { get; }

    IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp> HasKey<TKeyProp>(Expression<Func<TSource, TKeyProp>> expression);
    IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp> HasForeignKey<TKeyProp>(Expression<Func<TSource, TKeyProp>> expression);
  }

  public interface IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> : IFluentBogusRelation
      where TSource : class
      where TDep : class
  {
    Expression<Func<TDep, ICollection<TSource?>?>>? SourceRefExpression { get; }
    Expression<Func<TDep, TKeyProp>>? WithKeyExpression { get; }
    IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> WithKey(Expression<Func<TDep, TKeyProp>> expression);
  }
}
