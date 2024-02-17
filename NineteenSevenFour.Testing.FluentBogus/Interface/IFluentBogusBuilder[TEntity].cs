using AutoBogus;

namespace NineteenSevenFour.Testing.FluentBogus.Interface
{
  public interface IFluentBogusBuilder<TEntity>
      where TEntity : class
  {
    IFluentBogusBuilder<AutoFaker<TEntity>, TEntity> WithDefault(params object?[]? args);

    IFluentBogusBuilder<TFaker, TEntity> With<TFaker>(params object?[]? args)
        where TFaker : AutoFaker<TEntity>, new();
  }
}
