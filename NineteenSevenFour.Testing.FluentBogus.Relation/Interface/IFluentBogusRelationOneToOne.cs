using System;
using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Relation.Interface
{
  public interface IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> : IFluentBogusRelation
      where TSource : class
      where TDep : class
  {
    Expression<Func<TDep, TSource?>>? SourceRefExpression { get; }
    Expression<Func<TDep, TKeyProp>>? WithForeignKeyExpression { get; }
    Expression<Func<TDep, TKeyProp>>? WithKeyExpression { get; }
    IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithKey(Expression<Func<TDep, TKeyProp>> expression);
    IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithForeignKey(Expression<Func<TDep, TKeyProp>> expression);
  }
}
