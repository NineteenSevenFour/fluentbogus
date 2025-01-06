// <copyright file="FluentBogusBuilderBetaTest.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1600
namespace NineteenSevenFour.Testing.FluentBogus.UnitTest.Beta;

using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Faker;
using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Beta;

public class FluentBogusGeneratorBetaTest
{
  [Fact]
  public void ShouldCreateABuilderWithDefaultOptionsWhenCallingCreateBuilder()
  {
    // Arrange
    var builder = FluentBogusGeneratorBeta.CreateBuilder<PersonModel>();

    // Act
    var options = (FluentBogusBuilderOptionBeta<PersonModel>)builder.Options;

    // Assert
    builder
      .Should()
      .NotBeNull()
      .And
      .BeOfType<FluentBogusBuilderBeta<PersonModel>>();

    options
      .Should()
      .NotBeNull();

    options
      .FakerType
      .Should()
      .BeNull();

    options
      .FakerArgs
      .Should()
      .BeNull();

    options
      .Faker
      .Should()
      .BeNull();

    options
      .FakerConfiguration
      .Should()
      .BeNull();

    options
      .SkipProperties
      .Should()
      .BeEmpty();

    options
      .RuleSets
      .Should()
      .BeEmpty();

    options
      .RulesFor
      .Should()
      .BeEmpty();
  }

  [Fact]
  public void ShouldCreateABuilderWithCustomFakerWhenCallingCreateBuilderWithOptions()
  {
    // Arrange
    var builderOptions = new FluentBogusBuilderOptionBeta<PersonModel>();
    builderOptions.UseFaker<PersonFaker>();
    var builder = FluentBogusGeneratorBeta.CreateBuilder(builderOptions);

    // Act
    var options = (FluentBogusBuilderOptionBeta<PersonModel>)builder.Options;

    // Assert
    builder
      .Should()
      .NotBeNull()
      .And
      .BeOfType<FluentBogusBuilderBeta<PersonModel>>();

    options
      .Should()
      .NotBeNull();

    options
      .FakerType
      .Should()
      .NotBeNull()
      .And
      .Be<PersonFaker>();

    options
      .FakerArgs
      .Should()
      .BeEmpty();

    options
      .Faker
      .Should()
      .BeNull();

    options
      .FakerConfiguration
      .Should()
      .BeNull();

    options
      .SkipProperties
      .Should()
      .BeEmpty();

    options
      .RuleSets
      .Should()
      .BeEmpty();

    options
      .RulesFor
      .Should()
      .BeEmpty();
  }

  [Fact]
  public void ShouldCreateABuilderWithCustomFakerWhenCallingCreateBuilderWithOptionsAction()
  {
    // Arrange
    var builder = FluentBogusGeneratorBeta.CreateBuilder<PersonModel>(options => options.UseFaker<PersonFaker>());

    // Act
    var options = (FluentBogusBuilderOptionBeta<PersonModel>)builder.Options;

    // Assert
    builder
      .Should()
      .NotBeNull()
      .And
      .BeOfType<FluentBogusBuilderBeta<PersonModel>>();

    options
      .Should()
      .NotBeNull();

    options
      .FakerType
      .Should()
      .NotBeNull()
      .And
      .Be<PersonFaker>();

    options
      .FakerArgs
      .Should()
      .BeEmpty();

    options
      .Faker
      .Should()
      .BeNull();

    options
      .FakerConfiguration
      .Should()
      .BeNull();

    options
      .SkipProperties
      .Should()
      .BeEmpty();

    options
      .RuleSets
      .Should()
      .BeEmpty();

    options
      .RulesFor
      .Should()
      .BeEmpty();
  }
}

public class FluentBogusBuilderBetaTest
{
  [Fact]
  public void ShouldCreateAGeneratorWithDefaultFakerWhenCallingBuild()
  {
    // Arrange
    var builder = FluentBogusGeneratorBeta.CreateBuilder<PersonModel>();

    // Act
    var generator = builder.Build() as FluentBogusGeneratorBeta<PersonModel>;
    var options = generator.Options as FluentBogusBuilderOptionBeta<PersonModel>;
    var faker = options.Faker;

    // Assert
    builder
      .Should()
      .NotBeNull()
      .And
      .BeOfType<FluentBogusBuilderBeta<PersonModel>>();

    generator
      .Should()
      .NotBeNull()
      .And
      .BeOfType<FluentBogusGeneratorBeta<PersonModel>>();

    faker
      .Should()
      .NotBeNull()
      .And
      .BeOfType<AutoFaker<PersonModel>>();
  }

  [Fact]
  public void ShouldCreateAGeneratorWithCustomFakerWhenCallingBuild()
  {
    // Arrange
    var builder = FluentBogusGeneratorBeta.CreateBuilder<PersonModel>(options => options.UseFaker<PersonFaker>());

    // Act
    var generator = builder.Build() as FluentBogusGeneratorBeta<PersonModel>;
    var options = generator.Options as FluentBogusBuilderOptionBeta<PersonModel>;
    var faker = options.Faker;

    // Assert
    builder
      .Should()
      .NotBeNull()
      .And
      .BeOfType<FluentBogusBuilderBeta<PersonModel>>();

    generator
      .Should()
      .NotBeNull()
      .And
      .BeOfType<FluentBogusGeneratorBeta<PersonModel>>();

    faker
      .Should()
      .NotBeNull()
      .And
      .BeOfType<PersonFaker>();
  }
}
