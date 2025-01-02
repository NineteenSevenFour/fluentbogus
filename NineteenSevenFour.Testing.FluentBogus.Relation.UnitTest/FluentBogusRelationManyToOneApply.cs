// <copyright file="FluentBogusRelationManyToOneApply.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation.UnitTest;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using FluentAssertions;
using NineteenSevenFour.Testing.Example.Domain.Faker;
using NineteenSevenFour.Testing.Example.Domain.Model;
using Xunit;

public class FluentBogusRelationManyToOneApply
{
  [Fact]
  public void ShouldSkipWhenCalledWithNullDependency()
  {
    // Arrange
    var person = new PersonModel() { Addresses = null };

    // Act
    person.HasMany(p => p.Addresses)
      .HasKey(p => p.Id)
      .WithOne(a => a.Person)
      .WithForeignKey(a => a.PersonId)
      .Apply();

    // Assert
    person.Should().NotBeNull();
    person.Addresses.Should().BeNull();
  }

  [Fact]
  public void ShouldSkipWhenCalledWithNoDependency()
  {
    // Arrange
    var person = new PersonModel() { Addresses = [] };

    // Act
    person.HasMany(p => p.Addresses)
      .HasKey(p => p.Id)
      .WithOne(a => a.Person)
      .WithForeignKey(a => a.PersonId)
      .Apply();

    // Assert
    person.Should().NotBeNull();
    person.Addresses.Should().NotBeNull().And.HaveCount(0);
  }

  [Fact]
  public void ShouldThrowNullReferenceExceptionWhenCalledWithDependencyOfNullObject()
  {
    // Arrange
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    AddressModel address = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8601 // Possible null reference assignment.
    var person = new PersonModel() { Addresses = [address] };
#pragma warning restore CS8601 // Possible null reference assignment.

    // Act
    var result = () => person.HasMany(p => p.Addresses)
      .HasKey(p => p.Id)
      .WithOne(a => a.Person)
      .WithForeignKey(a => a.PersonId)
      .Apply();

    // Assert
    var exception = Assert.Throws<InvalidOperationException>(result);
    exception.Should().NotBeNull();
    exception.Message.Should().Be($"The Source can not be been defined due to dependency being null.");
  }

  [Fact]
  public void ShouldUpdateDependencyWhenCalledWithDependencyWithObject()
  {
    // Arrange
    var addresses = new AddressFaker().Generate(1);
    var person = new PersonFaker().Generate(1).First();
    person.Addresses = addresses;

    // Act
    person.HasMany(p => p.Addresses)
      .HasKey(p => p.Id)
      .WithOne(a => a.Person)
      .WithForeignKey(a => a.PersonId)
      .Apply();

    // Assert
    person.Should().NotBeNull();
    person.Addresses.Should().NotBeNull().And.HaveCount(1);

    var personAddress = person.Addresses.First();
    personAddress.PersonId.Should().Be(person.Id);
  }
}
