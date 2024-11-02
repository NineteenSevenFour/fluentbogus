// <copyright file="FluentBogusRelation.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;

  public class FluentBogusRelation<TSource>(TSource source) : IFluentBogusRelation<TSource>
    where TSource : class
  {
    public TSource Source => source;

    /// <inheritdoc/>>
    public IFluentBogusRelationManyToAny<TSource, TDep> HasMany<TDep>(Expression<Func<TSource, ICollection<TDep>?>> expression)
      where TDep : class => new FluentBogusRelationManyToAny<TSource, TDep>(this.Source, expression);

    /// <inheritdoc/>>
    public IFluentBogusRelationOneToAny<TSource, TDep> HasOne<TDep>(Expression<Func<TSource, TDep?>> expression)
      where TDep : class => new FluentBogusRelationOneToAny<TSource, TDep>(this.Source, expression);
  }
}
