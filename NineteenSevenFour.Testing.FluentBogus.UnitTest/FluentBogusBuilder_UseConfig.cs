// <copyright file="FluentBogusBuilder_UseConfig.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest
{
  using System;
  using FluentAssertions;
  using NineteenSevenFour.Testing.Example.Domain.Faker;
  using NineteenSevenFour.Testing.Example.Domain.Model;

  public class FluentBogusBuilderUseConfig
  {
    [Fact]
    public void Should_StoreFakerConfig_WhenCalled()
    {
      // Arrange
#pragma warning disable IDE0039 // Use local function
      Action<IAutoGenerateConfigBuilder> fakerConfig = (config) => config.WithTreeDepth(0);
#pragma warning restore IDE0039 // Use local function
      var builder = FluentBogusBuilderExtension.Fake<PersonModel>().UseFaker<PersonFaker>();

      // Act
      builder.UseConfig(fakerConfig);

      // Assert
      var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
      Assert.NotNull(typedBuilder);
      typedBuilder.FakerConfigBuilder.Should().NotBeNull().And.BeEquivalentTo(fakerConfig);
    }
  }
}
