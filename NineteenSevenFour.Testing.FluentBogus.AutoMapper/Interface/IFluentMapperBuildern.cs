using AutoBogus;

using AutoMapper;

namespace NineteenSevenFour.Testing.FluentMapper.Interface;

public interface IFluentMapperBuilder<TFaker, TEntity, TModel>
    where TFaker : AutoFaker<TEntity>, new()
    where TEntity : class
    where TModel : class
{
  IFluentMapperBuilder<TFaker, TEntity, TModel> With<TProfile>()
      where TProfile : Profile, new();

  (ICollection<TEntity>, ICollection<TModel>) Generate(int count);

  (TEntity, TModel) Generate();
}
