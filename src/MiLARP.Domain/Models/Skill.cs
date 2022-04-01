namespace MiLARP.Domain.Models;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Notes { get; set; }
    public IList<Requirement> Requirements { get; set; }
    public IList<Modifier> Modifyers { get; set; }
}