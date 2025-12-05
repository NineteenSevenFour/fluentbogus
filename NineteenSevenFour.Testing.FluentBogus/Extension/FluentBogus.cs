using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using NineteenSevenFour.Testing.FluentBogus.Interface;

namespace NineteenSevenFour.Testing.FluentBogus.Extension
{
  /// <summary>
  /// Provides static methods for creating fluent builders that generate fake instances of a specified entity type using
  /// Bogus.
  /// </summary>
  /// <remarks>Use this class to obtain a builder for configuring property generation rules and creating fake
  /// data for testing or seeding scenarios. The returned builders support fluent configuration and can be customized
  /// with logging if desired.</remarks>
  public static class FluentBogusBuilder
  {
    /// <summary>
    /// Creates a fluent builder for generating fake instances of the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to generate. Must be a reference type.</typeparam>
    /// <returns>An <see cref="IFluentBogusBuilder{TEntity}"/> that can be used to configure and create fake instances of
    /// <typeparamref name="TEntity"/>.</returns>
    public static IFluentBogusBuilder<TEntity> Fake<TEntity>()
      where TEntity : class => new FluentBogusBuilder<TEntity>(new NullLoggerFactory());

    /// <summary>
    /// Creates a fluent builder for generating fake instances of the specified entity type using Bogus.
    /// </summary>
    /// <remarks>Use the returned builder to configure property generation rules and create fake data for
    /// testing or seeding purposes.</remarks>
    /// <typeparam name="TEntity">The type of entity to generate. Must be a reference type.</typeparam>
    /// <param name="loggerFactory">The logger factory used to create loggers for the builder. Cannot be null.</param>
    /// <returns>An <see cref="IFluentBogusBuilder{TEntity}"/> instance for configuring and generating fake entities of type
    /// <typeparamref name="TEntity"/>.</returns>
    public static IFluentBogusBuilder<TEntity> Fake<TEntity>(ILoggerFactory loggerFactory)
      where TEntity : class => new FluentBogusBuilder<TEntity>(loggerFactory);
  }
}
