// <copyright file="IFluentBogusRelationManyToOne.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  using System;
  using System.Linq.Expressions;

  public interface IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> : IFluentBogusRelation
    where TSource : class
    where TDep : class
  {
    Expression<Func<TDep, TKeyProp>>? DependencyForeignKeyExpression { get; }

    Expression<Func<TDep, TSource?>>? SourceRefExpression { get; }

    IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithForeignKey(Expression<Func<TDep, TKeyProp>> expression);
  }
}
