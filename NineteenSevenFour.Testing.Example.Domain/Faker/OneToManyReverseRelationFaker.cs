// <copyright file="OneToManyReverseRelationFaker.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Example.Domain.Faker;

using System.Diagnostics.CodeAnalysis;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Relation;

/// <summary>
/// Defines a <see cref="PersonRelativeFaker"/> with a <see cref="FluentBogusRelation"/> configured.
/// </summary>
[ExcludeFromCodeCoverage]
public class OneToManyReverseRelationFaker : PersonRelativeFaker
{
  /// <summary>
  /// Initializes a new instance of the <see cref="OneToManyReverseRelationFaker"/> class.
  /// </summary>
  /// <param name="id">The ID of the <see cref="PersonRelativeModel"/>.</param>
  public OneToManyReverseRelationFaker(int? id)
    : base(id)
  {
    this.FinishWith((faker, model) =>
    {
      model.HasOne(r => r.Relative)
        .HasForeignKey(r => r.RelativeId)
        .WithMany(p => p.Relatives)
        .WithKey(p => p.Id)
        .Apply();
    });
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="OneToManyReverseRelationFaker"/> class.
  /// </summary>
  public OneToManyReverseRelationFaker()
    : this(null)
  {
  }
}
