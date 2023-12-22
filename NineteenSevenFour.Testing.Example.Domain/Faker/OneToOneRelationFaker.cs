// <copyright file="OneToOneRelationFaker.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Example.Domain.Faker;

using System.Diagnostics.CodeAnalysis;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Relation;

/// <summary>
/// Defines a <see cref="PersonFaker"/> with a <see cref="FluentBogusRelation"/> configured.
/// </summary>
[ExcludeFromCodeCoverage]
public class OneToOneRelationFaker : PersonFaker
{
  /// <summary>
  /// Initializes a new instance of the <see cref="OneToOneRelationFaker"/> class.
  /// </summary>
  /// <param name="id">The ID of the <see cref="PersonModel"/>.</param>
  public OneToOneRelationFaker(int? id)
    : base(id)
  {
    this.FinishWith((faker, model) =>
    {
      model.HasOne(p => p.Address)
        .HasKey(p => p.Id)
        .WithOne(a => a.Owner)
        .WithForeignKey(a => a.OwnerId)
        .Apply();
    });
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="OneToOneRelationFaker"/> class.
  /// </summary>
  public OneToOneRelationFaker()
    : this(null)
  {
  }
}
