// <copyright file="FluentMapperBuilder.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.AutoMapper;

/// <summary>
/// Provides extension to <see cref="FluentBogusBuilder{TFaker, TEntity}"/> to integrate with <see cref="AutoMapper"/>.
/// </summary>
public static class FluentMapperBuilder
{
  /// <summary>
  /// Allow a faked <typeparamref name="TEntity"/> to be mapped to a <typeparamref name="TModel"/>.
  /// </summary>
  /// <typeparam name="TFaker">The <see cref="AutoFaker"/> instance.</typeparam>
  /// <typeparam name="TEntity">The type of the entity to be mapped.</typeparam>
  /// <typeparam name="TModel">The type of the model used in the mapping.</typeparam>
  /// <param name="builder">The instance of the <see cref="FluentBogusBuilder{TFaker, TEntity}"/>.</param>
  /// <returns>An instance of <see cref="FluentMapperBuilder{TFaker, TEntity, TModel}"/>.</returns>
  public static IFluentMapperBuilder<TFaker, TEntity, TModel> MapTo<TFaker, TEntity, TModel>(
    this FluentBogusBuilder<TFaker, TEntity> builder)
    where TFaker : AutoFaker<TEntity>, new()
    where TEntity : class
    where TModel : class => new FluentMapperBuilder<TFaker, TEntity, TModel>(builder);
}
