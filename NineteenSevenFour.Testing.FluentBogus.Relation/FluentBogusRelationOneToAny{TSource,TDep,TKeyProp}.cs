// <copyright file="FluentBogusRelationOneToAny{TSource,TDep,TKeyProp}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NineteenSevenFour.Testing.Core;

public class FluentBogusRelationOneToAny<TSource, TDep, TKeyProp> : FluentBogusRelationOneToAny<TSource, TDep>, IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp>
  where TSource : class
  where TDep : class
{
  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationOneToAny{TSource, TDep, TKeyProp}"/> class.
  /// </summary>
  /// <param name="source">The instance of the source of the relation.</param>
  /// <param name="dependency">The instance of the dependency of the relation.</param>
  public FluentBogusRelationOneToAny(TSource source, TDep? dependency)
    : base(source, dependency)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationOneToAny{TSource, TDep, TKeyProp}"/> class.
  /// </summary>
  /// <param name="source">The instance of the source of the relation.</param>
  /// <param name="dependency">The instance of the dependency of the relation.</param>
  /// <param name="keyExpression">The expression that defines the primary key.</param>
  /// <param name="foreignKeyExpression">The expression that defines the foreign key.</param>
  public FluentBogusRelationOneToAny(
    TSource source,
    TDep? dependency,
    Expression<Func<TSource, TKeyProp>>? keyExpression,
    Expression<Func<TSource, TKeyProp>>? foreignKeyExpression)
    : this(source, dependency)
  {
    if (keyExpression != null)
    {
      FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(keyExpression));
      this.KeyExpression = keyExpression;
    }

    if (foreignKeyExpression != null)
    {
      FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(foreignKeyExpression));
      this.ForeignKeyExpression = foreignKeyExpression;
    }
  }

  /// <summary>
  /// Gets the expression that defines the primary key.
  /// </summary>
  internal Expression<Func<TSource, TKeyProp>> KeyExpression { get; private set; }

  /// <summary>
  /// Gets the expression that defines the foreign key.
  /// </summary>
  internal Expression<Func<TSource, TKeyProp>> ForeignKeyExpression { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> WithMany(Expression<Func<TDep, ICollection<TSource>?>?> expression) => new FluentBogusRelationOneToMany<TSource, TDep, TKeyProp>(this.Source, this.Dependency, this.KeyExpression, this.ForeignKeyExpression, expression);

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression) => new FluentBogusRelationOneToOne<TSource, TDep, TKeyProp>(this.Source, this.Dependency, this.KeyExpression, this.ForeignKeyExpression, expression);
}
