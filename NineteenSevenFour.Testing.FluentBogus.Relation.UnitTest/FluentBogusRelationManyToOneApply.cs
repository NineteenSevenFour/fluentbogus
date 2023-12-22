// <copyright file="FluentBogusRelationManyToOneApply.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation.UnitTest;

using System;
using System.Collections.Generic;
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
    var person = new PersonModel() { Relatives = null };

    // Act
    person.HasMany(p => p.Relatives)
      .HasKey(p => p.Id)
      .WithOne(a => a.Relative)
      .WithForeignKey(a => a.RelativeId)
      .Apply();

    // Assert
    person.Should().NotBeNull();
    person.Relatives.Should().BeNull();
  }

  [Fact]
  public void ShouldSkipWhenCalledWithNoDependency()
  {
    // Arrange
#if NET8_0_OR_GREATER
    var person = new PersonModel() { Relatives = [] };
#else
    var person = new PersonModel() { Relatives = new List<PersonRelativeModel>() };
#endif

    // Act
    person.HasMany(p => p.Relatives)
      .HasKey(p => p.Id)
      .WithOne(a => a.Relative)
      .WithForeignKey(a => a.RelativeId)
      .Apply();

    // Assert
    person.Should().NotBeNull();
    person.Relatives.Should().NotBeNull().And.HaveCount(0);
  }

  [Fact]
  public void ShouldThrowNullReferenceExceptionWhenCalledWithDependencyOfNullObject()
  {
    // Arrange
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    PersonRelativeModel relative = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8604 // Possible null reference argument.
#if NET8_0_OR_GREATER
    var person = new PersonModel() { Relatives = [relative] };
#else
    var person = new PersonModel() { Relatives = new List<PersonRelativeModel>() { relative } };
#endif
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8601 // Possible null reference assignment.

    // Act
    var result = () => person.HasMany(p => p.Relatives)
      .HasKey(p => p.Id)
      .WithOne(a => a.Relative)
      .WithForeignKey(a => a.RelativeId)
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
    var relatives = new PersonRelativeFaker().Generate(1);
    var person = new PersonFaker().Generate(1).First();
    person.Relatives = relatives;

    // Act
    person.HasMany(p => p.Relatives)
      .HasKey(p => p.Id)
      .WithOne(a => a.Relative)
      .WithForeignKey(a => a.RelativeId)
      .Apply();

    // Assert
    person.Should().NotBeNull();
    person.Relatives.Should().NotBeNull().And.HaveCount(1);

    var personAddress = person.Relatives.First();
    personAddress.RelativeId.Should().Be(person.Id);
  }
}
