// <copyright file="OneToManyRelationFaker.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Example.Domain.Faker;

using System.Diagnostics.CodeAnalysis;

using NineteenSevenFour.Testing.Example.Domain.Faker.Simple;
using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Relation;

/// <summary>
/// Defines a <see cref="PersonFaker"/> with a <see cref="FluentBogusRelation"/> configured.
/// </summary>
[ExcludeFromCodeCoverage]
public class OneToManyRelationFaker : PersonFaker
{
  /// <summary>
  /// Initializes a new instance of the <see cref="OneToManyRelationFaker"/> class.
  /// </summary>
  /// <param name="id">The ID of the <see cref="PersonModel"/>.</param>
  public OneToManyRelationFaker(int? id)
    : base(id)
  {
    this.FinishWith((faker, model) =>
    {
      model.HasMany(p => p.Relatives)
        .HasKey(p => p.Id)
        .WithOne(r => r.Relative)
        .WithForeignKey(r => r.RelativeId)
        .Apply();
    });
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="OneToManyRelationFaker"/> class.
  /// </summary>
  public OneToManyRelationFaker()
    : this(null)
  {
  }
}
