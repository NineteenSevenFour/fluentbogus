using AutoBogus;

using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AutoMapper.Extensions.ExpressionMapping;

using NineteenSevenFour.Testing.FluentBogus;
using NineteenSevenFour.Testing.FluentMapper.Interface;

namespace NineteenSevenFour.Testing.FluentMapper;

public class FluentMapperBuilder<TFaker, TEntity, TModel> : FluentBogusBuilder<TFaker, TEntity>, IFluentMapperBuilder<TFaker, TEntity, TModel>
    where TFaker : AutoFaker<TEntity>, new()
    where TEntity : class
    where TModel : class
{
  internal readonly Dictionary<string, Profile> mappingProfiles = new();
  internal IMapper Mapper => MapperConfiguration.CreateMapper();
  internal IConfigurationProvider MapperConfiguration => new MapperConfiguration(cfg =>
  {
    cfg.AllowNullCollections = true;
    cfg.AddCollectionMappers();
    cfg.AddExpressionMapping();

    foreach (var profile in mappingProfiles.Values)
    {
      cfg.AddProfile(profile);
    }
  });

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

    if (mappingProfiles.ContainsKey(key))
    {
      throw new InvalidOperationException($"The profile {profile.GetType().Name} is already registered ensure UseProfile() is called once per profile to add..");
    }
    mappingProfiles.Add(key, profile);
  }

  public FluentMapperBuilder(FluentBogusBuilder<TFaker, TEntity> builder) : base(builder)
  {
  }

  /// <inheritdoc/>>
  public new (ICollection<TEntity>, ICollection<TModel>) Generate(int count)
  {
    var entities = base.Generate(count);
    var models = Mapper.Map<ICollection<TModel>>(entities);
    return (entities, models);
  }

  /// <inheritdoc/>>
  public new (TEntity, TModel) Generate()
  {
    var entity = base.Generate();
    var model = Mapper.Map<TModel>(entity);
    return (entity, model);
  }

  /// <inheritdoc/>>
  public IFluentMapperBuilder<TFaker, TEntity, TModel> With<TProfile>()
      where TProfile : Profile, new()
  {
    UseProfileInternal(new TProfile());
    return this;
  }
}
