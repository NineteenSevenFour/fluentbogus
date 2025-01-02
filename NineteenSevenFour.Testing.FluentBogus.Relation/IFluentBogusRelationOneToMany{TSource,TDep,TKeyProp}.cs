// <copyright file="IFluentBogusRelationOneToMany{TSource,TDep,TKeyProp}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649
namespace NineteenSevenFour.Testing.FluentBogus.Relation;

using System;
using System.Linq.Expressions;

/// <summary>
/// Defines the One-to-Many relation.
/// </summary>
/// <typeparam name="TSource">The type of the parent of the relation.</typeparam>
/// <typeparam name="TDep">The type of the child of the relation.</typeparam>
/// <typeparam name="TKeyProp">The type of the property used as key to kink the parent and child.</typeparam>
public interface IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> : IFluentBogusRelation
  where TSource : class
  where TDep : class
{
  /// <summary>
  /// Allow to configure the key used to link the parent to the child.
  /// </summary>
  /// <param name="expression">The fluent expression describing the parent key.</param>
  /// <returns>A <see cref="IFluentBogusRelationOneToMany{TSource,TDep,TKeyProp}"/>.</returns>
  IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> WithKey(Expression<Func<TDep, TKeyProp>> expression);
}
