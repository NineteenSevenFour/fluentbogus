using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Relation.Extension;

using System.Collections.ObjectModel;

using Xunit;

namespace NineteenSevenFour.Testing.FluentBogus.Relation.UnitTest
{
  public class FluentBogusRelation_HasMany
  {
    [Fact]
    public void ShouldThrow_ArgumentNullException_WhenCalledFrom_NullSource()
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
      exception.Message.Should().Be($"Value cannot be null. (Parameter 'source')");
    }

    [Fact]
    public void ShouldThrow_ArgumentNullException_WhenCalledWith_NullExpression()
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
      exception.Message.Should().Be($"Value cannot be null. (Parameter 'expression')");
    }

    [Fact]
    public void ShouldNotSet_Dependency_WhenCalledWith_NonInitializedDependency()
    {
      // Arrange
      var person = new PersonModel(); // Addresses collection is not set in PersonModel

      // Act
      var hasManyRelation = person.HasMany(p => p.Addresses);

      // Assert
      hasManyRelation.Should().NotBeNull().And.BeOfType<FluentBogusRelationManyToAny<PersonModel, AddressModel>>();
      hasManyRelation.Dependency.Should().BeNullOrEmpty();

      var relation = hasManyRelation as FluentBogusRelation<PersonModel>;
      Assert.NotNull(relation);
      relation.Source.Should().Be(person);
    }

    [Fact]
    public void ShouldSet_Dependency_WhenCalledWith_InitializedDependency()
    {
      // Arrange
      var person = new PersonModel() { Addresses = new Collection<AddressModel>() };

      // Act
      var hasManyRelation = person.HasMany(p => p.Addresses);

      // Assert
      hasManyRelation.Should().NotBeNull().And.BeOfType<FluentBogusRelationManyToAny<PersonModel, AddressModel>>();
      hasManyRelation.Dependency.Should().BeOfType<Collection<AddressModel>>();

      var relation = hasManyRelation as FluentBogusRelation<PersonModel>;
      Assert.NotNull(relation);
      relation.Source.Should().Be(person);
    }
  }
}
