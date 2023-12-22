// <copyright file="FluentBogusBuilder{TEntity}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649
namespace NineteenSevenFour.Testing.FluentBogus;

using AutoBogus;

/// <inheritdoc/>>
public class FluentBogusBuilder<TEntity> : IFluentBogusBuilder<TEntity>
  where TEntity : class
{
  /// <inheritdoc/>>
  public IFluentBogusBuilder<AutoFaker<TEntity>, TEntity> UseFaker(params object?[]? args)
    => new FluentBogusBuilder<AutoFaker<TEntity>, TEntity>(args);

  /// <inheritdoc/>>
  public IFluentBogusBuilder<TFaker, TEntity> UseFaker<TFaker>(params object?[]? args)
    where TFaker : AutoFaker<TEntity>, new() => new FluentBogusBuilder<TFaker, TEntity>(args);
}
