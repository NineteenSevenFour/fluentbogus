// <copyright file="FluentBogusRelationOneToOne{TSource,TDep,TKeyProp}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649
namespace NineteenSevenFour.Testing.FluentBogus.Relation;

using System;
using System.Linq.Expressions;
using NineteenSevenFour.Testing.Core;

public class FluentBogusRelationOneToOne<TSource, TDep, TKeyProp> : FluentBogusRelationOneToAny<TSource, TDep, TKeyProp>, IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp>
  where TSource : class
  where TDep : class
{
  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusRelationOneToOne{TSource, TDep, TKeyProp}"/> class.
  /// </summary>
  /// <param name="source">The instance of the source of the relation.</param>
  /// <param name="dependency">The instance of the dependency of the relation.</param>
  /// <param name="keyExpression"></param>
  /// <param name="foreignKeyExpression"></param>
  /// <param name="expression"></param>
  public FluentBogusRelationOneToOne(
    TSource source,
    TDep? dependency,
    Expression<Func<TSource, TKeyProp>>? keyExpression,
    Expression<Func<TSource, TKeyProp>>? foreignKeyExpression,
    Expression<Func<TDep, TSource?>> expression)
    : base(source, dependency, keyExpression, foreignKeyExpression)
  {
    ArgumentNullException.ThrowIfNull(expression, nameof(expression));
    FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
    this.SourceRefExpression = expression;
  }

  internal Expression<Func<TDep, TSource?>>? SourceRefExpression { get; private set; }

  internal Expression<Func<TDep, TKeyProp>>? WithForeignKeyExpression { get; private set; }

  internal Expression<Func<TDep, TKeyProp>>? WithKeyExpression { get; private set; }

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithForeignKey(Expression<Func<TDep, TKeyProp>> expression)
  {
    ArgumentNullException.ThrowIfNull(expression, nameof(expression));
    FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
    this.WithForeignKeyExpression = expression;
    return this;
  }

  /// <inheritdoc/>>
  public IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp> WithKey(Expression<Func<TDep, TKeyProp>> expression)
  {
    ArgumentNullException.ThrowIfNull(expression, nameof(expression));
    FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
    this.WithKeyExpression = expression;
    return this;
  }

  /// <inheritdoc/>>
  public void Apply()
  {
    if (this.Dependency == null || this.SourceRefExpression == null)
    {
      return;
    }

    var sourceRef = this.SourceRefExpression.Compile().Invoke(this.Dependency);

    if (sourceRef != null)
    {
      return;
    }

    if (this.KeyExpression != null && this.WithForeignKeyExpression != null)
    {
      var sourceKey = this.KeyExpression.Compile().Invoke(this.Source);
      var withForeignKey = this.WithForeignKeyExpression.Compile().Invoke(this.Dependency);

      if (sourceKey == null || withForeignKey == null)
      {
        return;
      }

      FluentExpression.SetField(this.Dependency, this.SourceRefExpression, this.Source);
      FluentExpression.SetField(this.Dependency, this.WithForeignKeyExpression, sourceKey);
    }
    else if (this.ForeignKeyExpression != null && this.WithKeyExpression != null)
    {
      var sourceForeignKey = this.ForeignKeyExpression.Compile().Invoke(this.Source);
      var withKey = this.WithKeyExpression.Compile().Invoke(this.Dependency);

      if (sourceForeignKey == null || withKey == null)
      {
        return;
      }

      FluentExpression.SetField(this.Dependency, this.SourceRefExpression, this.Source);
      FluentExpression.SetField(this.Source, this.ForeignKeyExpression, withKey);
    }
    else
    {
      if (this.KeyExpression != null && this.WithForeignKeyExpression != null)
      {
        if (this.KeyExpression != null)
        {
          throw new ArgumentNullException(nameof(this.KeyExpression), "The Source Key must be defined using HasKey().");
        }

        if (this.WithForeignKeyExpression != null)
        {
          throw new ArgumentNullException(nameof(this.WithForeignKeyExpression), "The dependency key must be defined using WithForeignKey().");
        }
      }
      else if (this.ForeignKeyExpression != null && this.WithKeyExpression != null)
      {
        if (this.ForeignKeyExpression != null)
        {
          throw new ArgumentNullException(nameof(this.ForeignKeyExpression), "The Source Foreign Key must be defined using HasForeignKey().");
        }

        if (this.WithKeyExpression != null)
        {
          throw new ArgumentNullException(nameof(this.WithKeyExpression), "The dependency key must be defined using WithKey().");
        }
      }

      throw new InvalidOperationException("The relation configuration is incorrect. Check the use of HasKey(), HasForeignKey(), WithKey(), HasForeignKey()");
    }
  }
}
