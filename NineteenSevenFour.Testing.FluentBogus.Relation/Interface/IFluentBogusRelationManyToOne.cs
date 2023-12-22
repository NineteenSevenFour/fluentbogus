using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Relation.Interface;

public interface IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> : IFluentBogusRelation
    where TSource : class
    where TDep : class
{
  Expression<Func<TDep, TKeyProp?>>? DependencyForeignKeyExpression { get; }
  Expression<Func<TDep, TSource?>>? SourceRefExpression { get; }
  IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithForeignKey(Expression<Func<TDep, TKeyProp?>> expression);
}
