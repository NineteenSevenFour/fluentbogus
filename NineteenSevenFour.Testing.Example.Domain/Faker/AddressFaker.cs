// <copyright file="AddressFaker.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Example.Domain.Faker
{
  using System.Diagnostics.CodeAnalysis;
  using AutoBogus;
  using NineteenSevenFour.Testing.Example.Domain.Model;

  [ExcludeFromCodeCoverage]
  public class AddressFaker : AutoFaker<AddressModel>
  {
    public AddressFaker(int id)
      : this()
    {
      this.RuleFor(o => o.Id, () => id);
    }

    public AddressFaker()
      : base()
    {
      this.StrictMode(true);

      this.RuleFor(o => o.Id, f => f.Random.Int(1));
      this.RuleFor(o => o.Number, f => f.Random.Int(1));
      this.RuleFor(o => o.Type, f => f.Random.Enum<AddressType>());
      this.RuleFor(o => o.Street, f => f.Address.StreetAddress());
      this.RuleFor(o => o.City, f => f.Address.City());
      this.RuleFor(o => o.State, f => f.Address.StateAbbr());
      this.RuleFor(o => o.PostalCode, f => f.Address.ZipCode());
      this.RuleFor(o => o.Country, f => f.Address.Country());

      this.RuleFor(o => o.PersonId, f => null);
      this.RuleFor(o => o.Person, f => null);
    }
  }
}
