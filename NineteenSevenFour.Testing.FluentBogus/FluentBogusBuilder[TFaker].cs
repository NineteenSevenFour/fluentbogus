using AutoBogus;
using AutoBogus.Moq;

using NineteenSevenFour.Testing.Core.Extension;
using NineteenSevenFour.Testing.FluentBogus.Interface;

using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus
{
  public class FluentBogusBuilder<TFaker, TEntity> : FluentBogusBuilder<TEntity>, IFluentBogusBuilder<TFaker, TEntity>
      where TFaker : AutoFaker<TEntity>, new()
      where TEntity : class
  {
    private readonly Dictionary<string, dynamic> _rulesFor = new Dictionary<string, dynamic>();
    private AutoFaker<TEntity>? _faker;
    internal readonly List<string> SkipProperties = new List<string>();
    internal readonly List<string> RuleSets = new List<string>();
    internal Action<IAutoGenerateConfigBuilder>? FakerConfigBuilder;
    internal object?[]? FakerArgs;
    internal int Seed;
    internal string RuleSetString => string.Join(",", RuleSets);

    private void SkipInternal<TProperty>(Expression<Func<TEntity, TProperty>> expr)
    {
      var propertyOrFieldName = FluentExpression.MemberNameFor(expr);
      FluentExpression.EnsureMemberExists<TEntity>(propertyOrFieldName);

#pragma warning disable CS8604 // Possible null reference argument.
      if (!SkipProperties.Contains(propertyOrFieldName))
#pragma warning restore CS8604 // Possible null reference argument.
      {
        SkipProperties.Add(propertyOrFieldName);
      }
      else
      {
        throw new InvalidOperationException($"The property {propertyOrFieldName} for type {typeof(TEntity).Name} is already set to be skipped.");
      }
    }

    private void UseRuleSetInternal(IEnumerable<string> ruleSets)
    {
      foreach (var ruleSet in ruleSets)
      {
        UseRuleSetInternal(ruleSet);
      }
    }

    private void UseRuleSetInternal(string ruleset)
    {
      // TODO: Validate RuleSet against TFaker
      if (string.IsNullOrWhiteSpace(ruleset))
      {
        throw new ArgumentOutOfRangeException(nameof(ruleset), $"A ruleset must be provided.");
      }

      if (!RuleSets.Contains(ruleset))
      {
        RuleSets.Add(ruleset);
      }
      else
      {
        throw new InvalidOperationException($"The ruleset {ruleset} is already set to be used.");
      }
    }

    private void ConfigureFakerInternal(IAutoGenerateConfigBuilder builder)
    {
      // Call user define Faker configuration overrides
      FakerConfigBuilder?.Invoke(builder);

      // Force 
      builder.WithBinder<MoqBinder>();  // Configures the bind

      // Configures members to be skipped for a typeer to use
      foreach (var propName in SkipProperties)
      {
        builder.WithSkip<TEntity>(propName);
      }
    }

    private void EnsureFakerInternal()
    {
      _faker = (AutoFaker<TEntity>)Activator.CreateInstance(typeof(TFaker), FakerArgs);
    }

    internal FluentBogusBuilder(FluentBogusBuilder<TFaker, TEntity> fluentBogus)
    {
      Seed = fluentBogus.Seed;
      FakerArgs = fluentBogus.FakerArgs;
      SkipProperties = fluentBogus.SkipProperties;
      RuleSets = fluentBogus.RuleSets;
      FakerConfigBuilder = fluentBogus.FakerConfigBuilder;
      _rulesFor = fluentBogus._rulesFor;
    }

    public FluentBogusBuilder(params object?[]? args)
    {
      FakerArgs = args;
    }

    /// <inheritdoc/>>
    public ICollection<TEntity> Generate(int count)
    {
      EnsureFakerInternal();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
      return _faker
#pragma warning restore CS8602 // Dereference of a possibly null reference.
          .Configure(ConfigureFakerInternal)
          .UseSeed(Seed)
          .Generate(count, RuleSetString);
    }

    /// <inheritdoc/>>
    public TEntity Generate()
    {
      EnsureFakerInternal();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
      return _faker
#pragma warning restore CS8602 // Dereference of a possibly null reference.
          .Configure(ConfigureFakerInternal)
          .UseSeed(Seed)
          .Generate(RuleSetString);
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> Skip(params Expression<Func<TEntity, object?>>[] properties)
    {
      if (properties?.All(expr => expr == null) ?? true)
      {
        throw new ArgumentOutOfRangeException(nameof(properties), $"A List of properties must be provided.");
      }

      foreach (var property in properties)
      {
        SkipInternal(property);
      }

      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> Skip<TProperty>(Expression<Func<TEntity, TProperty>> property)
    {
      SkipInternal(property);
      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> UseRuleSet(string ruleset)
    {
      UseRuleSetInternal(ruleset);
      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> UseRuleSet(params string[] rulesets)
    {
      if (rulesets?.All(string.IsNullOrWhiteSpace) ?? true)
      {
        throw new ArgumentOutOfRangeException(nameof(rulesets), $"A List of ruleset must be provided.");
      }

      UseRuleSetInternal(rulesets);

      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> UseSeed(int seed)
    {
      this.Seed = seed;
      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> UseArgs(object?[]? args)
    {
      this.FakerArgs = args;
      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> UseConfig(Action<IAutoGenerateConfigBuilder> configBuilder)
    {
      this.FakerConfigBuilder = configBuilder;
      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> RuleFor<TProperty, TPropEntity, TPropFaker>(
        Expression<Func<TEntity, TProperty>> property,
        IFluentBogusBuilder<TPropFaker, TPropEntity>? builder,
        int count)
        where TProperty : ICollection<TPropEntity?>?
        where TPropEntity : class
        where TPropFaker : AutoFaker<TPropEntity>, new()
    {
      var propOrFieldName = FluentExpression.MemberNameFor(property);
      FluentExpression.EnsureMemberExists<TEntity>(propOrFieldName);

      if (builder == null)
      {
        Skip(e => property);
      }
      else
      {
#pragma warning disable CS8604 // Possible null reference argument.
        _rulesFor.Add(propOrFieldName, builder.Generate(count));
#pragma warning restore CS8604 // Possible null reference argument.
      }
      return this;
    }

    /// <inheritdoc/>>
    public IFluentBogusBuilder<TFaker, TEntity> RuleFor<TProperty, TPropFaker>(
        Expression<Func<TEntity, TProperty?>> property,
        IFluentBogusBuilder<TPropFaker, TProperty>? builder)
        where TProperty : class
        where TPropFaker : AutoFaker<TProperty>, new()
    {
      var propOrFieldName = FluentExpression.MemberNameFor(property);
      FluentExpression.EnsureMemberExists<TEntity>(propOrFieldName);

      if (builder == null)
      {
        Skip(e => property);
      }
      else
      {

#pragma warning disable CS8604 // Possible null reference argument.
        _rulesFor.Add(propOrFieldName, builder.Generate());
#pragma warning restore CS8604 // Possible null reference argument.           
      }
      return this;
    }

  }
}
