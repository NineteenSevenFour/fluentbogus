// <copyright file="FluentBogusBuilderSkip.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

using System;
using System.Linq.Expressions;

using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Faker;
using NineteenSevenFour.Testing.Example.Domain.Model;

public class FluentBogusBuilderSkip
{
  [Fact]
  public void ShouldAddPropertyToSkipListWhenCalledWithSinglePropertyLambdaExpression()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>();

    // Act
    builder.Skip(e => e.Relatives);

    // Assert
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.SkipProperties.Should()
      .NotBeNullOrEmpty()
      .And
      .HaveCount(1);
  }

  [Fact]
  public void ShouldThrowInvalidOperationExceptionWhenCalledWithDuplicateSinglePropertyLambdaExpression()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>();
    builder.Skip(e => e.Relatives);

    // Act
#pragma warning disable IDE0039 // Use local function
    var result = () => builder.Skip(e => e.Relatives);
#pragma warning restore IDE0039 // Use local function

    // Assert
    var exception = Assert.Throws<InvalidOperationException>(result);
    exception.Should().NotBeNull();
    exception.Message.Should().Be($"The property Relatives for type PersonModel is already set to be skipped.");
  }

  [Fact]
  public void ShouldAddPropertiesToSkipListWhenCalledWithMultiplePropertyLambdaExpression()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>();

    // Act
    builder.Skip(e => e.Address, e => e.Relatives);

    // Assert
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.SkipProperties.Should()
      .NotBeNullOrEmpty()
      .And
      .HaveCount(2);
  }

  [Fact]
  public void ShouldThrowArgumentOutOfRangeExceptionWhenCalledWithMultipleNullPropertyLambdaExpression()
  {
    // Arrange
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    Expression<Func<PersonModel, object?>> property1 = null;
    Expression<Func<PersonModel, object?>> property2 = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>();

    // Act
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable IDE0039 // Use local function
    var result = () => builder.Skip(property1, property2);
#pragma warning restore IDE0039 // Use local function
#pragma warning restore CS8604 // Possible null reference argument.

    // Assert
    var exception = Assert.Throws<ArgumentOutOfRangeException>(result);
    exception.Should()
      .NotBeNull();
    exception.Message.Should()
      .Be($"A List of properties must be provided. (Parameter 'properties')");
  }

  [Fact]
  public void ShouldThrowArgumentOutOfRangeExceptionWhenCalledWithEmptyArrayOfPropertyLambdaExpression()
  {
    // Arrange
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    Expression<Func<PersonModel, object?>>[] properties = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>();

    // Act
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable IDE0039 // Use local function
    var result = () => builder.Skip(properties);
#pragma warning restore IDE0039 // Use local function
#pragma warning restore CS8604 // Possible null reference argument.

    // Assert
    var exception = Assert.Throws<ArgumentOutOfRangeException>(result);
    exception.Should().NotBeNull();
    exception.Message.Should()
      .Be($"A List of properties must be provided. (Parameter 'properties')");
  }
}
