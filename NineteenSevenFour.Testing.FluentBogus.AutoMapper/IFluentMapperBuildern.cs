// <copyright file="IFluentMapperBuildern.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

using AutoBogus;
using AutoMapper;

namespace NineteenSevenFour.Testing.FluentBogus.AutoMapper
{
  using System.Collections.Generic;

  public interface IFluentMapperBuilder<TFaker, TEntity, TModel>
    where TFaker : AutoFaker<TEntity>, new()
    where TEntity : class
    where TModel : class
  {
    IFluentMapperBuilder<TFaker, TEntity, TModel> WithProfile<TProfile>()
      where TProfile : Profile, new();

    (ICollection<TEntity>, ICollection<TModel>) Generate(int count);

    (TEntity, TModel) Generate();
  }
}
