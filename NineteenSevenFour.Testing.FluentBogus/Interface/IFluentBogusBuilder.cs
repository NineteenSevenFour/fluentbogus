using AutoBogus;

using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Interface;


public interface IFluentBogusBuilder<TEntity>
    where TEntity : class
{
  IFluentBogusBuilder<AutoFaker<TEntity>, TEntity> WithDefault(params object?[]? args);

  IFluentBogusBuilder<TFaker, TEntity> With<TFaker>(params object?[]? args)
      where TFaker : AutoFaker<TEntity>, new();
}

public interface IFluentBogusBuilder<TFaker, TEntity>
    where TFaker : AutoFaker<TEntity>, new()
    where TEntity : class
{
  IFluentBogusBuilder<TFaker, TEntity> UseSeed(int seed);

  IFluentBogusBuilder<TFaker, TEntity> UseArgs(object?[]? args);

  IFluentBogusBuilder<TFaker, TEntity> UseConfig(Action<IAutoGenerateConfigBuilder> configBuilder);

  IFluentBogusBuilder<TFaker, TEntity> UseRuleSet(string ruleset);

  IFluentBogusBuilder<TFaker, TEntity> UseRuleSet(params string[] rulesets);

  IFluentBogusBuilder<TFaker, TEntity> Skip(params Expression<Func<TEntity, object?>>[] properties);

  IFluentBogusBuilder<TFaker, TEntity> Skip<TProperty>(Expression<Func<TEntity, TProperty>> property);

  // TODO: Add RuleFor wrapper to pass in FluentBogusBuilder

  ICollection<TEntity> Generate(int count);

  TEntity Generate();
}
