namespace MiLARP.Domain.Models;

public class CharacterClass
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IList<Skill> StartingSkills { get; set; }
}