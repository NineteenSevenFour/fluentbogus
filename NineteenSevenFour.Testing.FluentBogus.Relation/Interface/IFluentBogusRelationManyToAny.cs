// <copyright file="IFluentBogusRelationManyToAny.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;

  public interface IFluentBogusRelationManyToAny<TSource, TDep>
    where TSource : class
    where TDep : class
  {
    ICollection<TDep>? Dependency { get; }

    IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp> HasKey<TKeyProp>(Expression<Func<TSource, TKeyProp>> expression);
  }

  public interface IFluentBogusRelationManyToAny<TSource, TDep, TKeyProp>
    where TSource : class
    where TDep : class
  {
    Expression<Func<TSource, TKeyProp>>? SourceKeyExpression { get; }

    IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression);
  }
}
