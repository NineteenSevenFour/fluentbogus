// <copyright file="FluentBogusRelationManyToAnyHasKey.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation.UnitTest;

using System;
using System.Collections.ObjectModel;
using FluentAssertions;
using NineteenSevenFour.Testing.Example.Domain.Model;
using Xunit;

public class FluentBogusRelationManyToAnyHasKey
{
  [Fact]
  public void ShouldThrowArgumentNullExceptionWhenCalledWithNullExpression()
  {
    // Arrange
    var person = new PersonModel();

    // Act
#pragma warning disable IDE0039 // Use local function
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    var result = () =>
      person.HasMany(p => p.Relatives)
        .HasKey<int>(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore IDE0039 // Use local function

    // Assert
    var exception = Assert.Throws<ArgumentNullException>(result);
    exception.Should().NotBeNull();
    exception.Message.Should().Be($"Value cannot be null. (Parameter 'sourceKeyExpression')");
  }

  [Fact]
  public void ShouldSetSourceKeyExpressionWhenCalledWithValidExpression()
  {
    // Arrange
    var person = new PersonModel() { Relatives = new Collection<PersonRelativeModel>() };

    // Act
    var hasManyWithKeyRelation = person.HasMany(p => p.Relatives).HasKey(p => p.Id);

    // Assert
    hasManyWithKeyRelation.Should().NotBeNull().And.BeOfType<FluentBogusRelationManyToAny<PersonModel, AddressModel, int?>?>();
    ((FluentBogusRelationManyToAny<PersonModel, AddressModel, int?>)hasManyWithKeyRelation).SourceKeyExpression.Should().NotBeNull();
  }
}
