// <copyright file="FluentBogusRelationManyToOne.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;
  using NineteenSevenFour.Testing.Core;

  public class FluentBogusRelationManyToOne<TSource, TDep, TKeyProp> : FluentBogusRelationManyToAny<TSource, TDep, TKeyProp>, IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp>
    where TSource : class
    where TDep : class
  {
    public FluentBogusRelationManyToOne(
      TSource source,
      ICollection<TDep>? dependency,
      Expression<Func<TSource, TKeyProp>>? sourceKeyExpression)
      : base(source, dependency, sourceKeyExpression)
    {
    }

    public FluentBogusRelationManyToOne(
      TSource source,
      ICollection<TDep>? dependency,
      Expression<Func<TSource, TKeyProp>>? sourceKeyExpression,
      Expression<Func<TDep, TSource?>>? withOneExpression)
      : this(source, dependency, sourceKeyExpression)
    {
      ArgumentNullException.ThrowIfNull(withOneExpression, nameof(withOneExpression));
      FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(withOneExpression));
      this.SourceRefExpression = withOneExpression;
    }

    /// <inheritdoc/>>
    public Expression<Func<TDep, TKeyProp>>? DependencyForeignKeyExpression { get; private set; }

    /// <inheritdoc/>>
    public Expression<Func<TDep, TSource?>>? SourceRefExpression { get; private set; }

    /// <inheritdoc/>>
    public IFluentBogusRelationManyToOne<TSource, TDep, TKeyProp> WithForeignKey(Expression<Func<TDep, TKeyProp>> expression)
    {
      ArgumentNullException.ThrowIfNull(expression, nameof(expression));
      FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
      this.DependencyForeignKeyExpression = expression;
      return this;
    }

    /// <inheritdoc/>>
    public void Apply()
    {
      if (this.SourceKeyExpression == null)
      {
        throw new InvalidOperationException("The Many to One relation is not setup properly. The Source Key must be defined using HasKey().");
      }

      if (this.DependencyForeignKeyExpression == null)
      {
        throw new InvalidOperationException("The Many to One relation is not setup properly. The dependency foreign key must be defined using WithForeignKey().");
      }

      if (this.Dependency == null || this.SourceRefExpression == null)
      {
        return;
      }

      foreach (var item in this.Dependency)
      {
        // Set Source reference on Dependency's item
        var sourceRef = this.SourceRefExpression.Compile().Invoke(item);
        if (sourceRef == null)
        {
          var sourceKey = this.SourceKeyExpression.Compile().Invoke(this.Source);

          // var dependencyForeignKey = DependencyForeignKeyExpression.Compile().Invoke(item);

          // Set Dependency's item foreign key to Source key
          if (sourceKey != null) // (sourceKey != null && dependencyForeignKey != null)
          {
            FluentExpression.SetField(item, this.SourceRefExpression, this.Source);
            FluentExpression.SetField(item, this.DependencyForeignKeyExpression, sourceKey);
          }
          else
          {
            // TODO: Is this an actual use case ?
            throw new InvalidOperationException("The Many to One relation is not setup properly. Please review the relation definition as well as the entity definition.");
          }
        }
        else
        {
          // TODO: Is this an actual use case ?
          throw new ArgumentNullException(nameof(this.SourceKeyExpression), "The Source has already been defined.");
        }
      }
    }
  }
}
