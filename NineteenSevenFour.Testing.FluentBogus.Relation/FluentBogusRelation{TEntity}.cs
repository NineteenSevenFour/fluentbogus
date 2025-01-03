// <copyright file="FluentBogusRelation{TEntity}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649
namespace NineteenSevenFour.Testing.FluentBogus.Relation;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#if NET8_0_OR_GREATER
/// <summary>
/// Define a relation between parent and child or children.
/// </summary>
/// <typeparam name="TSource">Type of the source of the relation.</typeparam>
/// <param name="source">Instance of the source of the relation.</param>
public class FluentBogusRelation<TSource>(TSource source) : IFluentBogusRelation<TSource>
  where TSource : class
{
  /// <summary>
  /// Gets the instance of the source of the relation.
  /// </summary>
  internal TSource Source => source;
#else
/// <summary>
/// Define a relation between parent and child or children.
/// </summary>
/// <typeparam name="TSource">Type of the source of the relation.</typeparam>
public class FluentBogusRelation<TSource> : IFluentBogusRelation<TSource>
 where TSource : class
{
  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelation{TSource}"/> class.
  /// </summary>
  /// <param name="source">Instance of the source of the relation.</param>
  public FluentBogusRelation(TSource source)
  {
    this.Source = source;
  }

  /// <summary>
  /// Gets the instance of the source of the relation.
  /// </summary>
  internal TSource Source { get; }
#endif

  /// <inheritdoc/>>
  public IFluentBogusRelationManyToAny<TSource, TDep> HasMany<TDep>(Expression<Func<TSource, ICollection<TDep>?>> depExpr)
    where TDep : class => new FluentBogusRelationManyToAny<TSource, TDep>(this.Source, depExpr);

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToAny<TSource, TDep> HasOne<TDep>(Expression<Func<TSource, TDep?>> depExpr)
    where TDep : class => new FluentBogusRelationOneToAny<TSource, TDep>(this.Source, depExpr);
}
