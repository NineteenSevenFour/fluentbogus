using AutoBogus;

using NineteenSevenFour.Testing.Example.Domain.Model;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NineteenSevenFour.Testing.Example.Domain.Faker
{
  [ExcludeFromCodeCoverage]
  public class PersonFaker : AutoFaker<PersonModel>
  {
    public PersonFaker(int Id) : this()
    {
      RuleFor(o => o.Id, () => Id);
    }

    public PersonFaker() : base()
    {
      StrictMode(true);

      RuleFor(o => o.Id, f => f.Random.Int(1));
      RuleFor(o => o.Name, f => f.Person.FirstName);
      RuleFor(o => o.Surname, f => f.Name.LastName().ToUpper());
      RuleFor(o => o.Age, f => f.Random.Int(0, 99));
      RuleFor(o => o.Birthday, f => f.Person.DateOfBirth);
      RuleFor(o => o.Type, (f, o) =>
      {
        return o.Age switch
        {
          int age when age <= 5 => PersonType.Infant,
          int age when age > 5 && age < 18 => PersonType.Child,
          _ => PersonType.Adult,
        };
      });
      RuleFor(o => o.Addresses, _ => new List<AddressModel>());
      RuleFor(o => o.Relatives, _ => new List<PersonRelativeModel>());

      //FinishWith((f, e) =>
      //{
      //  e.HasMany(p => p.Addresses)
      //    .HasKey(p => p.Id)
      //    .WithOne(a => a.Person)
      //    .WithForeignKey(a => a.PersonId)
      //    .Apply();
      //});
    }    
  }
}
