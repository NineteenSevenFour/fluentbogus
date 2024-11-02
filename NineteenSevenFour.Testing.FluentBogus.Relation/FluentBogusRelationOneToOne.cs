// <copyright file="FluentBogusRelationOneToOne.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation
{
  using System;
  using System.Linq.Expressions;
  using NineteenSevenFour.Testing.Core;

  public class FluentBogusRelationOneToOne<TSource, TDep, TKeyProp> : FluentBogusRelationOneToAny<TSource, TDep, TKeyProp>, IFluentBogusRelationOneToOne<TSource, TDep, TKeyProp>
    where TSource : class
    where TDep : class
  {
    public FluentBogusRelationOneToOne(
      TSource source,
      TDep? dependency,
      Expression<Func<TSource, TKeyProp>>? sourceKeyExpression,
      Expression<Func<TSource, TKeyProp>>? sourceForeignKeyExpression,
      Expression<Func<TDep, TSource?>> expression)
      : base(source, dependency, sourceKeyExpression, sourceForeignKeyExpression)
    {
      ArgumentNullException.ThrowIfNull(expression, nameof(expression));
      FluentExpression.EnsureMemberExists<TDep>(FluentExpression.MemberNameFor(expression));
      this.SourceRefExpression = expression;
    }

    /// <inheritdoc/>>
    public Expression<Func<TDep, TSource?>>? SourceRefExpression { get; private set; }

    /// <inheritdoc/>>
    public Expression<Func<TDep, TKeyProp>>? WithForeignKeyExpression { get; private set; }

    /// <inheritdoc/>>
    public Expression<Func<TDep, TKeyProp>>? WithKeyExpression { get; private set; }

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

      if (this.SourceKeyExpression != null && this.WithForeignKeyExpression != null)
      {
        var sourceKey = this.SourceKeyExpression.Compile().Invoke(this.Source);
        var withForeignKey = this.WithForeignKeyExpression.Compile().Invoke(this.Dependency);

        if (sourceKey == null || withForeignKey == null)
        {
          return;
        }

        FluentExpression.SetField(this.Dependency, this.SourceRefExpression, this.Source);
        FluentExpression.SetField(this.Dependency, this.WithForeignKeyExpression, sourceKey);
      }
      else if (this.SourceForeignKeyExpression != null && this.WithKeyExpression != null)
      {
        var sourceForeignKey = this.SourceForeignKeyExpression.Compile().Invoke(this.Source);
        var withKey = this.WithKeyExpression.Compile().Invoke(this.Dependency);

        if (sourceForeignKey == null || withKey == null)
        {
          return;
        }

        FluentExpression.SetField(this.Dependency, this.SourceRefExpression, this.Source);
        FluentExpression.SetField(this.Source, this.SourceForeignKeyExpression, withKey);
      }
      else
      {
        if (this.SourceKeyExpression != null && this.WithForeignKeyExpression != null)
        {
          if (this.SourceKeyExpression != null)
          {
            throw new ArgumentNullException(nameof(this.SourceKeyExpression), "The Source Key must be defined using HasKey().");
          }

          if (this.WithForeignKeyExpression != null)
          {
            throw new ArgumentNullException(nameof(this.WithForeignKeyExpression), "The dependency key must be defined using WithForeignKey().");
          }
        }
        else if (this.SourceForeignKeyExpression != null && this.WithKeyExpression != null)
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

        throw new InvalidOperationException("The relation configuration is incorrect. Check the use of HasKey(), HasForeignKey(), WithKey(), HasForeignKey()");
      }
    }
  }
}
