// <copyright file="FluentBogusBuilder_WithDefault.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest
{
  using FluentAssertions;
  using NineteenSevenFour.Testing.Example.Domain.Model;

  public class FluentBogusBuilderWithDefault
  {
    [Fact]
    public void Should_Return_ABuilder_WithDefaultAutoFaker_WhenCalled_WithoutArguments()
    {
      // Arrange

      // Act
      var builder = FluentBogusBuilderExtension.Fake<PersonModel>().UseDefault();

      // Assert
      builder.Should()
        .NotBeNull().And
        .BeOfType<FluentBogusBuilder<AutoFaker<PersonModel>, PersonModel>>();
    }

    [Fact]
    public void Should_StoreArguments_WhenCalled_WithArguments()
    {
      // Arrange
      var args = new[] { "argOne", "argTwo" };

      // Act
      var builder = FluentBogusBuilderExtension.Fake<PersonModel>().UseDefault(args);

      // Assert
      var typedBuilder = builder as FluentBogusBuilder<AutoFaker<PersonModel>, PersonModel>;
      Assert.NotNull(typedBuilder);
      typedBuilder.FakerArgs.Should()
        .NotBeEmpty().And
        .HaveCount(args.Length);
    }
  }
}
