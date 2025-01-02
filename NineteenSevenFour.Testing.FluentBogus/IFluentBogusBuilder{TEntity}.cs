// <copyright file="IFluentBogusBuilder{TEntity}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649
namespace NineteenSevenFour.Testing.FluentBogus;

using AutoBogus;

/// <summary>
/// <see cref="FluentBogusBuilder{TEntity}"/> is the base class to allow fake data generation and provides two options to choose from.
/// First is to use the default <see cref="AutoFaker"/> intance and optionally provide it configuration arguments.
/// Second option is to use a custom <see cref="AutoFaker"/> intance which implement custom rules.
/// </summary>
/// <typeparam name="TEntity">The type of the entity use by the builder.</typeparam>
public interface IFluentBogusBuilder<TEntity>
  where TEntity : class
{
  /// <summary>
  /// Initialize an <see cref="IFluentBogusBuilder{TFaker,TEntity}"/> using a default <see cref="AutoFaker{TEntity}"/>.
  /// </summary>
  /// <param name="args">The array of argument to pass to the <see cref="AutoFaker{TEntity}"/> instance.</param>
  /// <returns>An <see cref="IFluentBogusBuilder{TFaker,TEntity}"/> instance.</returns>
  IFluentBogusBuilder<AutoFaker<TEntity>, TEntity> UseFaker(params object?[]? args);

  /// <summary>
  /// Initialize an <see cref="IFluentBogusBuilder{TFaker,TEntity}"/> using a custom <see cref="AutoFaker{TEntity}"/>.
  /// </summary>
  /// <typeparam name="TFaker">The custom <see cref="AutoFaker{TEntity}"/> type.</typeparam>
  /// <param name="args">The array of argument to pass to the <see cref="AutoFaker{TEntity}"/> instance.</param>
  /// <returns>An <see cref="IFluentBogusBuilder{TFaker,TEntity}"/> instance.</returns>
  IFluentBogusBuilder<TFaker, TEntity> UseFaker<TFaker>(params object?[]? args)
    where TFaker : AutoFaker<TEntity>, new();
}
