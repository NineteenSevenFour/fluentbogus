using AutoBogus;
using Microsoft.Extensions.Logging;
using NineteenSevenFour.Testing.FluentBogus.Interface;

namespace NineteenSevenFour.Testing.FluentBogus
{
  public class FluentBogusBuilder<TEntity> : IFluentBogusBuilder<TEntity>
      where TEntity : class
  {
    /// <summary>
    /// Gets the factory used to create logger instances for logging application events and diagnostics.
    /// </summary>
    /// <remarks>Use this property to obtain an <see cref="ILoggerFactory"/> for creating loggers scoped to
    /// specific categories or components. The factory is typically configured during application startup and should not
    /// be modified after initialization.</remarks>
    protected ILoggerFactory LoggerFactory { get; init; }

    /// <summary>
    /// Initializes a new instance of the FluentBogusBuilder class using the specified logger factory.
    /// </summary>
    /// <param name="loggerFactory">The logger factory to be used for creating logger instances. Must not be null.</param>
    /// <exception cref="NullReferenceException">Thrown if <paramref name="loggerFactory"/> is null.</exception>
    public FluentBogusBuilder(ILoggerFactory loggerFactory)
    {
      LoggerFactory = loggerFactory ?? throw new NullReferenceException($"The {nameof(loggerFactory)} should be provided a proper instance.");
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<AutoFaker<TEntity>, TEntity> WithDefault(params object?[]? args)
      => new FluentBogusBuilder<AutoFaker<TEntity>, TEntity>(LoggerFactory, args);

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> With<TFaker>(params object?[]? args)
        where TFaker : AutoFaker<TEntity>, new() => new FluentBogusBuilder<TFaker, TEntity>(LoggerFactory, args);
  }
}
