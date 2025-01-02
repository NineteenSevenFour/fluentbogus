// <copyright file="IFluentMapperBuilder{TFaker, TEntity, TModel}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649
namespace NineteenSevenFour.Testing.FluentBogus.AutoMapper;

using System.Collections.Generic;

/// <summary>
/// Define the <see cref="FluentMapperBuilder{TFaker,TEntity,TModel}"/> class which integrates with <see cref="AutoMapper"/>.
/// </summary>
/// <typeparam name="TFaker">The type of <see cref="AutoFaker"/> use to generates the <typeparamref name="TEntity"/>.</typeparam>
/// <typeparam name="TEntity">The type of the <typeparamref name="TEntity"/>.</typeparam>
/// <typeparam name="TModel">The type of the <typeparamref name="TModel"/>.</typeparam>
public interface IFluentMapperBuilder<TFaker, TEntity, TModel>
  where TFaker : AutoFaker<TEntity>, new()
  where TEntity : class
  where TModel : class
{
  /// <summary>
  /// Provide the <see cref="Profile"/> to use to map the <typeparamref name="TEntity"/> to a <typeparamref name="TModel"/>.
  /// </summary>
  /// <typeparam name="TProfile">A <see cref="Profile"/> to use for the mapping.</typeparam>
  /// <returns>The instance of <see cref="FluentMapperBuilder{TFaker,TEntity,TModel}"/>.</returns>
  IFluentMapperBuilder<TFaker, TEntity, TModel> WithProfile<TProfile>()
    where TProfile : Profile, new();

  /// <summary>
  /// Generates a collection of <typeparamref name="TEntity"/> and map them to their respective <typeparamref name="TModel"/>.
  /// </summary>
  /// <param name="count">The number to <typeparamref name="TEntity"/> to generate.</param>
  /// <returns>A collection of <typeparamref name="TModel"/>.</returns>
  (ICollection<TEntity> entities, ICollection<TModel> models) Generate(int count);

  /// <summary>
  /// Generates a single <typeparamref name="TEntity"/>> and map it the <typeparamref name="TModel"/>.
  /// </summary>
  /// <returns>A single <typeparamref name="TModel"/>.</returns>
  (TEntity entity, TModel model) Generate();
}
