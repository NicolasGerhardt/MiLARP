using System.ComponentModel;

namespace MiLARP.Domain.Models;

public class Character
{
    public int Id { get; set; }
    public string Name { get; }

    public int HitPointTotal
    {
        get
        {
            var hpTotal = Level;
            foreach (var skill in Skills)
            {
                foreach (var modifyer in skill.Modifyers)
                {
                    if (modifyer.Stat == StatType.HitPoint)
                    {
                        hpTotal += modifyer.Amount;
                    }
                }
            }

            return hpTotal;
        }
    }

    public int ActivePointTotal
    {
        get
        {
            var apTotal = Level;
            foreach (var skill in Skills)
            {
                foreach (var modifyer in skill.Modifyers)
                {
                    if (modifyer.Stat == StatType.ActivePoint)
                    {
                        apTotal += modifyer.Amount;
                    }
                }
            }

            return apTotal;
        }
    }
    public int Level { get; }
    public CharacterClass Class { get; }
    public IList<Skill> Skills { get; set; }

    public Character(string name, CharacterClass @class, IList<Skill> skills, int level = 1)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Class = @class ?? throw new ArgumentNullException(nameof(@class));
        Skills = skills ?? new List<Skill>();
        if (level < 1)
        {
            throw new InvalidEnumArgumentException(nameof(level));
        }
        Level = level;
    }
}