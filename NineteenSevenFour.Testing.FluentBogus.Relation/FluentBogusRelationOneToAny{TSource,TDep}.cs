// <copyright file="FluentBogusRelationOneToAny{TSource,TDep}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation;

using System;
using System.Linq.Expressions;
using NineteenSevenFour.Testing.Core;

public class FluentBogusRelationOneToAny<TSource, TDep> : FluentBogusRelation<TSource>, IFluentBogusRelationOneToAny<TSource, TDep>
  where TSource : class
  where TDep : class
{
  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationOneToAny{TSource, TDep}"/> class.
  /// </summary>
  /// <param name="source">The instance of the source of the relation.</param>
  public FluentBogusRelationOneToAny(TSource source)
    : base(source)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationOneToAny{TSource, TDep}"/> class.
  /// </summary>
  /// <param name="source">The instance of the source of the relation.</param>
  /// <param name="dependency">The instance of the dependency of the relation.</param>
  public FluentBogusRelationOneToAny(TSource source, TDep? dependency)
    : this(source)
  {
    this.Dependency = dependency;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationOneToAny{TSource, TDep}"/> class.
  /// </summary>
  /// <param name="source">The instance of the source of the relation.</param>
  /// <param name="depExpr">The expression that defines the property to use as the primary key of the relation.</param>
  public FluentBogusRelationOneToAny(TSource source, Expression<Func<TSource, TDep?>> depExpr)
    : this(source)
  {
    ArgumentNullException.ThrowIfNull(depExpr, nameof(depExpr));
    FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(depExpr));
    this.Dependency = depExpr.Compile().Invoke(source);
  }

  /// <summary>
  /// Gets the instance of the dependency of the relation.
  /// </summary>
#pragma warning disable SA1600
  internal TDep? Dependency { get; private set; }
#pragma warning restore SA1600

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp> HasForeignKey<TKeyProp>(Expression<Func<TSource, TKeyProp>> expression) => new FluentBogusRelationOneToAny<TSource, TDep, TKeyProp>(this.Source, this.Dependency, null, expression);

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp> HasKey<TKeyProp>(Expression<Func<TSource, TKeyProp>> expression) => new FluentBogusRelationOneToAny<TSource, TDep, TKeyProp>(this.Source, this.Dependency, expression, null);
}
