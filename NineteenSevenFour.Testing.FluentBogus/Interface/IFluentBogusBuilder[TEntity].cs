using AutoBogus;

namespace NineteenSevenFour.Testing.FluentBogus.Interface
{
  /// <summary>
  /// Defines a fluent interface for configuring entity generation strategies using AutoFaker for a specified entity
  /// type.
  /// </summary>
  /// <remarks>Use this interface to customize how entities of type TEntity are generated, including selecting
  /// default value strategies or specifying custom AutoFaker implementations. This is useful for scenarios such as
  /// automated testing, seeding, or prototyping where flexible and repeatable entity creation is required.</remarks>
  /// <typeparam name="TEntity">The type of entity to be generated. Must be a reference type.</typeparam>
  public interface IFluentBogusBuilder<TEntity>
      where TEntity : class
  {
    /// <summary>
    /// Configures the builder to use the default value generation strategy for the entity, optionally supplying
    /// constructor arguments.
    /// </summary>
    /// <remarks>Use this method when you want the builder to create entities with their default values,
    /// optionally providing specific constructor parameters. This is useful for scenarios where the entity requires
    /// certain arguments for instantiation.</remarks>
    /// <param name="args">An array of arguments to pass to the entity's constructor when generating default values. If null or empty, the
    /// default constructor is used.</param>
    /// <returns>An IFluentBogusBuilder instance configured to generate entities using the default value strategy and the
    /// specified constructor arguments.</returns>
    IFluentBogusBuilder<AutoFaker<TEntity>, TEntity> WithDefault(params object?[]? args);

    /// <summary>
    /// Configures the builder to use a specific AutoFaker implementation for generating entities, optionally supplying
    /// constructor arguments for the faker type.
    /// </summary>
    /// <remarks>Use this method to customize the entity generation process by providing a specific AutoFaker
    /// implementation and any required constructor arguments. This enables advanced scenarios where the default faker
    /// behavior needs to be overridden.</remarks>
    /// <typeparam name="TFaker">The type of AutoFaker to use for entity generation. Must inherit from AutoFaker{TEntity} and have a public
    /// parameterless constructor.</typeparam>
    /// <param name="args">An array of arguments to pass to the constructor of the specified AutoFaker type. Can be null or empty if the
    /// faker type does not require constructor parameters.</param>
    /// <returns>An IFluentBogusBuilder instance configured to use the specified AutoFaker type for entity generation.</returns>
    IFluentBogusBuilder<TFaker, TEntity> With<TFaker>(params object?[]? args)
        where TFaker : AutoFaker<TEntity>, new();
  }
}
