// <copyright file="PersonRelativeModel.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Example.Domain.Model
{
  using System.Diagnostics.CodeAnalysis;

  [ExcludeFromCodeCoverage]
  public class PersonRelativeModel
  {
    public int Id { get; set; }

    public int PersonId { get; set; }

    public PersonRelationType RelationType { get; set; }

    public virtual PersonModel? Relative { get; set; }
  }
}
