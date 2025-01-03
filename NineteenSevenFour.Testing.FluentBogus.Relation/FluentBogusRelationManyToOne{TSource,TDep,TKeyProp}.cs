// <copyright file="FluentBogusRelationManyToOne{TSource,TDep,TKeyProp}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649
namespace NineteenSevenFour.Testing.FluentBogus.Relation;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using NineteenSevenFour.Testing.Core;

#if NET8_0_OR_GREATER
public class FluentBogusRelationManyToOne<TSource, TDep, TKeyProp>(
  TSource source,
  ICollection<TDep>? dependency,
  Expression<Func<TSource, TKeyProp>>? sourceKeyExpression)
  : FluentBogusRelationManyToAny<TSource, TDep, TKeyProp>(source, dependency, sourceKeyExpression),
    IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp>
  where TSource : class
  where TDep : class
{
#else
public class FluentBogusRelationManyToOne<TSource, TDep, TKeyProp>
  : FluentBogusRelationManyToAny<TSource, TDep, TKeyProp>,
    IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp>
  where TSource : class
  where TDep : class
{
  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationManyToOne{TSource, TDep, TKeyProp}"/> class.
  /// </summary>
  /// <param name="source">The instance of the source of the relation.</param>
  /// <param name="dependency">The instance of the dependency of the relation.</param>
  /// <param name="keyExpression">The expression that defines the primary key of the relation.</param>
  public FluentBogusRelationManyToOne(
    TSource source,
    ICollection<TDep>? dependency,
    Expression<Func<TSource, TKeyProp>>? keyExpression)
    : base(source, dependency, keyExpression)
  {
  }
#endif

  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationManyToOne{TSource, TDep, TKeyProp}"/> class.
  /// </summary>
  /// <param name="source">The instance of the source of the relation.</param>
  /// <param name="dependency">The instance of the dependency of the relation.</param>
  /// <param name="keyExpr">The expression that defines the primary key of the relation.</param>
  /// <param name="withOneExpr">The expression that defines the One dependency property of the relation.</param>
  public FluentBogusRelationManyToOne(
    TSource source,
    ICollection<TDep>? dependency,
    Expression<Func<TSource, TKeyProp>>? keyExpr,
    Expression<Func<TDep, TSource?>>? withOneExpr)
    : this(source, dependency, keyExpr)
  {
    ArgumentNullException.ThrowIfNull(withOneExpr, nameof(withOneExpr));
    FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(withOneExpr));
    this.SourceRefExpression = withOneExpr;
  }

  internal Expression<Func<TDep, TKeyProp>>? ForeignKeyExpression { get; private set; }

  internal Expression<Func<TDep, TSource?>>? SourceRefExpression { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithForeignKey(Expression<Func<TDep, TKeyProp>> expression)
  {
    ArgumentNullException.ThrowIfNull(expression, nameof(expression));
    FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
    this.ForeignKeyExpression = expression;
    return this;
  }

  /// <inheritdoc/>>
  public void Apply()
  {
    if (this.KeyExpression == null)
    {
      throw new InvalidOperationException("The Many to One relation is not setup properly. The Source Key must be defined using HasKey().");
    }

    if (this.ForeignKeyExpression == null)
    {
      throw new InvalidOperationException("The Many to One relation is not setup properly. The dependency foreign key must be defined using WithForeignKey().");
    }

    if (this.Dependency == null || this.SourceRefExpression == null)
    {
      return;
    }

    foreach (var item in this.Dependency)
    {
      try
      {
        // Set Source reference on Dependency's item, beware that item could be null.
        // var sourceRef = this.SourceRefExpression.Compile().Invoke(item);
        var sourceKey = this.KeyExpression.Compile().Invoke(this.Source);

        // TODO: Review why commented out, use case ??
        // var dependencyForeignKey = DependencyForeignKeyExpression.Compile().Invoke(item);

        // Set Dependency's item foreign key to Source key
        if (sourceKey != null)
        {
          // (sourceKey != null && dependencyForeignKey != null)
          FluentExpression.SetField(item, this.SourceRefExpression, this.Source);
          FluentExpression.SetField(item, this.ForeignKeyExpression, sourceKey);
        }
        else
        {
          throw new InvalidOperationException("The Many to One relation is not setup properly. Please review the relation definition as well as the entity definition.");
        }
      }
      catch (Exception e)
      {
        // TODO: Review null item in collection, Is this an actual use case ? Does it make sense to allow null object in collection ?
        throw new InvalidOperationException("The Source can not be been defined due to dependency being null.", e);
      }
    }
  }
}
