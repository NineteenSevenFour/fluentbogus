using AutoBogus;
using AutoBogus.Moq;

using NineteenSevenFour.Testing.Core.Extension;
using NineteenSevenFour.Testing.FluentBogus.Interface;

using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus;

public class FluentBogusBuilder<TFaker, TEntity> : FluentBogusBuilder<TEntity>, IFluentBogusBuilder<TFaker, TEntity>
    where TFaker : AutoFaker<TEntity>, new()
    where TEntity : class
{
  internal Action<IAutoGenerateConfigBuilder>? fakerConfigBuilder;
  internal AutoFaker<TEntity>? faker;
  internal object?[]? fakerArgs;
  internal int seed;
  internal readonly List<string> skipProperties = new();
  internal readonly List<string> ruleSets = new();
  internal string RuleSetString => string.Join(",", ruleSets);

  internal void SkipInternal<TProperty>(Expression<Func<TEntity, TProperty>> expr)
  {
    var propertyOrFieldName = FluentExpression.MemberNameFor(expr);
    FluentExpression.EnsureMemberExists<TEntity>(propertyOrFieldName);

#pragma warning disable CS8604 // Possible null reference argument.
    if (!skipProperties.Contains(propertyOrFieldName))
#pragma warning restore CS8604 // Possible null reference argument.
    {
      skipProperties.Add(propertyOrFieldName);
    }
    else
    {
      throw new InvalidOperationException($"The property {propertyOrFieldName} for type {typeof(TEntity).Name} is already set to be skipped.");
    }
  }

  internal void SkipInternal(IEnumerable<string> properties)
  {
    foreach (var propertyOrFieldName in properties)
    {
      FluentExpression.EnsureMemberExists<TEntity>(propertyOrFieldName);
      if (!skipProperties.Contains(propertyOrFieldName))
      {
        skipProperties.Add(propertyOrFieldName);
      }
      else
      {
        throw new InvalidOperationException($"The property {propertyOrFieldName} for type {typeof(TEntity).Name} is already set to be skipped.");
      }
    }
  }

  internal void UseRuleSetInternal(IEnumerable<string> ruleSets)
  {
    foreach (var ruleSet in ruleSets)
    {
      UseRuleSetInternal(ruleSet);
    }
  }

  internal void UseRuleSetInternal(string ruleset)
  {
    // TODO: Validate RuleSet against TFaker
    if (string.IsNullOrWhiteSpace(ruleset))
    {
      throw new ArgumentOutOfRangeException(nameof(ruleset), $"A ruleset must be provided.");
    }

    if (!ruleSets.Contains(ruleset))
    {
      ruleSets.Add(ruleset);
    }
    else
    {
      throw new InvalidOperationException($"The ruleset {ruleset} is already set to be used.");
    }
  }

  internal void ConfigureFakerInternal(IAutoGenerateConfigBuilder builder)
  {
    // Call user define Faker configuration overrides
    fakerConfigBuilder?.Invoke(builder);

    // Force 
    builder.WithBinder<MoqBinder>();  // Configures the bind

    // Configures members to be skipped for a typeer to use
    foreach (var propName in skipProperties)
    {
      builder.WithSkip<TEntity>(propName);
    }
  }

  internal void EnsureFaker()
  {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    faker = (AutoFaker<TEntity>)Activator.CreateInstance(typeof(TFaker), fakerArgs);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

    if (faker == null) throw new NullReferenceException("The faker instance is not set. Generation of data is impossible.");
  }

  internal FluentBogusBuilder(FluentBogusBuilder<TFaker, TEntity> fluentBogus)
  {
    UseSeed(fluentBogus.seed);
    UseArgs(fluentBogus.fakerArgs);
    SkipInternal(fluentBogus.skipProperties);
    UseRuleSetInternal(fluentBogus.ruleSets);
    fakerConfigBuilder = fluentBogus.fakerConfigBuilder;
  }

  public FluentBogusBuilder(params object?[]? args)
  {
    fakerArgs = args;
  }

  /// <inheritdoc/>>
  public ICollection<TEntity> Generate(int count)
  {
    EnsureFaker();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    return faker
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        .Configure((builder) => ConfigureFakerInternal(builder))
        .UseSeed(seed)
        .Generate(count, RuleSetString);
  }

  /// <inheritdoc/>>
  public TEntity Generate()
  {
    EnsureFaker();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    return faker
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        .Configure((builder) => ConfigureFakerInternal(builder))
        .UseSeed(seed)
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
    if (rulesets?.All(r => string.IsNullOrWhiteSpace(r)) ?? true)
    {
      throw new ArgumentOutOfRangeException(nameof(rulesets), $"A List of ruleset must be provided.");
    }

    foreach (var rule in rulesets)
    {
      UseRuleSetInternal(rule);
    }
    return this;
  }

  /// <inheritdoc/>>
  public IFluentBogusBuilder<TFaker, TEntity> UseSeed(int seed)
  {
    this.seed = seed;
    return this;
  }

  /// <inheritdoc/>>
  public IFluentBogusBuilder<TFaker, TEntity> UseArgs(object?[]? args)
  {
    this.fakerArgs = args;
    return this;
  }

  /// <inheritdoc/>>
  public IFluentBogusBuilder<TFaker, TEntity> UseConfig(Action<IAutoGenerateConfigBuilder> configBuilder)
  {
    this.fakerConfigBuilder = configBuilder;
    return this;
  }
}
