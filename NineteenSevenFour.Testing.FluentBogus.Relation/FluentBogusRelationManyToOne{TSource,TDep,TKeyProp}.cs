// <copyright file="FluentBogusRelationManyToOne{TSource,TDep,TKeyProp}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using NineteenSevenFour.Testing.Core;

#if NET8_0_OR_GREATER
public class FluentBogusRelationManyToOne<TSource, TDep, TKeyProp>(
  TSource source,
  ICollection<TDep>? dependency,
  Expression<Func<TSource, TKeyProp>>? sourceKeyExpression)
  : FluentBogusRelationManyToAny<TSource, TDep, TKeyProp>(source, dependency, sourceKeyExpression),
    IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp>
  where TSource : class
  where TDep : class
{
#else
public class FluentBogusRelationManyToOne<TSource, TDep, TKeyProp>
  : FluentBogusRelationManyToAny<TSource, TDep, TKeyProp>,
    IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp>
  where TSource : class
  where TDep : class
{
  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationManyToOne{TSource, TDep, TKeyProp}"/> class.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="dependency"></param>
  /// <param name="sourceKeyExpression"></param>
  public FluentBogusRelationManyToOne(
    TSource source,
    ICollection<TDep>? dependency,
    Expression<Func<TSource, TKeyProp>>? sourceKeyExpression)
    : base(source, dependency, sourceKeyExpression)
  {
  }
#endif

  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationManyToOne{TSource, TDep, TKeyProp}"/> class.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="dependency"></param>
  /// <param name="sourceKeyExpression"></param>
  /// <param name="withOneExpression"></param>
  public FluentBogusRelationManyToOne(
    TSource source,
    ICollection<TDep>? dependency,
    Expression<Func<TSource, TKeyProp>>? sourceKeyExpression,
    Expression<Func<TDep, TSource?>>? withOneExpression)
    : this(source, dependency, sourceKeyExpression)
  {
    ArgumentNullException.ThrowIfNull(withOneExpression, nameof(withOneExpression));
    FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(withOneExpression));
    this.SourceRefExpression = withOneExpression;
  }

  internal Expression<Func<TDep, TKeyProp>>? DependencyForeignKeyExpression { get; private set; }

  internal Expression<Func<TDep, TSource?>>? SourceRefExpression { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithForeignKey(Expression<Func<TDep, TKeyProp>> expression)
  {
    ArgumentNullException.ThrowIfNull(expression, nameof(expression));
    FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
    this.DependencyForeignKeyExpression = expression;
    return this;
  }

  /// <inheritdoc/>>
  public void Apply()
  {
    if (this.SourceKeyExpression == null)
    {
      throw new InvalidOperationException("The Many to One relation is not setup properly. The Source Key must be defined using HasKey().");
    }

    if (this.DependencyForeignKeyExpression == null)
    {
      throw new InvalidOperationException("The Many to One relation is not setup properly. The dependency foreign key must be defined using WithForeignKey().");
    }

    if (this.Dependency == null || this.SourceRefExpression == null)
    {
      return;
    }

    foreach (var item in this.Dependency)
    {
      try
      {
        // Set Source reference on Dependency's item, beware that item could be null.
        // var sourceRef = this.SourceRefExpression.Compile().Invoke(item);
        var sourceKey = this.SourceKeyExpression.Compile().Invoke(this.Source);

        // TODO: Review why commented out, use case ??
        // var dependencyForeignKey = DependencyForeignKeyExpression.Compile().Invoke(item);

        // Set Dependency's item foreign key to Source key
        if (sourceKey != null)
        {
          // (sourceKey != null && dependencyForeignKey != null)
          FluentExpression.SetField(item, this.SourceRefExpression, this.Source);
          FluentExpression.SetField(item, this.DependencyForeignKeyExpression, sourceKey);
        }
        else
        {
          throw new InvalidOperationException("The Many to One relation is not setup properly. Please review the relation definition as well as the entity definition.");
        }
      }
      catch (Exception e)
      {
        // TODO: Review null item in collection, Is this an actual use case ? Does it make sense to allow null object in collection ?
        throw new InvalidOperationException("The Source can not be been defined due to dependency being null.", e);
      }
    }
  }
}
