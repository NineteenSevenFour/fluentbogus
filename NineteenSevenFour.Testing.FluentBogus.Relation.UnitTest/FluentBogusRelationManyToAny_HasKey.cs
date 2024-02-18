using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Relation.Extension;

using System.Collections.ObjectModel;

using Xunit;

namespace NineteenSevenFour.Testing.FluentBogus.Relation.UnitTest
{
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
      hasManyWithKeyRelation.Should().NotBeNull().And.BeOfType<FluentBogusRelationManyToAny<PersonModel, AddressModel, int?>?>();
      hasManyWithKeyRelation.SourceKeyExpression.Should().NotBeNull();
    }
  }
}
