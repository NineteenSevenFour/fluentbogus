// <copyright file="FluentBogusBuilder[TFaker].cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649
namespace NineteenSevenFour.Testing.FluentBogus
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;

  using AutoBogus;
  using AutoBogus.Moq;

  using NineteenSevenFour.Testing.Core;

  public class FluentBogusBuilder<TFaker, TEntity> : FluentBogusBuilder<TEntity>, IFluentBogusBuilder<TFaker, TEntity>
    where TFaker : AutoFaker<TEntity>, new()
    where TEntity : class
  {
    internal readonly List<string> SkipProperties = [];
    internal readonly List<string> RuleSets = [];

    internal string RuleSetString => string.Join(",", this.RuleSets);

    internal Action<IAutoGenerateConfigBuilder>? FakerConfigBuilder;
    internal object?[]? FakerArgs;
    internal int Seed;

    private readonly Dictionary<string, dynamic> rulesFor = [];
    private AutoFaker<TEntity>? faker;

    internal FluentBogusBuilder(FluentBogusBuilder<TFaker, TEntity> fluentBogus)
    {
      this.Seed = fluentBogus.Seed;
      this.FakerArgs = fluentBogus.FakerArgs;
      this.SkipProperties = fluentBogus.SkipProperties;
      this.RuleSets = fluentBogus.RuleSets;
      this.FakerConfigBuilder = fluentBogus.FakerConfigBuilder;
      this.rulesFor = fluentBogus.rulesFor;
    }

    public FluentBogusBuilder(params object?[]? args)
    {
      this.FakerArgs = args;
    }

    /// <inheritdoc/>>
    public ICollection<TEntity> Generate(int count)
    {
      this.EnsureFakerInternal();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
      return this.faker
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        .Configure(this.ConfigureFakerInternal)
        .UseSeed(this.Seed)
        .Generate(count, this.RuleSetString);
    }

    /// <inheritdoc/>>
    public TEntity Generate()
    {
      this.EnsureFakerInternal();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
      return this.faker
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        .Configure(this.ConfigureFakerInternal)
        .UseSeed(this.Seed)
        .Generate(this.RuleSetString);
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> Skip(params Expression<Func<TEntity, object?>>[] properties)
    {
      if (properties?.All(expr => expr == null) ?? true)
      {
        throw new ArgumentOutOfRangeException(nameof(properties), $"A List of properties must be provided.");
      }

      foreach (var property in properties)
      {
        this.SkipInternal(property);
      }

      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> Skip<TProperty>(Expression<Func<TEntity, TProperty>> prop)
    {
      this.SkipInternal(prop);
      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> UseRuleSet(string ruleset)
    {
      this.UseRuleSetInternal(ruleset);
      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> UseRuleSet(params string[] rulesets)
    {
      if (rulesets?.All(string.IsNullOrWhiteSpace) ?? true)
      {
        throw new ArgumentOutOfRangeException(nameof(rulesets), $"A List of ruleset must be provided.");
      }

      this.UseRuleSetInternal(rulesets);

      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> UseSeed(int seed)
    {
      this.Seed = seed;
      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> UseArgs(object?[]? args)
    {
      this.FakerArgs = args;
      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> UseConfig(Action<IAutoGenerateConfigBuilder> configBuilder)
    {
      this.FakerConfigBuilder = configBuilder;
      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> RuleFor<TProperty, TPropEntity, TPropFaker>(
      Expression<Func<TEntity, TProperty>> prop,
      IFluentBogusBuilder<TPropFaker, TPropEntity>? builder,
      int count)
      where TProperty : ICollection<TPropEntity?>?
      where TPropEntity : class
      where TPropFaker : AutoFaker<TPropEntity>, new()
    {
      var propOrFieldName = FluentExpression.MemberNameFor(prop);
      FluentExpression.EnsureMemberExists<TEntity>(propOrFieldName);

      if (builder == null)
      {
        this.Skip(e => prop);
      }
      else
      {
        this.rulesFor.Add(propOrFieldName, builder.Generate(count));
      }

      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> RuleFor<TProperty, TPropFaker>(
      Expression<Func<TEntity, TProperty?>> prop,
      IFluentBogusBuilder<TPropFaker, TProperty>? builder)
      where TProperty : class
      where TPropFaker : AutoFaker<TProperty>, new()
    {
      var propOrFieldName = FluentExpression.MemberNameFor(prop);
      FluentExpression.EnsureMemberExists<TEntity>(propOrFieldName);

      if (builder == null)
      {
        this.Skip(e => prop);
      }
      else
      {
        this.rulesFor.Add(propOrFieldName, builder.Generate());
      }

      return this;
    }

    private void SkipInternal<TProperty>(Expression<Func<TEntity, TProperty>> expr)
    {
      var propertyOrFieldName = FluentExpression.MemberNameFor(expr);
      FluentExpression.EnsureMemberExists<TEntity>(propertyOrFieldName);

      if (!this.SkipProperties.Contains(propertyOrFieldName))
      {
        this.SkipProperties.Add(propertyOrFieldName);
      }
      else
      {
        throw new InvalidOperationException($"The property {propertyOrFieldName} for type {typeof(TEntity).Name} is already set to be skipped.");
      }
    }

    private void UseRuleSetInternal(IEnumerable<string> ruleSets)
    {
      foreach (var ruleSet in ruleSets)
      {
        this.UseRuleSetInternal(ruleSet);
      }
    }

    private void UseRuleSetInternal(string ruleset)
    {
      // TODO: Validate RuleSet against TFaker
      if (string.IsNullOrWhiteSpace(ruleset))
      {
        throw new ArgumentOutOfRangeException(nameof(ruleset), $"A ruleset must be provided.");
      }

      if (!this.RuleSets.Contains(ruleset))
      {
        this.RuleSets.Add(ruleset);
      }
      else
      {
        throw new InvalidOperationException($"The ruleset {ruleset} is already set to be used.");
      }
    }

    private void ConfigureFakerInternal(IAutoGenerateConfigBuilder builder)
    {
      // Call user define Faker configuration overrides
      this.FakerConfigBuilder?.Invoke(builder);

      // Force
      builder.WithBinder<MoqBinder>();  // Configures the bind

      // Configures members to be skipped for a typeer to use
      foreach (var propName in this.SkipProperties)
      {
        builder.WithSkip<TEntity>(propName);
      }
    }

    private void EnsureFakerInternal()
    {
      this.faker = Activator.CreateInstance(typeof(TFaker), this.FakerArgs) as AutoFaker<TEntity>;
    }
  }
}
