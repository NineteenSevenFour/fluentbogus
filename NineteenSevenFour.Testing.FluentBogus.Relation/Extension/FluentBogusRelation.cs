// <copyright file="FluentBogusRelation.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;

  public static class FluentBogusRelation
  {
    public static IFluentBogusRelationManyToAny<TSource, TDep> HasMany<TSource, TDep>(
      this TSource entity,
      Expression<Func<TSource, ICollection<TDep>?>> expression)
      where TSource : class
      where TDep : class => new FluentBogusRelation<TSource>(entity).HasMany(expression);

    public static IFluentBogusRelationOneToAny<TSource, TDep> HasOne<TSource, TDep>(
      this TSource entity,
      Expression<Func<TSource, TDep?>> expression)
      where TSource : class
      where TDep : class => new FluentBogusRelation<TSource>(entity).HasOne(expression);
  }
}
