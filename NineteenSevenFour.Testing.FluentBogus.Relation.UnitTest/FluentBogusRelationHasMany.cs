// <copyright file="FluentBogusRelationHasMany.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation.UnitTest;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentAssertions;
using NineteenSevenFour.Testing.Example.Domain.Model;
using Xunit;

public class FluentBogusRelationHasMany
{
  [Fact]
  public void ShouldThrowArgumentNullExceptionWhenCalledFromNullSource()
  {
    // Arrange
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    PersonModel person = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

    // Act
#pragma warning disable IDE0039 // Use local function
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8604 // Possible null reference argument.
    var result = () => person.HasMany<PersonModel, ICollection<AddressModel>>(null);
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
    var person = new PersonModel();

    // Act
#pragma warning disable IDE0039 // Use local function
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    var result = () => person.HasMany<PersonModel, ICollection<AddressModel>>(null);
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
    var person = new PersonModel(); // Addresses collection is not set in PersonModel

    // Act
    var hasManyRelation = (FluentBogusRelationManyToAny<PersonModel, PersonRelativeModel>)person.HasMany(p => p.Relatives);

    // Assert
    hasManyRelation.Should().NotBeNull().And.BeOfType<FluentBogusRelationManyToAny<PersonModel, PersonRelativeModel>>();
    hasManyRelation.Dependency.Should().BeNullOrEmpty();
    hasManyRelation.Source.Should().Be(person);
  }

  [Fact]
  public void ShouldSetDependencyWhenCalledWithInitializedDependency()
  {
    // Arrange
    var person = new PersonModel() { Relatives = new Collection<PersonRelativeModel>() };

    // Act
    var hasManyRelation = (FluentBogusRelationManyToAny<PersonModel, PersonRelativeModel>)person.HasMany(p => p.Relatives);

    // Assert
    hasManyRelation.Should().NotBeNull().And.BeOfType<FluentBogusRelationManyToAny<PersonModel, PersonRelativeModel>>();
    hasManyRelation.Dependency.Should().BeOfType<Collection<PersonRelativeModel>>();
    hasManyRelation.Source.Should().Be(person);
  }
}
