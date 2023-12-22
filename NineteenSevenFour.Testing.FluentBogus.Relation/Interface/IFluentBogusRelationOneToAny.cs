using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Relation.Interface;

public interface IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp>
    where TSource : class
    where TDep : class
{
  Expression<Func<TSource, TKeyProp?>>? SourceKeyExpression { get; }
  Expression<Func<TSource, TKeyProp?>>? SourceForeignKeyExpression { get; }
  IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> WithMany(Expression<Func<TDep, ICollection<TSource?>?>> expression);
  IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression);
}
