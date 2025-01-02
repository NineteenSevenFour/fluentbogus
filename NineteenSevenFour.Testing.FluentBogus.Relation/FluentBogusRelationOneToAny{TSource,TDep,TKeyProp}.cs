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
  /// <param name="source"></param>
  /// <param name="dependency"></param>
  public FluentBogusRelationOneToAny(TSource source, TDep? dependency)
    : base(source, dependency)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationOneToAny{TSource, TDep, TKeyProp}"/> class.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="dependency"></param>
  /// <param name="sourceKeyExpression"></param>
  /// <param name="sourceForeignKeyExpression"></param>
  public FluentBogusRelationOneToAny(
    TSource source,
    TDep? dependency,
    Expression<Func<TSource, TKeyProp>>? sourceKeyExpression,
    Expression<Func<TSource, TKeyProp>>? sourceForeignKeyExpression)
    : this(source, dependency)
  {
    if (sourceKeyExpression != null)
    {
      FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(sourceKeyExpression));
      this.SourceKeyExpression = sourceKeyExpression;
    }

    if (sourceForeignKeyExpression != null)
    {
      FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(sourceForeignKeyExpression));
      this.SourceForeignKeyExpression = sourceForeignKeyExpression;
    }
  }

  internal Expression<Func<TSource, TKeyProp>>? SourceKeyExpression { get; private set; }

  internal Expression<Func<TSource, TKeyProp>>? SourceForeignKeyExpression { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> WithMany(Expression<Func<TDep, ICollection<TSource>?>> expression) => new FluentBogusRelationOneToMany<TSource, TDep, TKeyProp>(this.Source, this.Dependency, this.SourceKeyExpression, this.SourceForeignKeyExpression, expression);

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression) => new FluentBogusRelationOneToOne<TSource, TDep, TKeyProp>(this.Source, this.Dependency, this.SourceKeyExpression, this.SourceForeignKeyExpression, expression);
}
