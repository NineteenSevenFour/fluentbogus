namespace NineteenSevenFour.Testing.Example.Domain.Model;

public class PersonRelativeModel
{
  public int Id { get; set; }
  public int PersonId { get; set; }
  public PersonRelationType RelationType { get; set; }
  public virtual PersonModel? Relative { get; set; }
}
