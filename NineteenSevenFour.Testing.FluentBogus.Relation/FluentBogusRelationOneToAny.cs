using NineteenSevenFour.Testing.Core.Extension;
using NineteenSevenFour.Testing.FluentBogus.Relation.Interface;

using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Relation;

public class FluentBogusRelationOneToAny<TSource, TDep> : FluentBogusRelation<TSource>, IFluentBogusRelationOneToAny<TSource, TDep>
where TSource : class
where TDep : class
{
  public FluentBogusRelationOneToAny(TSource source) : base(source)
  {
  }
  public FluentBogusRelationOneToAny(TSource source, TDep? dependency) : this(source)
  {
    Dependency = dependency;
  }
  public FluentBogusRelationOneToAny(TSource source, Expression<Func<TSource, TDep?>> expression) : this(source)
  {
    if (expression == null) throw new ArgumentNullException(nameof(expression));
    FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(expression));
    Dependency = expression.Compile().Invoke(source);
  }

  /// <inheritdoc/>>
  public TDep? Dependency { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp> HasForeignKey<TKeyProp>(Expression<Func<TSource, TKeyProp?>> expression) => new FluentBogusRelationOneToAny<TSource, TDep, TKeyProp>(Source, Dependency, null, expression);

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp> HasKey<TKeyProp>(Expression<Func<TSource, TKeyProp?>> expression) => new FluentBogusRelationOneToAny<TSource, TDep, TKeyProp>(Source, Dependency, expression, null);
}

public class FluentBogusRelationOneToAny<TSource, TDep, TKeyProp> : FluentBogusRelationOneToAny<TSource, TDep>, IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp>
    where TSource : class
    where TDep : class
{
  public FluentBogusRelationOneToAny(TSource source, TDep? dependency) : base(source, dependency)
  {
  }

  public FluentBogusRelationOneToAny(
      TSource source,
      TDep? dependency,
      Expression<Func<TSource, TKeyProp?>>? sourceKeyExpression,
      Expression<Func<TSource, TKeyProp?>>? sourceForeignKeyExpression) : this(source, dependency)
  {
    if (sourceKeyExpression != null)
    {
      FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(sourceKeyExpression));
      SourceKeyExpression = sourceKeyExpression;
    }

    if (sourceForeignKeyExpression != null)
    {
      FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(sourceForeignKeyExpression));
      SourceForeignKeyExpression = sourceForeignKeyExpression;
    }
  }

  /// <inheritdoc/>>
  public Expression<Func<TSource, TKeyProp?>>? SourceKeyExpression { get; private set; }

  /// <inheritdoc/>>
  public Expression<Func<TSource, TKeyProp?>>? SourceForeignKeyExpression { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> WithMany(Expression<Func<TDep, ICollection<TSource?>?>> expression) => new FluentBogusRelationOneToMany<TSource, TDep, TKeyProp>(Source, Dependency, SourceKeyExpression, SourceForeignKeyExpression, expression);

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression) => new FluentBogusRelationOneToOne<TSource, TDep, TKeyProp>(Source, Dependency, SourceKeyExpression, SourceForeignKeyExpression, expression);
}
