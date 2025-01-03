using System.Reflection.Metadata;

namespace NineteenSevenFour.Testing.FluentBogus.Beta;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using AutoBogus;

public interface IFluentBogusOptionBeta<TEntity>
  where TEntity : class
{
}

public class FluentBogusBuilderOptionBeta<TEntity> : IFluentBogusOptionBeta<TEntity>
  where TEntity : class
{
}

public class FluentBogusGeneratorBeta
{
  public static FluentBogusBuilderBeta<TEntity> CreateBuilder<TEntity>()
    where TEntity : class => new(new FluentBogusBuilderOptionBeta<TEntity>());

  public static FluentBogusBuilderBeta<TEntity> CreateBuilder<TEntity>(IFluentBogusOptionBeta<TEntity> options)
    where TEntity : class => new(options);
}

public interface IFluentBogusGeneratorBeta<TEntity>
  where TEntity : class
{
  /// <summary>
  /// Generates a collection of <typeparamref name="TEntity"/>.
  /// </summary>
  /// <param name="count">The number of <typeparamref name="TEntity"/> to generates.</param>
  /// <returns>The <see cref="ICollection{TEntity}"/> instance.</returns>
  ICollection<TEntity> Generate(int count);

  /// <summary>
  /// Generates a single <typeparamref name="TEntity"/>.
  /// </summary>
  /// <returns>A <typeparamref name="TEntity"/>.</returns>
  TEntity Generate();
}

public class FluentBogusGeneratorBeta<TEntity> : IFluentBogusGeneratorBeta<TEntity>
  where TEntity : class
{
  public ICollection<TEntity> Generate(int count)
  {
    throw new NotImplementedException();
  }

  public TEntity Generate()
  {
    throw new NotImplementedException();
  }
}

public class FluentBogusBuilderBeta<TEntity>
    where TEntity : class
{
  public FluentBogusBuilderBeta(IFluentBogusOptionBeta<TEntity> options)
  {
    throw new NotImplementedException();
  }

  public FluentBogusGeneratorBeta<TEntity> Configure()
  {
    throw new NotImplementedException();
  }
}
