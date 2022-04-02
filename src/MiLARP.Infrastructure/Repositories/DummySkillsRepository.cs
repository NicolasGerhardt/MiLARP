using MiLARP.Application.DataAccess;
using MiLARP.Domain.Models;

namespace MiLARP.Infrastructure.Repositories;

public class DummySkillsRepository : ISkillsRepository
{
    private List<Skill?> _skills;
    public DummySkillsRepository()
    {
        _skills = new List<Skill?>
        {
            new Skill
            {
                Id = 0,
                Name = "First Test Skill",
                Description = "This is the Description",
                Notes = "Here are some notes on the skill"
            },
            new Skill
            {
                Id = 1,
                Name = "Second Test Skill",
                Description = "This is the Description",
                Notes = "Here are some notes on the skill"
            },
            new Skill
            {
                Id = 2,
                Name = "First Test Spell",
                Description = "This is the Description",
                Notes = "Here are some notes on the skill",
                Tags = new List<Tag> { new Tag { Text = "Spell" } }
            },
        };
    }

    public IList<Skill?> GetAllSkills()
    {
        return _skills;
    }

    public IList<Skill?> AddSkill(Skill? skill)
    {
        skill.Id = _skills.Count;
        _skills.Add(skill);
        return _skills;
    }

    public IList<Skill?> UpdateSkill(Skill? skill)
    {
        var skillIndex = _skills.FindIndex(x => x.Id == skill.Id);
        if (skillIndex < 0)
        {
            _skills.Add(skill);
        }
        _skills[skillIndex] = skill;
        return _skills;
    }

    public IList<Skill?> RemoveSkill(Skill skill)
    {
        var skillIndex = _skills.FindIndex(x => x.Id == skill.Id);
        _skills.Remove(_skills[skillIndex]);
        return _skills;
    }
}