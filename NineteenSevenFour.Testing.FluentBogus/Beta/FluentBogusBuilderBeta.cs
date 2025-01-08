// <copyright file="FluentBogusBuilderBeta.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

// ReSharper disable RedundantNameQualifier
#pragma warning disable SA1600
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1402
#pragma warning disable SA1649
namespace NineteenSevenFour.Testing.FluentBogus.Beta;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using AutoBogus;
using AutoBogus.Moq;

using NineteenSevenFour.Testing.Core;

/// <summary>
/// Interface that provides means of configuring a <see cref="FluentBogusBuilder{TEntity}"/>.
/// </summary>
/// <typeparam name="TEntity">The type of the entity that will be faked.</typeparam>
public interface IFluentBogusBuilderOptionBeta<TEntity>
  where TEntity : class
{
  /// <summary>
  /// The seed value to use in order to consistently generates data.
  /// </summary>
  /// <param name="seed">The seed value.</param>
  /// <returns>The <see cref="IFluentBogusBuilderOptionBeta{TEntity}"/> instance.</returns>
  IFluentBogusBuilderOptionBeta<TEntity> UseSeed(int seed);

  /// <summary>
  /// Initialize an <see cref="IFluentBogusBuilderOptionBeta{TEntity}"/> using a default <see cref="AutoFaker{TEntity}"/>.
  /// </summary>
  /// <param name="configBuilder">The <see cref="AutoFaker"/> builder.</param>
  /// <param name="args">The array of argument to pass to the <see cref="AutoFaker{TEntity}"/> instance.</param>
  /// <returns>An <see cref="IFluentBogusBuilderOptionBeta{TEntity}"/> instance.</returns>
  IFluentBogusBuilderOptionBeta<TEntity> UseFaker(Action<IAutoGenerateConfigBuilder> configBuilder, params object?[]? args);

  /// <summary>
  /// Initialize an <see cref="IFluentBogusBuilderOptionBeta{TEntity}"/> using a default <see cref="AutoFaker{TEntity}"/>.
  /// </summary>
  /// <param name="args">The array of argument to pass to the <see cref="AutoFaker{TEntity}"/> instance.</param>
  /// <returns>An <see cref="IFluentBogusBuilderOptionBeta{TEntity}"/> instance.</returns>
  IFluentBogusBuilderOptionBeta<TEntity> UseFaker(params object?[]? args);

  /// <summary>
  /// Initialize an <see cref="IFluentBogusBuilderOptionBeta{TEntity}"/> using a custom <see cref="AutoFaker{TEntity}"/>.
  /// </summary>
  /// <typeparam name="TFaker">The custom <see cref="AutoFaker{TEntity}"/> type.</typeparam>
  /// <param name="configBuilder">The <see cref="AutoFaker"/> builder.</param>
  /// <param name="args">The array of argument to pass to the <see cref="AutoFaker{TEntity}"/> instance.</param>
  /// <returns>An <see cref="IFluentBogusBuilderOptionBeta{TEntity}"/> instance.</returns>
  IFluentBogusBuilderOptionBeta<TEntity> UseFaker<TFaker>(Action<IAutoGenerateConfigBuilder> configBuilder, params object?[]? args)
    where TFaker : AutoFaker<TEntity>, new();

  /// <summary>
  /// Initialize an <see cref="IFluentBogusBuilderOptionBeta{TEntity}"/> using a custom <see cref="AutoFaker{TEntity}"/>.
  /// </summary>
  /// <typeparam name="TFaker">The custom <see cref="AutoFaker{TEntity}"/> type.</typeparam>
  /// <param name="args">The array of argument to pass to the <see cref="AutoFaker{TEntity}"/> instance.</param>
  /// <returns>An <see cref="IFluentBogusBuilderOptionBeta{TEntity}"/> instance.</returns>
  IFluentBogusBuilderOptionBeta<TEntity> UseFaker<TFaker>(params object?[]? args)
    where TFaker : AutoFaker<TEntity>, new();

  /// <summary>
  /// Register an <see cref="AutoFaker"/> ruleset.
  /// </summary>
  /// <param name="ruleset">The name of the ruleset.</param>
  /// <returns>The <see cref="IFluentBogusBuilderOptionBeta{TEntity}"/> instance.</returns>
  IFluentBogusBuilderOptionBeta<TEntity> UseRuleSet(string ruleset);

  /// <summary>
  /// Register an array of <see cref="AutoFaker"/> ruleset.
  /// </summary>
  /// <param name="rulesets">An array of ruleset name.</param>
  /// <returns>The <see cref="IFluentBogusBuilderOptionBeta{TEntity}"/> instance.</returns>
  IFluentBogusBuilderOptionBeta<TEntity> UseRuleSet(params string[] rulesets);

  /// <summary>
  /// Register an array of lambda expression to skip associated property during data generation.
  /// </summary>
  /// <param name="propExpressions">The lambda expressions describing the properties of the <typeparamref name="TEntity"/>.</param>
  /// <returns>The <see cref="IFluentBogusBuilderOptionBeta{TEntity}"/> instance.</returns>
  IFluentBogusBuilderOptionBeta<TEntity> Skip(params Expression<Func<TEntity, object?>>[] propExpressions);

  /// <summary>
  /// Register a lambda expression to skip a property during data generation.
  /// </summary>
  /// <typeparam name="TProperty">The type of the property belonging to the <typeparamref name="TEntity"/>.</typeparam>
  /// <param name="propExpr">The lambda expression describing the property of the <typeparamref name="TEntity"/>.</param>
  /// <returns>The <see cref="IFluentBogusBuilderOptionBeta{TEntity}"/> instance.</returns>
  IFluentBogusBuilderOptionBeta<TEntity> Skip<TProperty>(Expression<Func<TEntity, TProperty?>> propExpr);

  /// <summary>
  /// Defines a rule to use to generate fake data for a specific collection member.
  /// </summary>
  /// <typeparam name="TProperty">The type of the property belonging to the <typeparamref name="TEntity"/>.</typeparam>
  /// <typeparam name="TPropEntity">The type of the underlying object of the <typeparamref name="TProperty"/> property belonging to the <typeparamref name="TEntity"/>.</typeparam>
  /// <param name="propExpr">The lambda expression describing the property of the <typeparamref name="TEntity"/>.</param>
  /// <param name="builder">The <see cref="FluentBogusBuilderBeta{PropTEntity}"/> instance using the <see cref="AutoFaker{TPropEntity}"/>.</param>
  /// <param name="count">The number of <typeparamref name="TPropEntity"/> to generates.</param>
  /// <returns>The <see cref="IFluentBogusBuilderOptionBeta{TEntity}"/> instance.</returns>
  IFluentBogusBuilderOptionBeta<TEntity> UseRuleFor<TProperty, TPropEntity>(
    Expression<Func<TEntity, TProperty>> propExpr,
    FluentBogusBuilderBeta<TPropEntity>? builder,
    int count)
    where TPropEntity : class
    where TProperty : ICollection<TPropEntity>?;

  /// <summary>
  /// Defines a rule to generate fake data for a specific member.
  /// </summary>
  /// <typeparam name="TProperty">The type of the property belonging to the <typeparamref name="TEntity"/>.</typeparam>
  /// <typeparam name="TPropEntity">The type of the underlying object of the <typeparamref name="TProperty"/> property belonging to the <typeparamref name="TEntity"/>.</typeparam>
  /// <param name="propExpr">The lambda expression describing the property of the <typeparamref name="TEntity"/>.</param>
  /// <param name="builder">The <see cref="FluentBogusBuilderBeta{PropTEntity}"/> instance using the <see cref="AutoFaker{TPropEntity}"/>.</param>
  /// <returns>The <see cref="IFluentBogusBuilderOptionBeta{TEntity}"/> instance.</returns>
  IFluentBogusBuilderOptionBeta<TEntity> UseRuleFor<TProperty, TPropEntity>(
    Expression<Func<TEntity, TProperty>> propExpr,
    FluentBogusBuilderBeta<TPropEntity>? builder)
    where TPropEntity : class
    where TProperty : TPropEntity?;
}

/// <summary>
/// Interface providing the means to generates instance or instances of an entity.
/// </summary>
/// <typeparam name="TEntity">The type of the entity that will be faked.</typeparam>
public interface IFluentBogusGeneratorBeta<TEntity>
  where TEntity : class
{
  /// <summary>
  /// Generates a collection of <typeparamref name="TEntity"/>.
  /// </summary>
  /// <param name="count">The number of <typeparamref name="TEntity"/> to generates.</param>
  /// <returns>The <see cref="ICollection{TEntity}"/> instance.</returns>
  ICollection<TEntity> Generate(int count);

  /// <summary>
  /// Generates a single <typeparamref name="TEntity"/>.
  /// </summary>
  /// <returns>A <typeparamref name="TEntity"/>.</returns>
  TEntity Generate();
}

/// <summary>
/// Interface providing means to create a <see cref="FluentBogusGeneratorBeta{TEntity}"/> using configured <see cref="FluentBogusBuilderOptionBeta{TEntity}"/>.
/// </summary>
/// <typeparam name="TEntity">The type of the entity that will be faked.</typeparam>
public interface IFluentBogusBuilderBeta<TEntity>
  where TEntity : class
{
  /// <summary>
  /// Create a <see cref="FluentBogusGeneratorBeta{TEntity}"/> using the <see cref="FluentBogusBuilderOptionBeta{TEntity}"/> from the <see cref="FluentBogusBuilder{TEntity}"/>.
  /// </summary>
  /// <returns>A configured instance of <see cref="FluentBogusGeneratorBeta{TEntity}"/>.</returns>
  IFluentBogusGeneratorBeta<TEntity> Build();
}

public class RuleForModel
{
  public RuleForModel(IFluentBogusBuilderBeta<object> builder, int genCounter = 1)
  {
    this.Builder = builder;
    this.GenCounter = genCounter;
  }

  public IFluentBogusBuilderBeta<object> Builder { get; set; }

  public int GenCounter { get; set; }
}

public class FluentBogusBuilderOptionBeta<TEntity> : IFluentBogusBuilderOptionBeta<TEntity>
  where TEntity : class
{
  internal AutoFaker<TEntity>? Faker { get; set; }

  internal Type? FakerType { get; set; }

  internal Action<IAutoGenerateConfigBuilder>? FakerConfiguration { get; set; }

  internal object?[]? FakerArgs { get; set; }

#if NET8_0_OR_GREATER
  internal Dictionary<string, dynamic>? RulesFor { get; set; }
#else
  internal Dictionary<string, RuleForModel>? RulesFor { get; set; }
#endif

  internal List<string>? RuleSets { get; set; }

  /// <summary>
  /// Gets the RuleSets as a semicolon separated list.
  /// </summary>
  internal string RuleSetString => this.RuleSets?.Count <= 0 ? string.Empty : string.Join(",", this.RuleSets);

  internal int Seed { get; set; }

  internal List<string>? SkipProperties { get; set; }

  /// <inheritdoc />
  public IFluentBogusBuilderOptionBeta<TEntity> UseFaker(Action<IAutoGenerateConfigBuilder> configBuilder, params object?[]? args)
  {
    this.FakerConfiguration = configBuilder;
    this.FakerArgs = args;
    return this;
  }

  /// <inheritdoc />
  public IFluentBogusBuilderOptionBeta<TEntity> UseFaker(params object?[]? args)
  {
    this.FakerArgs = args;
    return this;
  }

  /// <inheritdoc />
  public IFluentBogusBuilderOptionBeta<TEntity> UseFaker<TFaker>(Action<IAutoGenerateConfigBuilder> configBuilder, params object?[]? args)
    where TFaker : AutoFaker<TEntity>, new()
  {
    this.FakerConfiguration = configBuilder;
    this.FakerArgs = args;
    this.FakerType = typeof(TFaker);
    return this;
  }

  /// <inheritdoc />
  public IFluentBogusBuilderOptionBeta<TEntity> UseFaker<TFaker>(params object?[]? args)
    where TFaker : AutoFaker<TEntity>, new()
  {
    this.FakerArgs = args;
    this.FakerType = typeof(TFaker);
    return this;
  }

  /// <inheritdoc />
  public IFluentBogusBuilderOptionBeta<TEntity> UseRuleSet(string ruleset)
  {
    if (string.IsNullOrWhiteSpace(ruleset))
    {
      throw new ArgumentOutOfRangeException(nameof(ruleset), $"A ruleset must be provided.");
    }

    if (!this.RuleSets?.Contains(ruleset) ?? true)
    {
#if NET8_0_OR_GREATER
      this.RuleSets ??= [];
#else
      this.RuleSets ??= new List<string>();
#endif

      this.RuleSets.Add(ruleset);
    }
    else
    {
      throw new InvalidOperationException($"The ruleset {ruleset} is already set to be used.");
    }

    return this;
  }

  /// <inheritdoc />
  public IFluentBogusBuilderOptionBeta<TEntity> UseRuleSet(params string[] rulesets)
  {
    foreach (var ruleset in rulesets)
    {
      this.UseRuleSet(ruleset);
    }

    return this;
  }

  /// <inheritdoc />
  public IFluentBogusBuilderOptionBeta<TEntity> UseSeed(int seed)
  {
    this.Seed = seed;
    return this;
  }

  /// <inheritdoc />
  public IFluentBogusBuilderOptionBeta<TEntity> Skip<TProperty>(Expression<Func<TEntity, TProperty?>> propExpr)
  {
    this.SkipProperty(propExpr);

    return this;
  }

  /// <inheritdoc />
  public IFluentBogusBuilderOptionBeta<TEntity> Skip(params Expression<Func<TEntity, object?>>[] propExpressions)
  {
    foreach (var propExpr in propExpressions)
    {
      this.SkipProperty(propExpr);
    }

    return this;
  }

  /// <inheritdoc />
  public IFluentBogusBuilderOptionBeta<TEntity> UseRuleFor<TProperty, TPropEntity>(
    Expression<Func<TEntity, TProperty>> propExpr,
    FluentBogusBuilderBeta<TPropEntity>? builder,
    int count)
    where TProperty : ICollection<TPropEntity>?
    where TPropEntity : class
  {
    var propOrFieldName = FluentExpression.MemberNameFor(propExpr);
    FluentExpression.EnsureMemberExists<TEntity>(propOrFieldName);

    if (builder == null)
    {
      this.Skip(e => propExpr);
    }
    else
    {
#if NET8_0_OR_GREATER
      this.RulesFor ??= new Dictionary<string, dynamic>();
#else
      this.RulesFor ??= new Dictionary<string, RuleForModel>();
#endif
      this.RulesFor.Add(propOrFieldName, new RuleForModel((IFluentBogusBuilderBeta<object>)builder, count));
    }

    return this;
  }

  /// <inheritdoc />
  public IFluentBogusBuilderOptionBeta<TEntity> UseRuleFor<TProperty, TPropEntity>(
    Expression<Func<TEntity, TProperty>> propExpr,
    FluentBogusBuilderBeta<TPropEntity>? builder)
    where TProperty : TPropEntity?
    where TPropEntity : class
  {
    var propOrFieldName = FluentExpression.MemberNameFor(propExpr);
    FluentExpression.EnsureMemberExists<TEntity>(propOrFieldName);

    if (builder == null)
    {
      this.Skip(e => propExpr);
    }
    else
    {
#if NET8_0_OR_GREATER
      this.RulesFor ??= new Dictionary<string, dynamic>();
#else
      this.RulesFor ??= new Dictionary<string, RuleForModel>();
#endif
      this.RulesFor.Add(propOrFieldName, new RuleForModel((IFluentBogusBuilderBeta<object>)builder));
    }

    return this;
  }

  private void SkipProperty<TProperty>(Expression<Func<TEntity, TProperty>> propExpr)
  {

    var propertyOrFieldName = FluentExpression.MemberNameFor(propExpr);
    FluentExpression.EnsureMemberExists<TEntity>(propertyOrFieldName);

    if (!this.SkipProperties?.Contains(propertyOrFieldName) ?? true)
    {
      this.SkipProperties ??= new List<string>();
      this.SkipProperties.Add(propertyOrFieldName);
    }
    else
    {
      throw new InvalidOperationException($"The property {propertyOrFieldName} for type {typeof(TEntity).Name} is already set to be skipped.");
    }
  }
}

public static class FluentBogusGeneratorBeta
{
  public static FluentBogusBuilderBeta<TEntity> CreateBuilder<TEntity>()
    where TEntity : class => new();

  public static FluentBogusBuilderBeta<TEntity> CreateBuilder<TEntity>(IFluentBogusBuilderOptionBeta<TEntity> options)
    where TEntity : class => new(options);

  public static FluentBogusBuilderBeta<TEntity> CreateBuilder<TEntity>(
    Action<IFluentBogusBuilderOptionBeta<TEntity>> configure)
    where TEntity : class
  {
    var options = new FluentBogusBuilderOptionBeta<TEntity>();
    configure.Invoke(options);
    return new FluentBogusBuilderBeta<TEntity>(options);
  }
}

public class FluentBogusGeneratorBeta<TEntity> : IFluentBogusGeneratorBeta<TEntity>
  where TEntity : class
{
  public FluentBogusGeneratorBeta(IFluentBogusBuilderOptionBeta<TEntity> options) => this.Options = options;

  protected internal IFluentBogusBuilderOptionBeta<TEntity> Options { get; }

  /// <inheritdoc />
  public ICollection<TEntity> Generate(int count)
  {
    var options = Options as FluentBogusBuilderOptionBeta<TEntity>;
    return options?.Faker?.Generate(count, options.RuleSetString) ?? throw new InvalidOperationException($"Generator has not been configured. Create a builder with/out options first.");
  }

  /// <inheritdoc />
  public TEntity Generate()
  {
    var options = Options as FluentBogusBuilderOptionBeta<TEntity>;
    return options?.Faker?.Generate(options.RuleSetString) ?? throw new InvalidOperationException($"Generator has not been configured. Create a builder with/out options first.");
  }
}

public class FluentBogusBuilderBeta<TEntity> : IFluentBogusBuilderBeta<TEntity>
    where TEntity : class
{
  public FluentBogusBuilderBeta() => this.Options = new FluentBogusBuilderOptionBeta<TEntity>();

  public FluentBogusBuilderBeta(IFluentBogusBuilderOptionBeta<TEntity> options)
  {
    this.Options = options;
  }

  protected internal IFluentBogusBuilderOptionBeta<TEntity> Options { get; }

  /// <inheritdoc />
  public IFluentBogusGeneratorBeta<TEntity> Build()
  {
    // Get the concrete instance for the builder's Options.
    var options = (FluentBogusBuilderOptionBeta<TEntity>)this.Options;

    // Instantiate the Faker.
    if (options.FakerType == null)
    {
      options.Faker = new AutoFaker<TEntity>();
    }
    else
    {
      options.Faker = Activator.CreateInstance(options.FakerType, options.FakerArgs) as AutoFaker<TEntity>;
    }

    // Configure the Faker.
    options.Faker?.Configure(config =>
    {
      // Run custom faker configuration defined by user.
      options.FakerConfiguration?.Invoke(config);

      // Configures the default binding.
      config.WithBinder<MoqBinder>();

      // Configures members to be skipped for a type
      foreach (var propName in options.SkipProperties)
      {
        config.WithSkip<TEntity>(propName);
      }
    });

    options.Faker?.UseSeed(options.Seed);

    foreach (var (prop, rule) in options.RulesFor)
    {
      // Ensure to not add a rule for skipped properties.
      if (!options.SkipProperties.Contains(prop))
      {
        options.Faker?.RuleFor(prop, _ => rule.Builder.Build().Generate(rule.GenCounter));
      }
    }

    return new FluentBogusGeneratorBeta<TEntity>(options);
  }
}
