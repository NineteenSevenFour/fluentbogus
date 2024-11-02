// <copyright file="IFluentBogusRelation.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;

  public interface IFluentBogusRelation
  {
    void Apply();
  }

  public interface IFluentBogusRelation<TSource>
    where TSource : class
  {
    TSource Source { get; }

    IFluentBogusRelationManyToAny<TSource, TDep> HasMany<TDep>(Expression<Func<TSource, ICollection<TDep>?>> expression)
      where TDep : class;

    IFluentBogusRelationOneToAny<TSource, TDep> HasOne<TDep>(Expression<Func<TSource, TDep?>> expression)
      where TDep : class;
  }
}
