// <copyright file="FluentBogusBuilderRuleSetString.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

using FluentAssertions;
using NineteenSevenFour.Testing.Example.Domain.Faker;
using NineteenSevenFour.Testing.Example.Domain.Model;

public class FluentBogusBuilderRuleSetString
{
  [Fact]
  public void ShouldReturnJoinedListWhenCalledWithRuleset()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>();

    // Act
    builder.UseRuleSet("rule1", "rule2");

    // Assert
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.RuleSetString.Should()
      .NotBeNullOrEmpty()
      .And
      .Be("rule1,rule2");
  }

  [Fact]
  public void ShouldReturnEmptyStringWhenCalledWithoutRuleset()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().UseFaker<PersonFaker>();

    // Act

    // Assert
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.RuleSetString.Should()
      .BeNullOrEmpty();
  }
}
