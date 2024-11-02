// <copyright file="FluentBogusBuilder_Fake.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest
{
  using FluentAssertions;
  using NineteenSevenFour.Testing.Example.Domain.Model;

  public class FluentBogusBuilderFake
  {
    [Fact]
    public void Should_Return_ABuilder_WhenCalled()
    {
      // Arrange

      // Act
      var builder = FluentBogusBuilderExtension.Fake<PersonModel>();

      // Assert
      builder.Should()
        .NotBeNull().And
        .BeOfType<FluentBogusBuilder<PersonModel>>();
    }
  }
}
