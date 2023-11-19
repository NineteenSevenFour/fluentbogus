using AutoBogus;

using NineteenSevenFour.Testing.FluentBogus;
using NineteenSevenFour.Testing.FluentMapper.Interface;

namespace NineteenSevenFour.Testing.FluentMapper.Extension;

public static class FluentBuilderExtension
{
  public static IFluentMapperBuilder<TFaker, TEntity, TModel> MapTo<TFaker, TEntity, TModel>(
    this FluentBogusBuilder<TFaker, TEntity> builder)
    where TFaker : AutoFaker<TEntity>, new()
    where TEntity : class
    where TModel : class => new FluentMapperBuilder<TFaker, TEntity, TModel>(builder);
}
