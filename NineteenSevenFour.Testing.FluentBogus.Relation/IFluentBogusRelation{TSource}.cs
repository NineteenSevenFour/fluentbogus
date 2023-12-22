// <copyright file="IFluentBogusRelation{TSource}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649
namespace NineteenSevenFour.Testing.FluentBogus.Relation;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

/// <summary>
/// Define a Many-to-Any relation.
/// </summary>
/// <typeparam name="TSource">The type of the source of the relation.</typeparam>
public interface IFluentBogusRelation<TSource>
  where TSource : class
{
  /// <summary>
  /// Allow to define a Many-to-Any relation.
  /// </summary>
  /// <typeparam name="TDep">The type of the dependency of the relation.</typeparam>
  /// <param name="depExpr">The fluent expression describing the navigation property to the dependency.</param>
  /// <returns>A <see cref="IFluentBogusRelationManyToAny{TSource,TDep}"/> providing a Many-to-Any relation.</returns>
  IFluentBogusRelationManyToAny<TSource, TDep> HasMany<TDep>(Expression<Func<TSource, ICollection<TDep>?>> depExpr)
    where TDep : class;

  /// <summary>
  /// Allow to define a One-to-Any relation.
  /// </summary>
  /// <typeparam name="TDep">The type of the dependency of the relation.</typeparam>
  /// <param name="depExpr">The fluent expression describing the navigation property to the dependency.</param>
  /// <returns>A <see cref="IFluentBogusRelationOneToAny{TSource,TDep}"/> providing a One-to-Any relation.</returns>
  IFluentBogusRelationOneToAny<TSource, TDep> HasOne<TDep>(Expression<Func<TSource, TDep?>> depExpr)
    where TDep : class;
}
