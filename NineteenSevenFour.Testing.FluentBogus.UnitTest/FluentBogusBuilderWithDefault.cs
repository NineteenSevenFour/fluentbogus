// <copyright file="FluentBogusBuilderWithDefault.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

using FluentAssertions;
using NineteenSevenFour.Testing.Example.Domain.Model;

public class FluentBogusBuilderWithDefault
{
  [Fact]
  public void ShouldReturnABuilderWithDefaultAutoFakerWhenCalledWithoutArguments()
  {
    // Arrange

    // Act
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker();

    // Assert
    builder.Should()
      .NotBeNull().And
      .BeOfType<FluentBogusBuilder<AutoFaker<PersonModel>, PersonModel>>();
  }

  [Fact]
  public void ShouldStoreArgumentsWhenCalledWithArguments()
  {
    // Arrange
    var args = new[] { "argOne", "argTwo" };

    // Act
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker(args);

    // Assert
    var typedBuilder = builder as FluentBogusBuilder<AutoFaker<PersonModel>, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.FakerArgs.Should()
      .NotBeEmpty().And
      .HaveCount(args.Length);
  }
}
