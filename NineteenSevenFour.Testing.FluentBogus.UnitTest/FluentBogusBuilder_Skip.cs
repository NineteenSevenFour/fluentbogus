// <copyright file="FluentBogusBuilder_Skip.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest
{
  using System;
  using System.Linq.Expressions;
  using FluentAssertions;
  using NineteenSevenFour.Testing.Example.Domain.Faker;
  using NineteenSevenFour.Testing.Example.Domain.Model;

  public class FluentBogusBuilderSkip
  {
    [Fact]
    public void Should_AddPropertyToSkipList_WhenCalled_WithSinglePropertyLambdaExpression()
    {
      // Arrange
      var builder = FluentBogusBuilderExtension.Fake<PersonModel>().UseFaker<PersonFaker>();

      // Act
      builder.Skip(e => e.Addresses);

      // Assert
      var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
      Assert.NotNull(typedBuilder);
      typedBuilder.SkipProperties.Should()
        .NotBeNullOrEmpty()
        .And
        .HaveCount(1);
    }

    [Fact]
    public void Should_ThrowInvalidOperationException__WhenCalled_WithDuplicateSinglePropertyLambdaExpression()
    {
      // Arrange
      var builder = FluentBogusBuilderExtension.Fake<PersonModel>().UseFaker<PersonFaker>();
      builder.Skip(e => e.Addresses);

      // Act
#pragma warning disable IDE0039 // Use local function
      var result = () => builder.Skip(e => e.Addresses);
#pragma warning restore IDE0039 // Use local function

      // Assert
      var exception = Assert.Throws<InvalidOperationException>(result);
      exception.Should().NotBeNull();
      exception.Message.Should().Be($"The property Addresses for type PersonModel is already set to be skipped.");
    }

    [Fact]
    public void Should_AddPropertiesToSkipList_WhenCalled_WithMultiplePropertyLambdaExpression()
    {
      // Arrange
      var builder = FluentBogusBuilderExtension.Fake<PersonModel>().UseFaker<PersonFaker>();

      // Act
      builder.Skip(e => e.Addresses, equals => equals.Relatives);

      // Assert
      var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
      Assert.NotNull(typedBuilder);
      typedBuilder.SkipProperties.Should()
        .NotBeNullOrEmpty()
        .And
        .HaveCount(2);
    }

    [Fact]
    public void Should_ThrowArgumentOutOfRangeException_WhenCalled_WithMultipleNullPropertyLambdaExpression()
    {
      // Arrange
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
      Expression<Func<PersonModel, object?>> property1 = null;
      Expression<Func<PersonModel, object?>> property2 = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
      var builder = FluentBogusBuilderExtension.Fake<PersonModel>().UseFaker<PersonFaker>();

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
    public void Should_ThrowArgumentOutOfRangeException_WhenCalled_WithEmptyArrayOfPropertyLambdaExpression()
    {
      // Arrange
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
      Expression<Func<PersonModel, object?>>[] properties = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
      var builder = FluentBogusBuilderExtension.Fake<PersonModel>().UseFaker<PersonFaker>();

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
}
