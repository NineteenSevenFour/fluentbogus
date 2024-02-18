using AutoBogus;

using NineteenSevenFour.Testing.FluentBogus.Interface;

namespace NineteenSevenFour.Testing.FluentBogus
{
  public class FluentBogusBuilder<TEntity> : IFluentBogusBuilder<TEntity>
      where TEntity : class
  {
    /// <inheritdoc/>>
    public IFluentBogusBuilder<AutoFaker<TEntity>, TEntity> WithDefault(params object?[]? args)
      => new FluentBogusBuilder<AutoFaker<TEntity>, TEntity>(args);

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> With<TFaker>(params object?[]? args)
        where TFaker : AutoFaker<TEntity>, new() => new FluentBogusBuilder<TFaker, TEntity>(args);
  }
}
