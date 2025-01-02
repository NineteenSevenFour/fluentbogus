// <copyright file="FluentBogusRelationManyToAny{TSource,TDep}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NineteenSevenFour.Testing.Core;

#if NET8_0_OR_GREATER
/// <summary>
/// Defines a Many to Any relation.
/// </summary>
/// <typeparam name="TSource">The type of the source of the relation.</typeparam>
/// <typeparam name="TDep">The type of the dependency (child or children) of the relation.</typeparam>
/// <param name="source">Instance of the source of the relation.</param>
public class FluentBogusRelationManyToAny<TSource, TDep>(TSource source)
  : FluentBogusRelation<TSource>(source), IFluentBogusRelationManyToAny<TSource, TDep>
  where TSource : class
  where TDep : class
{
#else
/// <summary>
/// Defines a Many to Any relation.
/// </summary>
/// <typeparam name="TSource">The type of the source of the relation.</typeparam>
/// <typeparam name="TDep">The type of the dependency (child or children) of the relation.</typeparam>
public class FluentBogusRelationManyToAny<TSource, TDep>
  : FluentBogusRelation<TSource>, IFluentBogusRelationManyToAny<TSource, TDep>
  where TSource : class
  where TDep : class
{
  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationManyToAny{TSource, TDep}"/> class.
  /// </summary>
  /// <param name="source"></param>
  public FluentBogusRelationManyToAny(TSource source)
      : base(source)
  {
  }
#endif

  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationManyToAny{TSource, TDep}"/> class.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="expression"></param>
  public FluentBogusRelationManyToAny(TSource source, Expression<Func<TSource, ICollection<TDep>?>> expression)
      : this(source)
  {
    ArgumentNullException.ThrowIfNull(expression, nameof(expression));
    FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(expression));
    this.Dependency = expression.Compile().Invoke(source);
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationManyToAny{TSource, TDep}"/> class.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="dependency"></param>
  public FluentBogusRelationManyToAny(TSource source, ICollection<TDep>? dependency)
    : this(source)
  {
    this.Dependency = dependency;
  }

  internal ICollection<TDep>? Dependency { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp> HasKey<TKeyProp>(Expression<Func<TSource, TKeyProp>> expression) => new FluentBogusRelationManyToAny<TSource, TDep, TKeyProp>(this.Source, this.Dependency, expression);
}
