using MiLARP.Domain.Models;

namespace MiLARP.Application.DataAccess;

public interface ISkillsRepository
{
    public IList<Skill?> GetAllSkills();
    public Skill? GetSkillById(int id);
    public IList<Skill?> AddSkill(Skill skill);
    public IList<Skill?> UpdateSkill(Skill skill);
    public IList<Skill?> RemoveSkill(Skill skill);
}