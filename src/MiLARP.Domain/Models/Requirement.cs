namespace MiLARP.Domain.Models;

public class Requirement
{
    public int Id { get; set; }
    public int? Level { get; set; }
    public CharacterClass Class { get; set; }
    public int? SkillId { get; set; }
}