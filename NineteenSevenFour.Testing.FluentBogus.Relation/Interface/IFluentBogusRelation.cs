using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Relation.Interface;

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

#region HasMany
public interface IFluentBogusRelationManyToAny<TSource, TDep>
    where TSource : class
    where TDep : class
{
  ICollection<TDep>? Dependency { get; }
  IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp> HasKey<TKeyProp>(Expression<Func<TSource, TKeyProp?>> expression);
}
public interface IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp>
    where TSource : class
    where TDep : class
{
  Expression<Func<TSource, TKeyProp?>>? SourceKeyExpression { get; }
  IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression);
}

#region WithOne
public interface IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> : IFluentBogusRelation
    where TSource : class
    where TDep : class
{
  Expression<Func<TDep, TKeyProp?>>? DependencyForeignKeyExpression { get; }
  Expression<Func<TDep, TSource?>>? SourceRefExpression { get; }
  IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithForeignKey(Expression<Func<TDep, TKeyProp?>> expression);
}
#endregion
#endregion

#region HasOne
public interface IFluentBogusRelationOneToAny<TSource, TDep>
    where TSource : class
    where TDep : class
{
  TDep? Dependency { get; }

  IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp> HasKey<TKeyProp>(Expression<Func<TSource, TKeyProp?>> expression);
  IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp> HasForeignKey<TKeyProp>(Expression<Func<TSource, TKeyProp?>> expression);
}
public interface IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp>
    where TSource : class
    where TDep : class
{
  Expression<Func<TSource, TKeyProp?>>? SourceKeyExpression { get; }
  Expression<Func<TSource, TKeyProp?>>? SourceForeignKeyExpression { get; }
  IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> WithMany(Expression<Func<TDep, ICollection<TSource?>?>> expression);
  IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression);
}

#region WithMany
public interface IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> : IFluentBogusRelation
    where TSource : class
    where TDep : class
{
  Expression<Func<TDep, ICollection<TSource?>?>>? SourceRefExpression { get; }
  Expression<Func<TDep, TKeyProp?>>? WithKeyExpression { get; }
  IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> WithKey(Expression<Func<TDep, TKeyProp?>> expression);
}
#endregion

#region WithOne
public interface IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> : IFluentBogusRelation
    where TSource : class
    where TDep : class
{
  Expression<Func<TDep, TSource?>>? SourceRefExpression { get; }
  Expression<Func<TDep, TKeyProp?>>? WithForeignKeyExpression { get; }
  Expression<Func<TDep, TKeyProp?>>? WithKeyExpression { get; }
  IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithKey(Expression<Func<TDep, TKeyProp?>> expression);
  IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithForeignKey(Expression<Func<TDep, TKeyProp?>> expression);
}
#endregion
#endregion
