// <copyright file="IFluentBogusRelationManyToAny{TSource,TDep,TKeyProp}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649

namespace NineteenSevenFour.Testing.FluentBogus.Relation;

using System;
using System.Linq.Expressions;

/// <summary>
/// Defines a Many-to-One relation.
/// </summary>
/// <typeparam name="TSource">The type of the source of the relation.</typeparam>
/// <typeparam name="TDep">The type of the dependency of the relation.</typeparam>
/// <typeparam name="TKeyProp">The type of the property used as key to link the parent and child.</typeparam>
public interface IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp>
  where TSource : class
  where TDep : class
{
  /// <summary>
  /// Allows to define the One dependency of the Many-to-One relation.
  /// </summary>
  /// <param name="expression">The expression that defines the One dependency part of the Many-to-One relation.</param>
  /// <returns>A <see cref="IFluentBogusRelationManyToOne{TSource,TDep,TKeyProp}"/>.</returns>
  IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression);
}
