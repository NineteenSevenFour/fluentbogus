using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Relation.Extension;

using System.Collections.ObjectModel;
using System.Linq.Expressions;

using Xunit;

namespace NineteenSevenFour.Testing.FluentBogus.Relation.UnitTest;

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

public class FluentBogusRelationManyToAny_HasKey
{
  [Fact]
  public void ShouldThrow_ArgumentNullException_WhenCalledWith_NullExpression()
  {
    // Arrange
    var person = new PersonModel();

    // Act
#pragma warning disable IDE0039 // Use local function
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    var result = () =>
        person.HasMany(p => p.Addresses)
              .HasKey<int>(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore IDE0039 // Use local function

    // Assert
    var exception = Assert.Throws<ArgumentNullException>(result);
    exception.Should().NotBeNull();
    exception.Message.Should().Be($"Value cannot be null. (Parameter 'sourceKeyExpression')");
  }

  [Fact]
  public void ShouldSet_SourceKeyExpression_WhenCalledWith_ValidExpression()
  {
    // Arrange
    var person = new PersonModel() { Addresses = new Collection<AddressModel>() };

    // Act
    var hasManyWithKeyRelation = person.HasMany(p => p.Addresses).HasKey(p => p.Id);

    // Assert
    hasManyWithKeyRelation.Should().NotBeNull().And.BeOfType<FluentBogusRelationManyToAny<PersonModel, AddressModel, int>?>();
    hasManyWithKeyRelation.SourceKeyExpression.Should().NotBeNull();
  }
}

public class FluentBogusRelationManyToAny_WithOne
{
  [Fact]
  public void ShouldThrow_ArgumentNullException_WhenCalledWith_NullExpression()
  {
    // Arrange
    var person = new PersonModel();

    // Act
#pragma warning disable IDE0039 // Use local function
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    var result = () =>
        person.HasMany(p => p.Addresses)
              .HasKey<int>(p => p.Id)
              .WithOne(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore IDE0039 // Use local function

    // Assert
    var exception = Assert.Throws<ArgumentNullException>(result);
    exception.Should().NotBeNull();
    exception.Message.Should().Be($"Value cannot be null. (Parameter 'withOneExpression')");
  }

  [Fact]
  public void ShouldSet_SourceRefExpression_WhenCalledWith_SourceExpression()
  {
    // Arrange
    var person = new PersonModel() { Addresses = new Collection<AddressModel>() };

    // Act
    var hasManyWithOneRelation =
        person.HasMany(p => p.Addresses)
              .HasKey<int>(p => p.Id)
              .WithOne(a => a.Person);

    // Assert
    hasManyWithOneRelation.Should().NotBeNull().And.BeOfType<FluentBogusRelationManyToOne<PersonModel, AddressModel, int>?>();
    hasManyWithOneRelation.SourceRefExpression.Should().NotBeNull();
  }
}

public class FluentBogusRelationManyToOne_WithForeignKey
{

}

public class FluentBogusRelationManyToOne_Apply
{

}
