// <copyright file="FluentBogusBuilderExtension.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus
{
  /// <summary>
  /// Extension for <see cref="FluentBogusBuilder{TEntity}"/>./>
  /// </summary>
  public static class FluentBogusBuilderExtension
  {
    /// <summary>
    /// Create a <see cref="FluentBogusBuilder{TEntity}"/> for an TEntity to allow bogus data to be generated.
    /// </summary>
    /// <typeparam name="TEntity">The type of the class entity to fake.</typeparam>
    /// <returns>A <see cref="FluentBogusBuilder{TEntity}"/>.</returns>
    public static IFluentBogusBuilder<TEntity> Fake<TEntity>()
      where TEntity : class => new FluentBogusBuilder<TEntity>();
  }
}
