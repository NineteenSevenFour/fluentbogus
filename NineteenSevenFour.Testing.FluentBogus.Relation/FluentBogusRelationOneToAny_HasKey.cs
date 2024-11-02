// <copyright file="FluentBogusRelationOneToAny.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  using System;
  using System.Linq.Expressions;
  using NineteenSevenFour.Testing.Core;

  public class FluentBogusRelationOneToAny<TSource, TDep> : FluentBogusRelation<TSource>, IFluentBogusRelationOneToAny<TSource, TDep>
    where TSource : class
    where TDep : class
  {
    public FluentBogusRelationOneToAny(TSource source)
      : base(source)
    {
    }

    public FluentBogusRelationOneToAny(TSource source, TDep? dependency)
      : this(source)
    {
      this.Dependency = dependency;
    }

    public FluentBogusRelationOneToAny(TSource source, Expression<Func<TSource, TDep?>> expression)
      : this(source)
    {
      ArgumentNullException.ThrowIfNull(expression, nameof(expression));
      FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(expression));
      this.Dependency = expression.Compile().Invoke(source);
    }

    /// <inheritdoc/>>
    public TDep? Dependency { get; private set; }

    /// <inheritdoc/>>
    public IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp> HasForeignKey<TKeyProp>(Expression<Func<TSource, TKeyProp>> expression) => new FluentBogusRelationOneToAny<TSource, TDep, TKeyProp>(this.Source, this.Dependency, null, expression);

    /// <inheritdoc/>>
    public IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp> HasKey<TKeyProp>(Expression<Func<TSource, TKeyProp>> expression) => new FluentBogusRelationOneToAny<TSource, TDep, TKeyProp>(this.Source, this.Dependency, expression, null);
  }
}
