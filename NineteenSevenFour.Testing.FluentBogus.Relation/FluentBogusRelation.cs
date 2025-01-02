// <copyright file="FluentBogusRelation.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

/// <summary>
/// Extension methods to help defines entities relation using fluent notation.
/// </summary>
public static class FluentBogusRelation
{
  /// <summary>
  /// Defines the entry point of the Many-to-One relation.
  /// </summary>
  /// <typeparam name="TSource">The type of the source entity of the relation.</typeparam>
  /// <typeparam name="TDep">The type of the dependency of the relation.</typeparam>
  /// <param name="entity">The instance of the source of the relation.</param>
  /// <param name="expression">The expression describing the child property of the relation.</param>
  /// <returns>A <see cref="IFluentBogusRelationManyToAny{TSource,TDep}"/>.</returns>
  public static IFluentBogusRelationManyToAny<TSource, TDep> HasMany<TSource, TDep>(
    this TSource entity,
    Expression<Func<TSource, ICollection<TDep>?>> expression)
    where TSource : class
    where TDep : class => new FluentBogusRelation<TSource>(entity).HasMany(expression);

  /// <summary>
  /// Defines the entry point of the One-to-Many relation.
  /// </summary>
  /// <typeparam name="TSource">The type of the source entity of the relation.</typeparam>
  /// <typeparam name="TDep">The type of the dependency of the relation.</typeparam>
  /// <param name="entity">The instance of the source of the relation.</param>
  /// <param name="expression">The expression describing the child property of the relation.</param>
  /// <returns>A <see cref="IFluentBogusRelationOneToAny{TSource,TDep}"/>.</returns>
  public static IFluentBogusRelationOneToAny<TSource, TDep> HasOne<TSource, TDep>(
    this TSource entity,
    Expression<Func<TSource, TDep?>> expression)
    where TSource : class
    where TDep : class => new FluentBogusRelation<TSource>(entity).HasOne(expression);
}
