using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Faker;
using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Relation.Extension;

using System.Collections.ObjectModel;

using Xunit;

namespace NineteenSevenFour.Testing.FluentBogus.Relation.UnitTest
{
  public class FluentBogusRelationManyToOne_Apply
  {
    [Fact]
    public void ShouldSkip_WhenCalledWith_NullDependency()
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
    public void ShouldSkip_WhenCalledWith_NoDependency()
    {
      // Arrange
      var person = new PersonModel() { Addresses = new Collection<AddressModel>() };

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
    public void ShouldSkip_WhenCalledWith_DependencyOfNullObject()
    {
      // Arrange
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
      AddressModel address = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
      var person = new PersonModel() { Addresses = new Collection<AddressModel>() { address } };
#pragma warning restore CS8604 // Possible null reference argument.

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
      personAddress.Should().BeNull();
    }

    [Fact]
    public void ShouldUpdateDependency_WhenCalledWith_DependencyWithObject()
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
}
