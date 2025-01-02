// <copyright file="IFluentBogusRelationManyToAny{TSource,TDep}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649
namespace NineteenSevenFour.Testing.FluentBogus.Relation;

using System;
using System.Linq.Expressions;

/// <summary>
/// Defines a Many-to-Any relation.
/// </summary>
/// <typeparam name="TSource">The type of the source of the relation.</typeparam>
/// <typeparam name="TDep">The type of the dependency of the relation.</typeparam>
public interface IFluentBogusRelationManyToAny<TSource, TDep>
  where TSource : class
  where TDep : class
{
  /// <summary>
  /// Allows to define the primary key of the Many-To-Any relation.
  /// </summary>
  /// <typeparam name="TKeyProp">The type of the property used as primary key of the relation.</typeparam>
  /// <param name="expression">The expression that defines the property to use as the primary key of the relation.</param>
  /// <returns>A <see cref="IFluentBogusRelationManyToAny{TSource,TDep,TKeyProp}"/>.</returns>
  IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp> HasKey<TKeyProp>(Expression<Func<TSource, TKeyProp>> expression);
}
