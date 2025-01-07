// <copyright file="FluentBogusBuilderBetaTest.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable SA1649
#pragma warning disable SA1402
#pragma warning disable SA1600
namespace NineteenSevenFour.Testing.FluentBogus.UnitTest.Beta;

using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Faker;
using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Beta;

public class FluentBogusBuilderOptionBetaTest
{
  [Fact]
  public void ShouldSetFakerArgsWhenCallingUseFakerWithArgs()
  {
    // Arrange
    var options = new FluentBogusBuilderOptionBeta<PersonModel>();
    object?[]? args = { "arg1", "arg2" };

    // Act
    options.UseFaker(args);

    // Assert
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
      .BeEquivalentTo(args);
  }
}

public class FluentBogusGeneratorBetaExtensionTest
{
  [Fact]
  public void ShouldCreateABuilderWithDefaultOptionsWhenCallingCreateBuilder()
  {
    // Arrange

    // Act
    var builder = FluentBogusGeneratorBeta.CreateBuilder<PersonModel>();

    // Assert
    var options = (FluentBogusBuilderOptionBeta<PersonModel>)builder.Options;

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

    // Act
    var builder = FluentBogusGeneratorBeta.CreateBuilder(builderOptions);

    // Assert
    var options = (FluentBogusBuilderOptionBeta<PersonModel>)builder.Options;

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

    // Act
    var builder = FluentBogusGeneratorBeta
      .CreateBuilder<PersonModel>(options =>
        options.UseFaker<PersonFaker>());

    // Assert
    var options = (FluentBogusBuilderOptionBeta<PersonModel>)builder.Options;

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

public class FluentBogusBuilderBetaExtensionTest
{
  [Fact]
  public void ShouldCreateAGeneratorWithDefaultFakerWhenCallingBuildWithDefault()
  {
    // Arrange
    var builder = FluentBogusGeneratorBeta.CreateBuilder<PersonModel>();

    // Act
    var generator = builder.Build() as FluentBogusGeneratorBeta<PersonModel>;

    // Assert
    var options = generator.Options as FluentBogusBuilderOptionBeta<PersonModel>;
    var faker = options.Faker;

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
  public void ShouldCreateAGeneratorWithCustomFakerWhenCallingBuildWithOptionsAction()
  {
    // Arrange
    var builder = FluentBogusGeneratorBeta.CreateBuilder<PersonModel>(options => options.UseFaker<PersonFaker>());

    // Act
    var generator = builder.Build() as FluentBogusGeneratorBeta<PersonModel>;

    // Assert
    var options = generator.Options as FluentBogusBuilderOptionBeta<PersonModel>;
    var faker = options.Faker;

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

public class FluentBogusGeneratorBetaTest
{
  [Fact]
  public void ShouldGenerateASingleInstanceOfPersonWhenUsingBuilderWithDefaultFaker()
  {
    // Arrange
    var builder = FluentBogusGeneratorBeta.CreateBuilder<PersonModel>();
    var generator = builder.Build() as FluentBogusGeneratorBeta<PersonModel>;

    // Act
    var person = generator.Generate();

    // Assert
    var options = generator.Options as FluentBogusBuilderOptionBeta<PersonModel>;
    var faker = options.Faker;

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

    person
      .Should()
      .NotBeNull();

    person.Relatives
      .Should()
      .NotBeEmpty();

    person.Address
      .Should()
      .NotBeNull();
  }

  [Fact]
  public void ShouldGenerateASingleInstanceOfPersonWhenUsingBuilderWithCustomFaker()
  {
    // Arrange
    var builder = FluentBogusGeneratorBeta
      .CreateBuilder<PersonModel>(o => o.UseFaker<PersonFaker>());
    var generator = builder.Build() as FluentBogusGeneratorBeta<PersonModel>;

    // Act
    var person = generator.Generate();

    // Assert
    var options = generator.Options as FluentBogusBuilderOptionBeta<PersonModel>;
    var faker = options.Faker;

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

    person
      .Should()
      .NotBeNull();

    person.Relatives
      .Should()
      .BeNull();

    person.Address
      .Should()
      .BeNull();
  }

  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  [InlineData(5)]
  public void ShouldGenerateAZeroToMultipleInstanceOfPersonWhenUsingBuilderWithDefaultFaker(int genCount)
  {
    // Arrange
    var builder = FluentBogusGeneratorBeta.CreateBuilder<PersonModel>();
    var generator = builder.Build() as FluentBogusGeneratorBeta<PersonModel>;

    // Act
    var persons = generator.Generate(genCount);

    // Assert
    var options = generator.Options as FluentBogusBuilderOptionBeta<PersonModel>;
    var faker = options.Faker;

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

    persons
      .Should()
      .NotBeNull()
      .And
      .HaveCount(genCount);

    foreach (var person in persons)
    {
      person.Relatives
        .Should()
        .NotBeEmpty();

      person.Address
        .Should()
        .NotBeNull();
    }
  }

  [Fact]
  public void ShouldGenerateASingleInstanceOfPersonWhenUsingBuilderWithDefaultFakerAndFakerConfigArg()
  {
    // Arrange
    var builder = FluentBogusGeneratorBeta
      .CreateBuilder<PersonModel>(c =>
        c.UseFaker(f =>
          f.WithSkip<PersonModel>(p => p.Relatives)));
    var generator = builder.Build() as FluentBogusGeneratorBeta<PersonModel>;

    // Act
    var person = generator.Generate();

    // Assert
    var options = generator.Options as FluentBogusBuilderOptionBeta<PersonModel>;
    var faker = options.Faker;

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

    person
      .Should()
      .NotBeNull();

    person.Relatives
      .Should()
      .BeNull();

    person.Address
      .Should()
      .NotBeNull();
  }
}
