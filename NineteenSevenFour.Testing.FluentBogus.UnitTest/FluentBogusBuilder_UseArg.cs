// <copyright file="FluentBogusBuilder_UseArg.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest
{
  using FluentAssertions;
  using NineteenSevenFour.Testing.Example.Domain.Faker;
  using NineteenSevenFour.Testing.Example.Domain.Model;

  public class FluentBogusBuilderUseArg
  {
    [Fact]
    public void Should_StoreFakerArg_WhenCalled()
    {
      // Arrange
      var args = new[] { "argOne", "argTwo" };
      var builder = FluentBogusBuilderExtension.Fake<PersonModel>().UseFaker<PersonFaker>();

      // Act
      builder.UseArgs(args);

      // Assert
      var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
      Assert.NotNull(typedBuilder);
      typedBuilder.FakerArgs.Should()
        .NotBeEmpty().And
        .HaveCount(args.Length);
    }
  }
}
