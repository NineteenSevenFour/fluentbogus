// <copyright file="FluentMapperBuilder{TFaker, TEntity, TModel}.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649
namespace NineteenSevenFour.Testing.FluentBogus.AutoMapper;

using System;
using System.Collections.Generic;

#if NET8_0_OR_GREATER
/// <inheritdoc cref="IFluentMapperBuilder{TFaker,TEntity,TModel}"/>
public class FluentMapperBuilder<TFaker, TEntity, TModel>(FluentBogusBuilder<TFaker, TEntity> builder)
  : FluentBogusBuilder<TFaker, TEntity>(builder), IFluentMapperBuilder<TFaker, TEntity, TModel>
  where TFaker : AutoFaker<TEntity>, new()
  where TEntity : class
  where TModel : class
{
  /// <summary>
  /// Gets the configured mapping profiles.
  /// </summary>
#pragma warning disable SA1401
  internal readonly Dictionary<string, Profile> MappingProfiles = [];
#pragma warning restore SA1401
#else
/// <inheritdoc cref="IFluentMapperBuilder{TFaker,TEntity,TModel}"/>
public class FluentMapperBuilder<TFaker, TEntity, TModel> : FluentBogusBuilder<TFaker, TEntity>, IFluentMapperBuilder<TFaker, TEntity, TModel>
  where TFaker : AutoFaker<TEntity>, new()
  where TEntity : class
  where TModel : class
{
  /// <summary>
  /// Gets the configured mapping profiles.
  /// </summary>
#pragma warning disable SA1401
  internal readonly Dictionary<string, Profile> MappingProfiles = new();
#pragma warning restore SA1401

  /// <summary>
  /// Initializes a new instance of the <see cref="FluentMapperBuilder{TFaker, TEntity, TModel}"/> class.
  /// </summary>
  /// <param name="builder">The instance of the <see cref="FluentBogusBuilder{TFaker,TEntity}"/> to use as base.</param>
  public FluentMapperBuilder(FluentBogusBuilder<TFaker, TEntity> builder)
    : base(builder)
  {
  }
#endif

  /// <summary>
  /// Gets the mapper instance.
  /// </summary>
  internal IMapper Mapper => this.MapperConfiguration.CreateMapper();

  /// <summary>
  /// Gets the mapper configuration.
  /// </summary>
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
    var profile = new TProfile();

    if (profile == null)
    {
      throw new ArgumentNullException(nameof(profile), $"Not a valid mapping profile instance:");
    }

    var key = profile.GetType().FullName;
    if (string.IsNullOrWhiteSpace(key))
    {
      throw new ArgumentException($"Not a valid mapping profile: {key}", nameof(profile));
    }

    if (!this.MappingProfiles.TryAdd(key, profile))
    {
      throw new InvalidOperationException($"The profile {profile.GetType().Name} is already registered ensure UseProfile() is called once per profile to add..");
    }

    return this;
  }
}
