// <copyright file="FluentBogusRelationManyToAnyWithOne.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation.UnitTest;

using System;
using System.Collections.ObjectModel;
using FluentAssertions;
using NineteenSevenFour.Testing.Example.Domain.Model;
using Xunit;

public class FluentBogusRelationManyToAnyWithOne
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
      person.HasMany(p => p.Addresses)
        .HasKey(p => p.Id)
        .WithOne(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore IDE0039 // Use local function

    // Assert
    var exception = Assert.Throws<ArgumentNullException>(result);
    exception.Should().NotBeNull();
    exception.Message.Should().Be($"Value cannot be null. (Parameter 'withOneExpression')");
  }

  [Fact]
  public void ShouldSetSourceRefExpressionWhenCalledWithSourceExpression()
  {
    // Arrange
    var person = new PersonModel() { Addresses = new Collection<AddressModel>() };

    // Act
    var hasManyWithOneRelation =
      person.HasMany(p => p.Addresses)
        .HasKey(p => p.Id)
        .WithOne(a => a.Person);

    // Assert
    hasManyWithOneRelation.Should().NotBeNull().And.BeOfType<FluentBogusRelationManyToOne<PersonModel, AddressModel, int?>?>();
    hasManyWithOneRelation.SourceRefExpression.Should().NotBeNull();
    var sourceRefExpression = hasManyWithOneRelation.SourceRefExpression?.Compile();
    sourceRefExpression.Should().NotBeNull().And.BeOfType<Func<AddressModel, PersonModel>>();
  }
}
