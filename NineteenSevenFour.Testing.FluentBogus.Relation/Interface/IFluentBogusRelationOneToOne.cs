// <copyright file="IFluentBogusRelationOneToOne.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  using System;
  using System.Linq.Expressions;

  public interface IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> : IFluentBogusRelation
    where TSource : class
    where TDep : class
  {
    Expression<Func<TDep, TSource?>>? SourceRefExpression { get; }

    Expression<Func<TDep, TKeyProp>>? WithForeignKeyExpression { get; }

    Expression<Func<TDep, TKeyProp>>? WithKeyExpression { get; }

    IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithKey(Expression<Func<TDep, TKeyProp>> expression);

    IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithForeignKey(Expression<Func<TDep, TKeyProp>> expression);
  }
}
