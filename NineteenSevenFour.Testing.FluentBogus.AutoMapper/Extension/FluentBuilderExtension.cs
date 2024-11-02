// <copyright file="FluentBuilderExtension.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

using AutoBogus;

namespace NineteenSevenFour.Testing.FluentBogus.AutoMapper
{
  public static class FluentBuilderExtension
  {
    public static IFluentMapperBuilder<TFaker, TEntity, TModel> MapTo<TFaker, TEntity, TModel>(
      this FluentBogusBuilder<TFaker, TEntity> builder)
      where TFaker : AutoFaker<TEntity>, new()
      where TEntity : class
      where TModel : class => new FluentMapperBuilder<TFaker, TEntity, TModel>(builder);
  }
}
