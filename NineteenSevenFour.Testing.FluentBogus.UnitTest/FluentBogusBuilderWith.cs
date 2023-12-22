// <copyright file="FluentBogusBuilderWith.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

using FluentAssertions;
using NineteenSevenFour.Testing.Example.Domain.Faker;
using NineteenSevenFour.Testing.Example.Domain.Model;

public class FluentBogusBuilderWith
{
  [Fact]
  public void ShouldReturnABuilderWithCustomFakerWhenCalledWithoutArguments()
  {
    // Arrange

    // Act
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>();

    // Assert
    builder.Should()
      .NotBeNull().And
      .BeOfType<FluentBogusBuilder<PersonFaker, PersonModel>>();
  }

  [Fact]
  public void ShouldStoreArgumentsWhenCalledWithArguments()
  {
    // Arrange
    var args = new[] { "argOne", "argTwo" };

    // Act
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>(args);

    // Assert
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.FakerArgs.Should()
      .NotBeEmpty().And
      .HaveCount(args.Length);
  }
}
