// <copyright file="FluentBogusBuilderGenerate.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Faker;
using NineteenSevenFour.Testing.Example.Domain.Model;

public class FluentBogusBuilderGenerate
{
  [Fact]
  public void ShouldReturnAnInstanceOfEntityWhenCalledWithoutSkipOrConfigOrSeed()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>();
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;

    // Act
    var person = builder.Generate();

    // Assert
    person.Should().NotBeNull();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    typedBuilder.SkipProperties.Should().HaveCount(0);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
  }

  [Fact]
  public void ShouldReturnAnInstanceOfEntityWhenCalledWithSkip()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>();
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;

    // Act
    var person = builder.Skip(p => p.Relatives).Generate();

    // Assert
    person.Should().NotBeNull();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    typedBuilder.SkipProperties.Should().HaveCount(1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
  }

  [Fact]
  public void ShouldReturnAnInstanceOfEntityWhenCalledWithConfig()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>();
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;

    // Act
    var person = builder.UseFakerConfig(b => b.WithTreeDepth(1)).Generate();

    // Assert
    person.Should().NotBeNull();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    typedBuilder.SkipProperties.Should().HaveCount(0);
    typedBuilder.FakerConfigBuilder.Should().NotBeNull();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
  }

  [Fact]
  public void ShouldReturnAnArrayOfInstanceOfEntityWhenCalledWithoutSkipOrConfigOrSeed()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>();
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;

    // Act
    var persons = builder.Generate(2);

    // Assert
    persons.Should().NotBeNullOrEmpty().And.HaveCount(2);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    typedBuilder.SkipProperties.Should().HaveCount(0);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
  }

  [Fact]
  public void ShouldReturnAnArrayOfInstanceOfEntityWhenCalledWithSkip()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>();
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;

    // Act
    var persons = builder.Skip(p => p.Relatives).Generate(2);

    // Assert
    persons.Should().NotBeNullOrEmpty().And.HaveCount(2);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    typedBuilder.SkipProperties.Should().HaveCount(1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
  }

  [Fact]
  public void ShouldReturnAnArrayOfInstanceOfEntityWhenCalledWithConfig()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>();
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;

    // Act
    var persons = builder.UseFakerConfig(b => b.WithTreeDepth(1)).Generate(2);

    // Assert
    persons.Should().NotBeNullOrEmpty().And.HaveCount(2);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    typedBuilder.SkipProperties.Should().HaveCount(0);
    typedBuilder.FakerConfigBuilder.Should().NotBeNull();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
  }
}
