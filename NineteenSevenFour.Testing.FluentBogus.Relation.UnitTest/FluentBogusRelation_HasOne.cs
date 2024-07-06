using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Relation.Extension;

using Xunit;

namespace NineteenSevenFour.Testing.FluentBogus.Relation.UnitTest
{
  public class FluentBogusRelationHasOne
  {
    [Fact]
    public void ShouldThrow_ArgumentNullException_WhenCalledFrom_NullSource()
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
      exception.Message.Should().Be($"Value cannot be null. (Parameter 'source')");
    }

    [Fact]
    public void ShouldThrow_ArgumentNullException_WhenCalledWith_NullExpression()
    {
      // Arrange
      var address = new AddressModel();

      // Act
#pragma warning disable IDE0039 // Use local function
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
      var result = () => address.HasOne<AddressModel, PersonModel> (null);
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
      var address = new AddressModel(); // Addresses collection is not set in PersonModel

      // Act
      var hasOneRelation = address.HasOne(a => a.Person);

      // Assert
      hasOneRelation.Should().NotBeNull().And.BeOfType<FluentBogusRelationOneToAny<AddressModel, PersonModel>>();
      hasOneRelation.Dependency.Should().BeNull();

      var relation = hasOneRelation as FluentBogusRelation<AddressModel>;
      Assert.NotNull(relation);
      relation.Source.Should().Be(address);
    }

    [Fact]
    public void ShouldSet_Dependency_WhenCalledWith_InitializedDependency()
    {
      // Arrange
      var address = new AddressModel() { Person = new PersonModel() };

      // Act
      var hasOneRelation = address.HasOne(p => p.Person);

      // Assert
      hasOneRelation.Should().NotBeNull().And.BeOfType<FluentBogusRelationOneToAny<AddressModel, PersonModel>>();
      hasOneRelation.Dependency.Should().BeOfType<PersonModel> ();

      var relation = hasOneRelation as FluentBogusRelation<AddressModel>;
      Assert.NotNull(relation);
      relation.Source.Should().Be(address);
    }
  }
}
