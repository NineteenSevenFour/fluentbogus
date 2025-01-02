// <copyright file="FluentBogusBuilderUseSeed.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

using FluentAssertions;
using NineteenSevenFour.Testing.Example.Domain.Faker;
using NineteenSevenFour.Testing.Example.Domain.Model;

public class FluentBogusBuilderUseSeed
{
  [Fact]
  public void ShouldStoreSeedWhenCalled()
  {
    // Arrange
    var seed = 456;
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>();

    // Act
    builder.UseSeed(seed);

    // Assert
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.Seed.Should()
      .Be(seed);
  }
}
