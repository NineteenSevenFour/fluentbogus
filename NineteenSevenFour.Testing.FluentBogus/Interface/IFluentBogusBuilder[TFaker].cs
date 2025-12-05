using AutoBogus;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Interface
{
  /// <summary>
  /// Defines a fluent interface for configuring and generating test entities using Bogus-based auto-fakers. Provides
  /// methods to customize generation rules, seed values, constructor arguments, and property behaviors for flexible
  /// test data creation.
  /// </summary>
  /// <remarks>This interface enables chaining of configuration methods to build complex test data scenarios. It
  /// supports skipping properties, applying custom rule sets, and composing nested builders for related entities.
  /// Implementations are typically used in unit tests or data seeding workflows to automate the creation of realistic,
  /// customizable objects.</remarks>
  /// <typeparam name="TFaker">The type of auto-faker used to generate instances of <typeparamref name="TEntity"/>. Must inherit from
  /// AutoFaker{TEntity} and have a parameterless constructor.</typeparam>
  /// <typeparam name="TEntity">The type of entity to be generated. Must be a reference type.</typeparam>
  public interface IFluentBogusBuilder<TFaker, TEntity>
      where TFaker : AutoFaker<TEntity>, new()
      where TEntity : class
  {
    /// <summary>
    /// Sets the random seed used for data generation, ensuring reproducible results.
    /// </summary>
    /// <remarks>Setting a seed is useful for testing scenarios where consistent, repeatable fake data is
    /// required.</remarks>
    /// <param name="seed">The seed value to initialize the random number generator. Using the same seed will produce identical generated
    /// data across runs.</param>
    /// <returns>The current builder instance with the specified seed applied, enabling method chaining.</returns>
    IFluentBogusBuilder<TFaker, TEntity> UseSeed(int seed);

    /// <summary>
    /// Configures the builder to use the specified constructor arguments when creating instances of the entity type.
    /// </summary>
    /// <remarks>Use this method when the entity type requires specific constructor parameters. If the
    /// arguments do not match a valid constructor, an exception may be thrown during entity instantiation.</remarks>
    /// <param name="args">An array of arguments to pass to the entity's constructor. The order and types must match the constructor
    /// signature. Can be null or empty if the constructor does not require arguments.</param>
    /// <returns>An instance of the fluent builder configured to use the provided constructor arguments for entity creation.</returns>
    IFluentBogusBuilder<TFaker, TEntity> UseArgs(object?[]? args);

    /// <summary>
    /// Configures the auto-generation behavior using the specified configuration builder action.
    /// </summary>
    /// <remarks>Use this method to specify custom rules or options for how entities are generated. The
    /// configuration is applied immediately and affects subsequent generation operations.</remarks>
    /// <param name="configBuilder">An action that receives an <see cref="IAutoGenerateConfigBuilder"/> to customize auto-generation settings.
    /// Cannot be null.</param>
    /// <returns>An <see cref="IFluentBogusBuilder{TFaker, TEntity}"/> instance with the applied configuration, enabling further
    /// fluent setup.</returns>
    IFluentBogusBuilder<TFaker, TEntity> UseConfig(Action<IAutoGenerateConfigBuilder> configBuilder);

    /// <summary>
    /// Configures the builder to apply the specified rule set when generating entities.
    /// </summary>
    /// <remarks>Use rule sets to apply predefined groups of rules when generating test data. This allows for
    /// flexible configuration of entity generation based on different scenarios.</remarks>
    /// <param name="ruleset">The name of the rule set to use. Must not be null or empty.</param>
    /// <returns>An instance of the builder configured to use the specified rule set.</returns>
    IFluentBogusBuilder<TFaker, TEntity> UseRuleSet(string ruleset);

    /// <summary>
    /// Configures the builder to apply the specified rule sets when generating entities.
    /// </summary>
    /// <remarks>Rule sets enable grouping of generation rules for more flexible and reusable entity creation.
    /// If multiple rule sets are specified, all corresponding rules will be applied when generating entities.</remarks>
    /// <param name="rulesets">An array of rule set names to apply. Each rule set defines a group of generation rules to be used. Cannot be
    /// null or contain null or empty values.</param>
    /// <returns>An instance of the builder configured to use the specified rule sets. Allows for further fluent configuration.</returns>
    IFluentBogusBuilder<TFaker, TEntity> UseRuleSet(params string[] rulesets);

    /// <summary>
    /// Excludes the specified properties from automatic generation when building entities.
    /// </summary>
    /// <remarks>Use this method to prevent certain properties from being populated by the builder. This is
    /// useful when you want to manually set values or avoid generating data for specific properties.</remarks>
    /// <param name="properties">An array of expressions identifying the properties of <typeparamref name="TEntity"/> to skip during entity
    /// generation. Each expression should select a property to be excluded.</param>
    /// <returns>An <see cref="IFluentBogusBuilder{TFaker, TEntity}"/> instance for further configuration.</returns>
    IFluentBogusBuilder<TFaker, TEntity> Skip(params Expression<Func<TEntity, object?>>[] properties);

    /// <summary>
    /// Excludes the specified property from automatic value generation when building entities.
    /// </summary>
    /// <remarks>Use this method to prevent the builder from setting values for the specified property. This
    /// is useful when certain properties should remain unset or be assigned manually after entity creation.</remarks>
    /// <typeparam name="TProperty">The type of the property to exclude from value generation.</typeparam>
    /// <param name="property">An expression that identifies the property of <typeparamref name="TEntity"/> to skip during entity generation.
    /// Cannot be null.</param>
    /// <returns>A fluent builder that can be further configured to customize entity generation.</returns>
    IFluentBogusBuilder<TFaker, TEntity> Skip<TProperty>(Expression<Func<TEntity, TProperty>> property);

    IFluentBogusBuilder<TFaker, TEntity> RuleFor<TProperty, TPropEntity, TPropFaker>(
        Expression<Func<TEntity, TProperty>> property,
        IFluentBogusBuilder<TPropFaker, TPropEntity> builder,
        int count)
        where TPropEntity : class
        where TProperty : ICollection<TPropEntity?>?
        where TPropFaker : AutoFaker<TPropEntity>, new();

    IFluentBogusBuilder<TFaker, TEntity> RuleFor<TProperty, TPropFaker>(
        Expression<Func<TEntity, TProperty?>> property,
        IFluentBogusBuilder<TPropFaker, TProperty> builder)
        where TProperty : class
        where TPropFaker : AutoFaker<TProperty>, new();

    ICollection<TEntity> Generate(int count);

    TEntity Generate();
  }
}
