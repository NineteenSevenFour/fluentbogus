// <copyright file="FluentBogusRelationManyToOne_WithForeignKey.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation.UnitTest
{
  using System;
  using System.Collections.ObjectModel;
  using FluentAssertions;
  using NineteenSevenFour.Testing.Example.Domain.Model;
  using Xunit;

  public class FluentBogusRelationManyToOneWithForeignKey
  {
    [Fact]
    public void ShouldThrowArgumentNullException_DependencyForeignKeyExpression_WhenCalledWith_NullExpression()
    {
      // Arrange
      var person = new PersonModel() { Addresses = new Collection<AddressModel>() };

      // Act
#pragma warning disable IDE0039 // Use local function
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
      var result = () =>
        person.HasMany(p => p.Addresses)
          .HasKey(p => p.Id)
          .WithOne(a => a.Person)
          .WithForeignKey(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore IDE0039 // Use local function

      // Assert
      var exception = Assert.Throws<ArgumentNullException>(result);
      exception.Should().NotBeNull();
      exception.Message.Should().Be($"Value cannot be null. (Parameter 'expression')");
    }

    [Fact]
    public void ShouldSet_DependencyForeignKeyExpression_WhenCalledWith_ForeignKeyExpression()
    {
      // Arrange
      var person = new PersonModel() { Addresses = new Collection<AddressModel>() };

      // Act
      var hasManyWithOneRelation =
        person.HasMany(p => p.Addresses)
          .HasKey(p => p.Id)
          .WithOne(a => a.Person)
          .WithForeignKey(a => a.PersonId);

      // Assert
      hasManyWithOneRelation.Should().NotBeNull().And.BeOfType<FluentBogusRelationManyToOne<PersonModel, AddressModel, int?>?>();
      hasManyWithOneRelation.DependencyForeignKeyExpression.Should().NotBeNull();
      var dependencyForeignKeyExpression = hasManyWithOneRelation.DependencyForeignKeyExpression?.Compile();
      dependencyForeignKeyExpression.Should().NotBeNull().And.BeOfType<Func<AddressModel, int?>>();
    }
  }
}
