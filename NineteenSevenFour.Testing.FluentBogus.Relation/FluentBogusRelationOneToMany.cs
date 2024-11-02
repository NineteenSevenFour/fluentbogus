// <copyright file="FluentBogusRelationOneToMany.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;
  using NineteenSevenFour.Testing.Core;

  public class FluentBogusRelationOneToMany<TSource, TDep, TKeyProp> : FluentBogusRelationOneToAny<TSource, TDep, TKeyProp>, IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp>
    where TSource : class
    where TDep : class
  {
    public FluentBogusRelationOneToMany(
      TSource source,
      TDep? dependency,
      Expression<Func<TSource, TKeyProp>>? sourceKeyExpression,
      Expression<Func<TSource, TKeyProp>>? sourceForeignKeyExpression,
      Expression<Func<TDep, ICollection<TSource?>?>> sourceRefExpression)
      : base(source, dependency, sourceKeyExpression, sourceForeignKeyExpression)
    {
      ArgumentNullException.ThrowIfNull(sourceRefExpression, nameof(sourceRefExpression));
      FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(sourceRefExpression));
      this.SourceRefExpression = sourceRefExpression;
    }

    /// <inheritdoc/>>
    public Expression<Func<TDep, ICollection<TSource?>?>>? SourceRefExpression { get; private set; }

    /// <inheritdoc/>>
    public Expression<Func<TDep, TKeyProp>>? WithKeyExpression { get; private set; }

    /// <inheritdoc/>>
    public IFluentBogusRelationOneToMany<TSource, TDep, TKeyProp> WithKey(Expression<Func<TDep, TKeyProp>> expression)
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

      sourceRef ??= default;
      if ((sourceRef?.Count ?? 0) == 0)
      {
        return;
      }

      if (this.SourceForeignKeyExpression != null && this.WithKeyExpression != null)
      {
        var sourceForeignKey = this.SourceForeignKeyExpression.Compile().Invoke(this.Source);
        var withKey = this.WithKeyExpression.Compile().Invoke(this.Dependency);

        if (sourceForeignKey == null || withKey == null)
        {
          return;
        }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        sourceRef.Add(this.Source);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        FluentExpression.SetField(this.Dependency, this.SourceRefExpression, sourceRef);
        FluentExpression.SetField(this.Source, this.SourceForeignKeyExpression, withKey);
      }
      else
      {
        if (this.SourceForeignKeyExpression != null)
        {
          throw new ArgumentNullException(nameof(this.SourceForeignKeyExpression), "The Source Foreign Key must be defined using HasForeignKey().");
        }

        if (this.WithKeyExpression != null)
        {
          throw new ArgumentNullException(nameof(this.WithKeyExpression), "The dependency key must be defined using WithKey().");
        }
      }
    }
  }
}
