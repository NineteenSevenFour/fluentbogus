using NineteenSevenFour.Testing.Core.Extension;
using NineteenSevenFour.Testing.FluentBogus.Relation.Interface;
using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Relation;

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

#region HasMany
public class FluentBogusRelationManyToAny<TSource, TDep> : FluentBogusRelation<TSource>, IFluentBogusRelationManyToAny<TSource, TDep>
    where TSource : class
    where TDep : class
{
  public FluentBogusRelationManyToAny(TSource source) : base(source)
  {
  }
  public FluentBogusRelationManyToAny(TSource source, Expression<Func<TSource, ICollection<TDep>?>> expression) : this(source)
  {
    if (expression == null) throw new ArgumentNullException(nameof(expression));
    FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(expression));
    Dependency = expression.Compile()?.Invoke(source);
  }

  public FluentBogusRelationManyToAny(TSource source, ICollection<TDep>? dependency) : this(source)
  {
    Dependency = dependency;
  }

  /// <inheritdoc/>>
  public ICollection<TDep>? Dependency { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp> HasKey<TKeyProp>(Expression<Func<TSource, TKeyProp?>> expression) => new FluentBogusRelationManyToAny<TSource, TDep, TKeyProp>(Source, Dependency, expression);
}

public class FluentBogusRelationManyToAny<TSource, TDep, TKeyProp> : FluentBogusRelationManyToAny<TSource, TDep>, IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp>
    where TSource : class
    where TDep : class
{
  public FluentBogusRelationManyToAny(TSource source, ICollection<TDep>? dependency) : base(source, dependency)
  {
  }
  public FluentBogusRelationManyToAny(TSource source, ICollection<TDep>? dependency, Expression<Func<TSource, TKeyProp?>>? sourceKeyExpression) : this(source, dependency)
  {
    if (sourceKeyExpression == null) throw new ArgumentNullException(nameof(sourceKeyExpression));
    FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(sourceKeyExpression));
    SourceKeyExpression = sourceKeyExpression;
  }

  /// <inheritdoc/>>
  public Expression<Func<TSource, TKeyProp?>>? SourceKeyExpression { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression) => new FluentBogusRelationManyToOne<TSource, TDep, TKeyProp>(Source, Dependency, SourceKeyExpression, expression);
}

#region WithOne
public class FluentBogusRelationManyToOne<TSource, TDep, TKeyProp> : FluentBogusRelationManyToAny<TSource, TDep, TKeyProp>, IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp>
    where TSource : class
    where TDep : class
{
  public FluentBogusRelationManyToOne(
      TSource source,
      ICollection<TDep>? dependency,
      Expression<Func<TSource, TKeyProp?>>? sourceKeyExpression) : base(source, dependency, sourceKeyExpression)
  {
  }
  public FluentBogusRelationManyToOne(
      TSource source,
      ICollection<TDep>? dependency,
      Expression<Func<TSource, TKeyProp?>>? sourceKeyExpression,
      Expression<Func<TDep, TSource?>>? withOneExpression) : this(source, dependency, sourceKeyExpression)
  {
    if (withOneExpression == null) throw new ArgumentNullException(nameof(withOneExpression));
    FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(withOneExpression));
    SourceRefExpression = withOneExpression;
  }

  /// <inheritdoc/>>
  public Expression<Func<TDep, TKeyProp?>>? DependencyForeignKeyExpression { get; private set; }

  /// <inheritdoc/>>
  public Expression<Func<TDep, TSource?>>? SourceRefExpression { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithForeignKey(Expression<Func<TDep, TKeyProp?>> expression)
  {
    if (expression == null) throw new ArgumentNullException(nameof(expression));
    FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
    DependencyForeignKeyExpression = expression;
    return this;
  }

  /// <inheritdoc/>>
  public void Apply()
  {
    if (Dependency != null && SourceRefExpression != null)
    {
      foreach (var item in Dependency)
      {
        if (item == null) continue;

        // Set Source reference on Dependency's item
        var sourceRef = SourceRefExpression.Compile().Invoke(item);
        if (sourceRef == null)
        {
          // Set Depndency's item foreign key to Source key
          if (SourceKeyExpression != null && DependencyForeignKeyExpression != null)
          {
            var sourceKey = SourceKeyExpression.Compile().Invoke(Source);
            var dependencyForeignKey = DependencyForeignKeyExpression.Compile().Invoke(item);

            if (sourceKey != null && dependencyForeignKey != null)
            {
              FluentExpression.SetField(item, SourceRefExpression, Source);
              FluentExpression.SetField(item, DependencyForeignKeyExpression, sourceKey);
            }
            else
            {
              throw new InvalidOperationException("The Many to One relation is not setup properly. Please review the relation definition as well as the entity definition.");
            }
          }
          else
          {
            if (SourceKeyExpression != null)
            {
              throw new ArgumentNullException(nameof(SourceKeyExpression), "The Source Key must be defined using HasKey().");
            }
            if (DependencyForeignKeyExpression != null)
            {
              throw new ArgumentNullException(nameof(DependencyForeignKeyExpression), "The dependency foreign key must be defined using WithForeignKey().");
            }
          }
        }
      }
    }
  }
}
#endregion
#endregion

#region HasOne
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

#region WithMany
public class FluentBogusRelationOneToMany<TSource, TDep, TKeyProp> : FluentBogusRelationOneToAny<TSource, TDep, TKeyProp>, IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp>
    where TSource : class
    where TDep : class
{
  public FluentBogusRelationOneToMany(
      TSource source,
      TDep? dependency,
      Expression<Func<TSource, TKeyProp?>>? sourceKeyExpression,
      Expression<Func<TSource, TKeyProp?>>? sourceForeignKeyExpression,
      Expression<Func<TDep, ICollection<TSource?>?>> sourceRefExpression) : base(source, dependency, sourceKeyExpression, sourceForeignKeyExpression)
  {
    if (sourceRefExpression == null) throw new ArgumentNullException(nameof(sourceRefExpression));
    FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(sourceRefExpression));
    SourceRefExpression = sourceRefExpression;
  }

  /// <inheritdoc/>>
  public Expression<Func<TDep, ICollection<TSource?>?>>? SourceRefExpression { get; private set; }

  /// <inheritdoc/>>
  public Expression<Func<TDep, TKeyProp?>>? WithKeyExpression { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> WithKey(Expression<Func<TDep, TKeyProp?>> expression)
  {
    if (expression == null) throw new ArgumentNullException(nameof(expression));
    FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
    WithKeyExpression = expression;
    return this;
  }

  /// <inheritdoc/>>
  public void Apply()
  {
    if (Dependency != null && SourceRefExpression != null)
    {
      var sourceRef = SourceRefExpression.Compile().Invoke(Dependency);

      sourceRef ??= default;
      if (sourceRef?.Any() == false)
      {
        if (SourceForeignKeyExpression != null && WithKeyExpression != null)
        {
          var sourceForeignKey = SourceForeignKeyExpression.Compile().Invoke(Source);
          var withKey = WithKeyExpression.Compile().Invoke(Dependency);

          if (sourceForeignKey != null && withKey != null)
          {
            sourceRef.Add(Source);
            FluentExpression.SetField(Dependency, SourceRefExpression, sourceRef);
            FluentExpression.SetField(Source, SourceForeignKeyExpression, withKey);
          }
        }
        else
        {
          if (SourceForeignKeyExpression != null)
          {
            throw new ArgumentNullException(nameof(SourceForeignKeyExpression), "The Source Foreign Key must be defined using HasForeignKey().");
          }
          if (WithKeyExpression != null)
          {
            throw new ArgumentNullException(nameof(WithKeyExpression), "The dependency key must be defined using WithKey().");
          }
        }
      }
    }
  }
}
#endregion

#region WithOne
public class FluentBogusRelationOneToOne<TSource, TDep, TKeyProp> : FluentBogusRelationOneToAny<TSource, TDep, TKeyProp>, IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp>
where TSource : class
where TDep : class
{
  public FluentBogusRelationOneToOne
      (TSource source,
      TDep? dependency,
      Expression<Func<TSource, TKeyProp?>>? sourceKeyExpression,
      Expression<Func<TSource, TKeyProp?>>? sourceForeignKeyExpression,
      Expression<Func<TDep, TSource?>> expression) : base(source, dependency, sourceKeyExpression, sourceForeignKeyExpression)
  {
    if (expression == null) throw new ArgumentNullException(nameof(expression));
    FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
    SourceRefExpression = expression;
  }

  /// <inheritdoc/>>
  public Expression<Func<TDep, TSource?>>? SourceRefExpression { get; private set; }

  /// <inheritdoc/>>
  public Expression<Func<TDep, TKeyProp?>>? WithForeignKeyExpression { get; private set; }

  /// <inheritdoc/>>
  public Expression<Func<TDep, TKeyProp?>>? WithKeyExpression { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithForeignKey(Expression<Func<TDep, TKeyProp?>> expression)
  {
    if (expression == null) throw new ArgumentNullException(nameof(expression));
    FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
    WithForeignKeyExpression = expression;
    return this;
  }

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithKey(Expression<Func<TDep, TKeyProp?>> expression)
  {
    if (expression == null) throw new ArgumentNullException(nameof(expression));
    FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
    WithKeyExpression = expression;
    return this;
  }

  /// <inheritdoc/>>
  public void Apply()
  {
    if (Dependency != null && SourceRefExpression != null)
    {
      var sourceRef = SourceRefExpression.Compile().Invoke(Dependency);

      if (sourceRef == null)
      {
        if (SourceKeyExpression != null && WithForeignKeyExpression != null)
        {
          var sourceKey = SourceKeyExpression.Compile().Invoke(Source);
          var withForeignKey = WithForeignKeyExpression.Compile().Invoke(Dependency);

          if (sourceKey != null && withForeignKey != null)
          {
            FluentExpression.SetField(Dependency, SourceRefExpression, Source);
            FluentExpression.SetField(Dependency, WithForeignKeyExpression, sourceKey);
          }
        }
        else if (SourceForeignKeyExpression != null && WithKeyExpression != null)
        {
          var sourceForeignKey = SourceForeignKeyExpression.Compile().Invoke(Source);
          var withKey = WithKeyExpression.Compile().Invoke(Dependency);

          if (sourceForeignKey != null && withKey != null)
          {
            FluentExpression.SetField(Dependency, SourceRefExpression, Source);
            FluentExpression.SetField(Source, SourceForeignKeyExpression, withKey);
          }
        }
        else
        {
          if (SourceKeyExpression != null && WithForeignKeyExpression != null)
          {
            if (SourceKeyExpression != null)
            {
              throw new ArgumentNullException(nameof(SourceKeyExpression), "The Source Key must be defined using HasKey().");
            }
            if (WithForeignKeyExpression != null)
            {
              throw new ArgumentNullException(nameof(WithForeignKeyExpression), "The dependency key must be defined using WithForeignKey().");
            }
          }
          else if (SourceForeignKeyExpression != null && WithKeyExpression != null)
          {
            if (SourceForeignKeyExpression != null)
            {
              throw new ArgumentNullException(nameof(SourceForeignKeyExpression), "The Source Foreign Key must be defined using HasForeignKey().");
            }
            if (WithKeyExpression != null)
            {
              throw new ArgumentNullException(nameof(WithKeyExpression), "The dependency key must be defined using WithKey().");
            }
          }
          throw new InvalidOperationException("The relation configuration is incorrect. Check the use of HasKey(), HasForeignKey(), WithKey(), HasForeignKey()");
        }
      }
    }
  }
}
#endregion
#endregion
