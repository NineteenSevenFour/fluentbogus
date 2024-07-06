using AutoBogus;

using NineteenSevenFour.Testing.Example.Domain.Model;

using System.Diagnostics.CodeAnalysis;

namespace NineteenSevenFour.Testing.Example.Domain.Faker
{
  [ExcludeFromCodeCoverage]
  public class AddressFaker : AutoFaker<AddressModel>
  {
    public AddressFaker(int id) : this()
    {
      RuleFor(o => o.Id, () => id);
    }

    public AddressFaker() : base()
    {
      StrictMode(true);

      RuleFor(o => o.Id, f => f.Random.Int(1));
      RuleFor(o => o.Number, f => f.Random.Int(1));
      RuleFor(o => o.Type, f => f.Random.Enum<AddressType>());
      RuleFor(o => o.Street, f => f.Address.StreetAddress());
      RuleFor(o => o.City, f => f.Address.City());
      RuleFor(o => o.State, f => f.Address.StateAbbr());
      RuleFor(o => o.PostalCode, f => f.Address.ZipCode());
      RuleFor(o => o.Country, f => f.Address.Country());

      RuleFor(o => o.PersonId, f => null);
      RuleFor(o => o.Person, f => null);

    }
  }
}
