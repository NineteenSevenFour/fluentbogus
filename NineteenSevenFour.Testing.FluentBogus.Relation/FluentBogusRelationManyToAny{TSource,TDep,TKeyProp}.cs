// <copyright file="FluentBogusRelationManyToAny{TSource,TDep,TKeyProp}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NineteenSevenFour.Testing.Core;

#if NET8_0_OR_GREATER
public class FluentBogusRelationManyToAny<TSource, TDep, TKeyProp>(TSource source, ICollection<TDep>? dependency)
  : FluentBogusRelationManyToAny<TSource, TDep>(source, dependency),
    IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp>
  where TSource : class
  where TDep : class
{
#else
public class FluentBogusRelationManyToAny<TSource, TDep, TKeyProp>
  : FluentBogusRelationManyToAny<TSource, TDep>,
    IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp>
  where TSource : class
  where TDep : class
{
  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationManyToAny{TSource, TDep, TKeyProp}"/> class.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="dependency"></param>
  public FluentBogusRelationManyToAny(TSource source, ICollection<TDep>? dependency)
    : base(source, dependency)
  {
  }
#endif

  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationManyToAny{TSource, TDep, TKeyProp}"/> class.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="dependency"></param>
  /// <param name="sourceKeyExpression"></param>
  public FluentBogusRelationManyToAny(TSource source, ICollection<TDep>? dependency, Expression<Func<TSource, TKeyProp>>? sourceKeyExpression)
    : this(source, dependency)
  {
    ArgumentNullException.ThrowIfNull(sourceKeyExpression, nameof(sourceKeyExpression));
    FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(sourceKeyExpression));
    this.SourceKeyExpression = sourceKeyExpression;
  }

  internal Expression<Func<TSource, TKeyProp>>? SourceKeyExpression { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression) => new FluentBogusRelationManyToOne<TSource, TDep, TKeyProp>(this.Source, this.Dependency, this.SourceKeyExpression, expression);
}
