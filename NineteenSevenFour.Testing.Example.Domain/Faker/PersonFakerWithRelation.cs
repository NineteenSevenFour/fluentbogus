// <copyright file="PersonFakerWithRelation.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Example.Domain.Faker
{
  using System;
  using System.Diagnostics.CodeAnalysis;

  using NineteenSevenFour.Testing.Example.Domain.Model;
  using NineteenSevenFour.Testing.FluentBogus.Relation;

  [ExcludeFromCodeCoverage]
  public class PersonFakerWithRelation : PersonFaker
  {
    private readonly Action<Bogus.Faker, PersonModel> finishWith = (f, o) =>
    {
      o.HasMany(parent => parent.Addresses)
        .HasKey(parent => parent.Id)
        .WithOne(child => child.Person)
        .WithForeignKey(child => child.PersonId)
        .Apply();
    };

    public PersonFakerWithRelation(int id)
      : base(id)
    {
      this.FinishWith(this.finishWith);
    }

    public PersonFakerWithRelation()
      : base()
    {
      this.FinishWith(this.finishWith);
    }
  }
}
