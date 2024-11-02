// <copyright file="FluentBogusRelationManyToAny_WithOne.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;
  using NineteenSevenFour.Testing.Core;

  public class FluentBogusRelationManyToAny<TSource, TDep, TKeyProp> : FluentBogusRelationManyToAny<TSource, TDep>, IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp>
    where TSource : class
    where TDep : class
  {
    public FluentBogusRelationManyToAny(TSource source, ICollection<TDep>? dependency)
      : base(source, dependency)
    {
    }

    public FluentBogusRelationManyToAny(TSource source, ICollection<TDep>? dependency, Expression<Func<TSource, TKeyProp>>? sourceKeyExpression)
      : this(source, dependency)
    {
      ArgumentNullException.ThrowIfNull(sourceKeyExpression, nameof(sourceKeyExpression));
      FluentExpression.EnsureMemberExists<TSource>(FluentExpression.MemberNameFor(sourceKeyExpression));
      this.SourceKeyExpression = sourceKeyExpression;
    }

    /// <inheritdoc/>>
    public Expression<Func<TSource, TKeyProp>>? SourceKeyExpression { get; private set; }

    /// <inheritdoc/>>
    public IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression) => new FluentBogusRelationManyToOne<TSource, TDep, TKeyProp>(this.Source, this.Dependency, this.SourceKeyExpression, expression);
  }
}
