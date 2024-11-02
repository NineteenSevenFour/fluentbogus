// <copyright file="FluentMapperBuilder.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

using AutoBogus;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AutoMapper.Extensions.ExpressionMapping;

namespace NineteenSevenFour.Testing.FluentBogus.AutoMapper
{
  using System;
  using System.Collections.Generic;

  public class FluentMapperBuilder<TFaker, TEntity, TModel> : FluentBogusBuilder<TFaker, TEntity>, IFluentMapperBuilder<TFaker, TEntity, TModel>
    where TFaker : AutoFaker<TEntity>, new()
    where TEntity : class
    where TModel : class
  {
    internal readonly Dictionary<string, Profile> MappingProfiles = new();

    public FluentMapperBuilder(FluentBogusBuilder<TFaker, TEntity> builder)
      : base(builder)
    {
    }

    internal IMapper Mapper => this.MapperConfiguration.CreateMapper();

    internal IConfigurationProvider MapperConfiguration => new MapperConfiguration(cfg =>
    {
      cfg.AllowNullCollections = true;
      cfg.AddCollectionMappers();
      cfg.AddExpressionMapping();

      foreach (var profile in this.MappingProfiles.Values)
      {
        cfg.AddProfile(profile);
      }
    });

    /// <inheritdoc/>>
    public new (ICollection<TEntity>, ICollection<TModel>) Generate(int count)
    {
      var entities = base.Generate(count);
      var models = this.Mapper.Map<ICollection<TModel>>(entities);
      return (entities, models);
    }

    /// <inheritdoc/>>
    public new (TEntity, TModel) Generate()
    {
      var entity = base.Generate();
      var model = this.Mapper.Map<TModel>(entity);
      return (entity, model);
    }

    /// <inheritdoc/>>
    public IFluentMapperBuilder<TFaker, TEntity, TModel> WithProfile<TProfile>()
      where TProfile : Profile, new()
    {
      UseProfileInternal(new TProfile());
      return this;
    }

    internal void UseProfileInternal(Profile? profile)
    {
      if (profile == null)
      {
        throw new ArgumentNullException(nameof(profile), $"Not a valid mapping profile instance:");
      }

      var key = profile.GetType().FullName;
      if (string.IsNullOrWhiteSpace(key))
      {
        throw new ArgumentException($"Not a valid mapping profile: {key}", nameof(profile));
      }

      if (this.MappingProfiles.ContainsKey(key))
      {
        throw new InvalidOperationException($"The profile {profile.GetType().Name} is already registered ensure UseProfile() is called once per profile to add..");
      }

      this.MappingProfiles.Add(key, profile);
    }
  }
}
