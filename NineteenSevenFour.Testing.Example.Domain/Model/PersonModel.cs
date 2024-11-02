// <copyright file="PersonModel.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Example.Domain.Model
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  [ExcludeFromCodeCoverage]
  public class PersonModel
  {
    public int? Id { get; set; }

    public PersonType Type { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public int Age { get; set; }

    public DateTime Birthday { get; set; }

    public ICollection<AddressModel>? Addresses { get; set; }

    public ICollection<PersonRelativeModel>? Relatives { get; set; }
  }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
