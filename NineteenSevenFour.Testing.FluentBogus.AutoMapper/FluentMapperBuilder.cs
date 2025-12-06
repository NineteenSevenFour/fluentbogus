using AutoBogus;

using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using NineteenSevenFour.Testing.FluentBogus.AutoMapper.Interface;
using Microsoft.Extensions.Logging;

namespace NineteenSevenFour.Testing.FluentBogus.AutoMapper
{
  /// <summary>
  /// Provides a fluent builder for configuring object-to-object mapping between entity and model types using AutoMapper
  /// and Bogus. Enables generation of test data and mapping profiles for automated testing and data transformation
  /// scenarios.
  /// </summary>
  /// <remarks>Use this builder to register mapping profiles and generate collections of entities and their
  /// corresponding models for testing or data seeding. Mapping profiles can be added fluently, and generated entities
  /// are automatically mapped to models using the configured profiles. This class is intended for scenarios where both
  /// data generation and mapping are required in a streamlined workflow.</remarks>
  /// <typeparam name="TFaker">The type of AutoFaker used to generate instances of the entity type. Must inherit from AutoFaker{TEntity} and have
  /// a parameterless constructor.</typeparam>
  /// <typeparam name="TEntity">The entity type to be generated and mapped. Must be a reference type.</typeparam>
  /// <typeparam name="TModel">The model type to which entities are mapped. Must be a reference type.</typeparam>
  public class FluentMapperBuilder<TFaker, TEntity, TModel> : FluentBogusBuilder<TFaker, TEntity>, IFluentMapperBuilder<TFaker, TEntity, TModel>
      where TFaker : AutoFaker<TEntity>, new()
      where TEntity : class
      where TModel : class
  {
    /// <summary>
    /// Provides a mapping of profile names to their corresponding profile instances.
    /// </summary>
    internal readonly Dictionary<string, Profile> MappingProfiles = new Dictionary<string, Profile>();

    /// <summary>
    /// Gets the object-to-object mapper configured for this instance.
    /// </summary>
    internal IMapper Mapper => MapperConfiguration.CreateMapper();

    /// <summary>
    /// Gets the AutoMapper configuration provider used for mapping object models within the application.
    /// </summary>
    /// <remarks>The configuration provider includes all registered mapping profiles and enables expression
    /// mapping. Null collections are allowed during mapping operations. This property is intended for internal use and
    /// should not be accessed directly by external components.</remarks>
    internal IConfigurationProvider MapperConfiguration => new MapperConfiguration(cfg =>
    {
      cfg.AllowNullCollections = true;
      cfg.AddExpressionMapping();

      foreach (var profile in MappingProfiles.Values)
      {
        cfg.AddProfile(profile);
      }
    }, LoggerFactory);

    /// <summary>
    /// Registers the specified mapping profile for use within the current context.
    /// </summary>
    /// <remarks>This method is intended for internal use to ensure that each mapping profile is registered
    /// only once per context. Calling this method multiple times with the same profile type will result in an
    /// exception.</remarks>
    /// <param name="profile">The mapping profile instance to register. Cannot be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if the profile parameter is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the profile does not have a valid type name.</exception>
    /// <exception cref="InvalidOperationException">Thrown if a profile of the same type has already been registered.</exception>
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

      if (MappingProfiles.ContainsKey(key))
      {
        throw new InvalidOperationException($"The profile {profile.GetType().Name} is already registered ensure UseProfile() is called once per profile to add..");
      }
      MappingProfiles.Add(key, profile);
    }

    /// <summary>
    /// Initializes a new instance of the FluentMapperBuilder class with the specified builder and logger factory.
    /// </summary>
    /// <param name="builder">The FluentBogusBuilder instance used to configure entity mapping operations. Cannot be null.</param>
    /// <param name="loggerFactory">The logger factory used to create loggers for mapping operations. Cannot be null.</param>
    public FluentMapperBuilder(FluentBogusBuilder<TFaker, TEntity> builder, ILoggerFactory loggerFactory) : base(builder, loggerFactory)
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
}
