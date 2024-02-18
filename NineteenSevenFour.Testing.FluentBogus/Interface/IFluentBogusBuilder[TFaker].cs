using AutoBogus;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.Interface
{
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

    IFluentBogusBuilder<TFaker, TEntity> RuleFor<TProperty, TPropEntity, TPropFaker>(
        Expression<Func<TEntity, TProperty>> property,
        IFluentBogusBuilder<TPropFaker, TPropEntity> builder,
        int count)
        where TPropEntity : class
        where TProperty : ICollection<TPropEntity?>?
        where TPropFaker : AutoFaker<TPropEntity>, new();

    IFluentBogusBuilder<TFaker, TEntity> RuleFor<TProperty, TPropFaker>(
        Expression<Func<TEntity, TProperty?>> property,
        IFluentBogusBuilder<TPropFaker, TProperty> builder)
        where TProperty : class
        where TPropFaker : AutoFaker<TProperty>, new();

    ICollection<TEntity> Generate(int count);

    TEntity Generate();
  }
}
