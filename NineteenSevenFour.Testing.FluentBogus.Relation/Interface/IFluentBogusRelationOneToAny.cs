// <copyright file="IFluentBogusRelationOneToAny.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;

  public interface IFluentBogusRelationOneToAny<TSource, TDep, TKeyProp>
    where TSource : class
    where TDep : class
  {
    Expression<Func<TSource, TKeyProp>>? SourceKeyExpression { get; }

    Expression<Func<TSource, TKeyProp>>? SourceForeignKeyExpression { get; }

    IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> WithMany(Expression<Func<TDep, ICollection<TSource?>?>> expression);

    IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithOne(Expression<Func<TDep, TSource?>> expression);
  }
}
