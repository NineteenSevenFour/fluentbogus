using NineteenSevenFour.Testing.FluentBogus.Interface;

namespace NineteenSevenFour.Testing.FluentBogus.Extension
{
  public static class FluentBogusBuilder
  {
    public static IFluentBogusBuilder<TEntity> Fake<TEntity>()
      where TEntity : class => new FluentBogusBuilder<TEntity>();
  }
}
