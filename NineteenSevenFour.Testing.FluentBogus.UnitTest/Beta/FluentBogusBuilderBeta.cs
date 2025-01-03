// <copyright file="FluentBogusBuilderBeta.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest.Beta;

using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Beta;

public class FluentBogusBuilderBeta
{
  [Fact]
  public void ShouldCreateABuilderFromCallToGenerator()
  {
    // Arrange
    var builder = FluentBogusGeneratorBeta.CreateBuilder<PersonModel>();

    // Act

    // Assert
    builder.Should()
      .NotBeNull().And
      .BeOfType<FluentBogusBuilderBeta<PersonModel>>();
  }
}
