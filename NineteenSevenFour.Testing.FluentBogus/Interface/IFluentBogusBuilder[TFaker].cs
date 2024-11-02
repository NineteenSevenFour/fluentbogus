// <copyright file="IFluentBogusBuilder[TFaker].cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649
namespace NineteenSevenFour.Testing.FluentBogus
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;
  using AutoBogus;

  /// <summary>
  /// Interface of the FluentBogusBuilder.
  /// </summary>
  /// <typeparam name="TFaker">The <see cref="AutoFaker"/> instance that will be used to fake the <typeparam name="TEntity"/>.</typeparam>
  /// <typeparam name="TEntity">The entity to fake.</typeparam>
  public interface IFluentBogusBuilder<TFaker, TEntity>
    where TFaker : AutoFaker<TEntity>, new()
    where TEntity : class
  {
    /// <summary>
    /// The seed value to use in order to consistently generates data.
    /// </summary>
    /// <param name="seed">The seed value.</param>
    /// <returns>The <see cref="IFluentBogusBuilder{TFaker,TEntity}"/> instance.</returns>
    IFluentBogusBuilder<TFaker, TEntity> UseSeed(int seed);

    /// <summary>
    /// Provides capability to pass arguments to an <see cref="AutoFaker"/> instance.
    /// </summary>
    /// <param name="args">An array of arguments.</param>
    /// <returns>The <see cref="IFluentBogusBuilder{TFaker,TEntity}"/> instance.</returns>
    IFluentBogusBuilder<TFaker, TEntity> UseArgs(object?[]? args);

    /// <summary>
    /// An action that allow to hook into the configuration process of an <see cref="AutoFaker"/> builder.
    /// </summary>
    /// <param name="configBuilder">The <see cref="AutoFaker"/> builder.</param>
    /// <returns>The <see cref="IFluentBogusBuilder{TFaker,TEntity}"/> instance.</returns>
    IFluentBogusBuilder<TFaker, TEntity> UseConfig(Action<IAutoGenerateConfigBuilder> configBuilder);

    /// <summary>
    /// Register an <see cref="AutoFaker"/> ruleset.
    /// </summary>
    /// <param name="ruleset">The name of the ruleset.</param>
    /// <returns>The <see cref="IFluentBogusBuilder{TFaker,TEntity}"/> instance.</returns>
    IFluentBogusBuilder<TFaker, TEntity> UseRuleSet(string ruleset);

    /// <summary>
    /// Register an array of <see cref="AutoFaker"/> ruleset.
    /// </summary>
    /// <param name="rulesets">An array of ruleset name.</param>
    /// <returns>The <see cref="IFluentBogusBuilder{TFaker,TEntity}"/> instance.</returns>
    IFluentBogusBuilder<TFaker, TEntity> UseRuleSet(params string[] rulesets);

    /// <summary>
    /// Register an array of lambda expression to skip associated property during data generation.
    /// </summary>
    /// <param name="properties">The lambda expression describing the collection property of the <typeparam name="TEntity"/>.</param>
    /// <returns>The <see cref="IFluentBogusBuilder{TFaker,TEntity}"/> instance.</returns>
    IFluentBogusBuilder<TFaker, TEntity> Skip(params Expression<Func<TEntity, object?>>[] properties);

    /// <summary>
    /// Register a lambda expression to skip a property during data generation.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property belonging to the <typeparam name="TEntity"/>.</typeparam>
    /// <param name="prop">The lambda expression describing the property of the <typeparam name="TEntity"/>.</param>
    /// <returns>The <see cref="IFluentBogusBuilder{TFaker,TEntity}"/> instance.</returns>
    IFluentBogusBuilder<TFaker, TEntity> Skip<TProperty>(Expression<Func<TEntity, TProperty>> prop);

    /// <summary>
    /// Defines a rule to generate fake data for a specific collection member.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property belonging to the <typeparam name="TEntity"/>.</typeparam>
    /// <typeparam name="TPropEntity">The type of the property of the collection <typeparam name="TProperty"/> property belonging to the <typeparam name="TEntity"/>.</typeparam>
    /// <typeparam name="TPropFaker">The <see cref="AutoFaker{TProperty}"/> that will be used to generate fake data.</typeparam>
    /// <param name="prop">The lambda expression describing the property of the <typeparam name="TEntity"/>.</param>
    /// <param name="builder">The <see cref="IFluentBogusBuilder{TFaker,TEntity}"/> instance using the <see cref="AutoFaker{TProperty}"/>.</param>
    /// <param name="count">The number of <typeparam name="TEntity"/> to generates.</param>
    /// <returns>The <see cref="IFluentBogusBuilder{TFaker,TEntity}"/> instance.</returns>
    IFluentBogusBuilder<TFaker, TEntity> RuleFor<TProperty, TPropEntity, TPropFaker>(
      Expression<Func<TEntity, TProperty>> prop,
      IFluentBogusBuilder<TPropFaker, TPropEntity>? builder,
      int count)
      where TPropEntity : class
      where TProperty : ICollection<TPropEntity?>?
      where TPropFaker : AutoFaker<TPropEntity>, new();

    /// <summary><typeparam name="TEntity"/><typeparam name="TEntity"/>
    /// Defines a rule to generate fake data for a specific member.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property belonging to the <typeparam name="TEntity"/>.</typeparam>
    /// <typeparam name="TPropFaker">The <see cref="AutoFaker{TProperty}"/> that will be used to generate fake data.</typeparam>
    /// <param name="prop">The lambda expression describing the property of the <typeparam name="TEntity"/>.</param>
    /// <param name="builder">The <see cref="IFluentBogusBuilder{TFaker,TEntity}"/> instance using the <see cref="AutoFaker{TProperty}"/>.</param>
    /// <returns>The <see cref="IFluentBogusBuilder{TFaker,TEntity}"/> instance.</returns>
    IFluentBogusBuilder<TFaker, TEntity> RuleFor<TProperty, TPropFaker>(
      Expression<Func<TEntity, TProperty?>> prop,
      IFluentBogusBuilder<TPropFaker, TProperty>? builder)
      where TProperty : class
      where TPropFaker : AutoFaker<TProperty>, new();

    /// <summary>
    /// Generates a collection of <typeparam name="TEntity"/>.
    /// </summary>
    /// <param name="count">The number of <typeparam name="TEntity"/> to generates.</param>
    /// <returns>The <see cref="ICollection{TEntity}"/> instance.</returns>
    ICollection<TEntity> Generate(int count);

    /// <summary>
    /// Generates a single <typeparam name="TEntity"/>.
    /// </summary>
    /// <returns>A <typeparam name="TEntity"/>.</returns>
    TEntity Generate();
  }
}
