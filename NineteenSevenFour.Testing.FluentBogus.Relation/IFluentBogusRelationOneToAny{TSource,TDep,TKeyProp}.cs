// <copyright file="IFluentBogusRelationOneToAny{TSource,TDep,TKeyProp}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649
namespace NineteenSevenFour.Testing.FluentBogus.Relation;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

/// <summary>
/// Defines a One-to-Any relation.
/// </summary>
/// <typeparam name="TSource">The type of the source of the relation.</typeparam>
/// <typeparam name="TDep">The type of the dependency of the relation.</typeparam>
/// <typeparam name="TKeyProp">The type of the property used as foreign key of the relation.</typeparam>
public interface IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp>
  where TSource : class
  where TDep : class
{
  /// <summary>
  /// Allows to define the Many dependency of the Many-to-Any relation.
  /// </summary>
  /// <param name="expression">The expression that defines the One dependency part of the Many-to-Any relation.</param>
  /// <returns>A <see cref="IFluentBogusRelationOneToMany{TSource,TDep,TKeyProp}"/>.</returns>
  IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> WithMany(Expression<Func<TDep, ICollection<TSource>?>> expression);

  /// <summary>
  /// Allows to define the One dependency of the One-to-Any relation.
  /// </summary>
  /// <param name="expression">The expression that defines the One dependency part of the One-to-Any relation.</param>
  /// <returns>A <see cref="IFluentBogusRelationOneToOne{TSource,TDep,TKeyProp}"/>.</returns>
  IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression);
}
