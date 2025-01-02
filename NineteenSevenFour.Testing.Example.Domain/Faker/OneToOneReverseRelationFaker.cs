// <copyright file="OneToOneReverseRelationFaker.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Example.Domain.Faker;

using System.Diagnostics.CodeAnalysis;

using NineteenSevenFour.Testing.Example.Domain.Faker.Simple;
using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Relation;

/// <summary>
/// Defines a <see cref="OneToOneReverseRelationFaker"/> with a <see cref="FluentBogusRelation"/> configured.
/// </summary>
[ExcludeFromCodeCoverage]
public class OneToOneReverseRelationFaker : AddressFaker
{
  /// <summary>
  /// Initializes a new instance of the <see cref="OneToOneReverseRelationFaker"/> class.
  /// </summary>
  /// <param name="id">The ID of the <see cref="AddressModel"/>.</param>
  public OneToOneReverseRelationFaker(int? id)
    : base(id)
  {
    this.FinishWith((faker, model) =>
    {
      model.HasOne(a => a.Owner)
        .HasForeignKey(a => a.OwnerId)
        .WithOne(p => p.Address)
        .WithKey(p => p.Id)
        .Apply();
    });
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="OneToOneReverseRelationFaker"/> class.
  /// </summary>
  public OneToOneReverseRelationFaker()
    : this(null)
  {
  }
}
