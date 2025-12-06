using AutoBogus;
using Microsoft.Extensions.Logging;
using NineteenSevenFour.Testing.FluentBogus.AutoMapper.Interface;

namespace NineteenSevenFour.Testing.FluentBogus.AutoMapper.Extension
{
  public static class FluentBuilderExtension
  {
    /// <summary>
    /// Creates a fluent mapper builder that enables mapping between the specified entity and model types using the
    /// provided faker and logger factory.
    /// </summary>
    /// <typeparam name="TFaker">The type of the AutoFaker used to generate instances of the entity type.</typeparam>
    /// <typeparam name="TEntity">The type of the entity to be mapped.</typeparam>
    /// <typeparam name="TModel">The type of the model to map to.</typeparam>
    /// <param name="builder">The fluent Bogus builder that configures generation of entity instances.</param>
    /// <param name="loggerFactory">The logger factory used to create loggers for mapping operations.</param>
    /// <returns>A fluent mapper builder that supports mapping between the entity and model types.</returns>
    public static IFluentMapperBuilder<TFaker, TEntity, TModel> MapTo<TFaker, TEntity, TModel>(
      this FluentBogusBuilder<TFaker, TEntity> builder,
      ILoggerFactory loggerFactory)
      where TFaker : AutoFaker<TEntity>, new()
      where TEntity : class
      where TModel : class => new FluentMapperBuilder<TFaker, TEntity, TModel>(builder, loggerFactory);
  }
}
