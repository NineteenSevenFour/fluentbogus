// <copyright file="PersonRelativeModel.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
namespace NineteenSevenFour.Testing.Example.Domain.Model;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Describe the relation between two person.
/// </summary>
[ExcludeFromCodeCoverage]
public class PersonRelativeModel
{
  /// <summary>
  /// Gets or sets the ID of the relation.
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// Gets or sets the relation type.
  /// </summary>
  public PersonRelationType Relation { get; set; }

  /// <summary>
  /// Gets or sets the related person ID.
  /// </summary>
  public int? RelativeId { get; set; }

  /// <summary>
  /// Gets or sets the related person.
  /// </summary>
  public virtual PersonModel? Relative { get; set; }
}
