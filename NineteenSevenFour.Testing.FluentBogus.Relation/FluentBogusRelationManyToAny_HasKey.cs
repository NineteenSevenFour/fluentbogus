// <copyright file="FluentBogusRelationManyToAny_HasKey.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;
  using NineteenSevenFour.Testing.Core;

  public class FluentBogusRelationManyToAny<TSource, TDep> : FluentBogusRelation<TSource>, IFluentBogusRelationManyToAny<TSource, TDep>
    where TSource : class
    where TDep : class
  {
    public FluentBogusRelationManyToAny(TSource source)
      : base(source)
    {
    }

    public FluentBogusRelationManyToAny(TSource source, Expression<Func<TSource, ICollection<TDep>?>> expression)
      : this(source)
    {
      ArgumentNullException.ThrowIfNull(expression, nameof(expression));
      FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(expression));
      this.Dependency = expression.Compile().Invoke(source);
    }

    public FluentBogusRelationManyToAny(TSource source, ICollection<TDep>? dependency)
      : this(source)
    {
      this.Dependency = dependency;
    }

    /// <inheritdoc/>>
    public ICollection<TDep>? Dependency { get; private set; }

    /// <inheritdoc/>>
    public IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp> HasKey<TKeyProp>(Expression<Func<TSource, TKeyProp>> expression) => new FluentBogusRelationManyToAny<TSource, TDep, TKeyProp>(this.Source, this.Dependency, expression);
  }
}
