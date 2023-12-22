// <copyright file="FluentBogusRelationHasOne.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation.UnitTest;

using System;
using FluentAssertions;
using NineteenSevenFour.Testing.Example.Domain.Model;
using Xunit;

public class FluentBogusRelationHasOne
{
  [Fact]
  public void ShouldThrowArgumentNullExceptionWhenCalledFromNullSource()
  {
    // Arrange
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    AddressModel address = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

    // Act
#pragma warning disable IDE0039 // Use local function
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8604 // Possible null reference argument.
    var result = () => address.HasOne<AddressModel, PersonModel>(null);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore IDE0039 // Use local function

    // Assert
    var exception = Assert.Throws<ArgumentNullException>(result);
    exception.Should().NotBeNull();
    exception.Message.Should().Be($"Value cannot be null. (Parameter 'depExpr')");
  }

  [Fact]
  public void ShouldThrowArgumentNullExceptionWhenCalledWithNullExpression()
  {
    // Arrange
    var address = new AddressModel();

    // Act
#pragma warning disable IDE0039 // Use local function
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    var result = () => address.HasOne<AddressModel, PersonModel>(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore IDE0039 // Use local function

    // Assert
    var exception = Assert.Throws<ArgumentNullException>(result);
    exception.Should().NotBeNull();
    exception.Message.Should().Be($"Value cannot be null. (Parameter 'depExpr')");
  }

  [Fact]
  public void ShouldNotSetDependencyWhenCalledWithNonInitializedDependency()
  {
    // Arrange
    var address = new AddressModel(); // Addresses collection is not set in PersonModel

    // Act
    var hasOneRelation = (FluentBogusRelationOneToAny<AddressModel, PersonModel>)address.HasOne(a => a.Owner);

    // Assert
    hasOneRelation.Should().NotBeNull().And.BeOfType<FluentBogusRelationOneToAny<AddressModel, PersonModel>>();
    hasOneRelation.Dependency.Should().BeNull();
    hasOneRelation.Source.Should().Be(address);
  }

  [Fact]
  public void ShouldSetDependencyWhenCalledWithInitializedDependency()
  {
    // Arrange
    var address = new AddressModel() { Owner = new PersonModel() };

    // Act
    var hasOneRelation = (FluentBogusRelationOneToAny<AddressModel, PersonModel>)address.HasOne(p => p.Owner);

    // Assert
    hasOneRelation.Should().NotBeNull().And.BeOfType<FluentBogusRelationOneToAny<AddressModel, PersonModel>>();
    hasOneRelation.Dependency.Should().BeOfType<PersonModel>();
    hasOneRelation.Source.Should().Be(address);
  }
}
