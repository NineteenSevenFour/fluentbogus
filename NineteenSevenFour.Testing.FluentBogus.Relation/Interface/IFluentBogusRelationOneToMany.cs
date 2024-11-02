// <copyright file="IFluentBogusRelationOneToMany.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;

  public interface IFluentBogusRelationOneToAny<TSource, TDep>
    where TSource : class
    where TDep : class
  {
    TDep? Dependency { get; }

    IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp> HasKey<TKeyProp>(Expression<Func<TSource, TKeyProp>> expression);

    IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp> HasForeignKey<TKeyProp>(Expression<Func<TSource, TKeyProp>> expression);
  }

  public interface IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> : IFluentBogusRelation
    where TSource : class
    where TDep : class
  {
    Expression<Func<TDep, ICollection<TSource?>?>>? SourceRefExpression { get; }

    Expression<Func<TDep, TKeyProp>>? WithKeyExpression { get; }

    IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> WithKey(Expression<Func<TDep, TKeyProp>> expression);
  }
}
