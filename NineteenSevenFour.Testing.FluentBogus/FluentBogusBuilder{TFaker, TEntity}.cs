// <copyright file="FluentBogusBuilder{TFaker, TEntity}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649
namespace NineteenSevenFour.Testing.FluentBogus;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using AutoBogus;
using AutoBogus.Moq;

using NineteenSevenFour.Testing.Core;

/// <inheritdoc cref="IFluentBogusBuilder{TFaker, TEntity}"/>>
public class FluentBogusBuilder<TFaker, TEntity> : FluentBogusBuilder<TEntity>, IFluentBogusBuilder<TFaker, TEntity>
  where TFaker : AutoFaker<TEntity>, new()
  where TEntity : class
{
#pragma warning disable SA1401
#pragma warning disable SA1600
#if NET8_0_OR_GREATER
  internal readonly List<string> SkipProperties = [];
  internal readonly List<string> RuleSets = [];
  internal readonly Dictionary<string, dynamic> RulesFor = [];
#else
  internal readonly List<string> SkipProperties = new();
  internal readonly List<string> RuleSets = new();
  internal readonly Dictionary<string, dynamic> RulesFor = new();
#endif

  internal Action<IAutoGenerateConfigBuilder>? FakerConfigBuilder;
  internal object?[]? FakerArgs;
  internal int Seed;

  internal AutoFaker<TEntity>? Faker;
#pragma warning restore SA1600
#pragma warning restore SA1401

  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusBuilder{TFaker, TEntity}"/> class.
  /// </summary>
  /// <param name="args">The optional arguments to provide to the <see cref="AutoFaker"/>.</param>
  public FluentBogusBuilder(params object?[]? args)
  {
    this.FakerArgs = args;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="FluentBogusBuilder{TFaker, TEntity}"/> class.
  /// </summary>
  /// <param name="fluentBogus">The instance of <see cref="FluentBogusBuilder{TFaker, TEntity}"/> to clone.</param>
  internal FluentBogusBuilder(FluentBogusBuilder<TFaker, TEntity> fluentBogus)
  {
    this.Seed = fluentBogus.Seed;
    this.FakerArgs = fluentBogus.FakerArgs;
    this.SkipProperties = fluentBogus.SkipProperties;
    this.RuleSets = fluentBogus.RuleSets;
    this.FakerConfigBuilder = fluentBogus.FakerConfigBuilder;
    this.RulesFor = fluentBogus.RulesFor;
  }

  /// <summary>
  /// Gets the RuleSets as a semicolon separated list.
  /// </summary>
  internal string RuleSetString => string.Join(",", this.RuleSets);

  /// <inheritdoc/>>
  public ICollection<TEntity> Generate(int count)
  {
    this.EnsureFaker();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    return this.Faker
#pragma warning restore CS8602 // Dereference of a possibly null reference.
      .Configure(this.ConfigureFaker)
      .UseSeed(this.Seed)
      .Generate(count, this.RuleSetString);
  }

  /// <inheritdoc/>>
  public TEntity Generate()
  {
    this.EnsureFaker();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    return this.Faker
#pragma warning restore CS8602 // Dereference of a possibly null reference.
      .Configure(this.ConfigureFaker)
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
      this.SkipProperty(property);
    }

    return this;
  }

  /// <inheritdoc/>>
  public IFluentBogusBuilder<TFaker, TEntity> Skip<TProperty>(Expression<Func<TEntity, TProperty>> prop)
  {
    this.SkipProperty(prop);
    return this;
  }

  /// <inheritdoc/>>
  public IFluentBogusBuilder<TFaker, TEntity> UseRuleSet(string ruleset)
  {
    this.AddUseRuleSet(ruleset);
    return this;
  }

  /// <inheritdoc/>>
  public IFluentBogusBuilder<TFaker, TEntity> UseRuleSet(params string[] rulesets)
  {
    if (rulesets?.All(string.IsNullOrWhiteSpace) ?? true)
    {
      throw new ArgumentOutOfRangeException(nameof(rulesets), $"A List of ruleset must be provided.");
    }

    this.AddUseRuleSet(rulesets);

    return this;
  }

  /// <inheritdoc/>>
  public IFluentBogusBuilder<TFaker, TEntity> UseSeed(int seed)
  {
    this.Seed = seed;
    return this;
  }

  /// <inheritdoc/>>
  public IFluentBogusBuilder<TFaker, TEntity> UseFakerConfig(Action<IAutoGenerateConfigBuilder> configBuilder)
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
      this.RulesFor.Add(propOrFieldName, builder.Generate(count));
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
      this.RulesFor.Add(propOrFieldName, builder.Generate());
    }

    return this;
  }

  private void SkipProperty<TProperty>(Expression<Func<TEntity, TProperty>> expr)
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

  private void AddUseRuleSet(IEnumerable<string> ruleSets)
  {
    foreach (var ruleSet in ruleSets)
    {
      this.AddUseRuleSet(ruleSet);
    }
  }

  private void AddUseRuleSet(string ruleset)
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

  private void ConfigureFaker(IAutoGenerateConfigBuilder builder)
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

  private void EnsureFaker()
  {
    this.Faker = Activator.CreateInstance(typeof(TFaker), this.FakerArgs) as AutoFaker<TEntity>;
  }
}
