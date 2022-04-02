using MiLARP.Domain.Models;

namespace MiLARP.UI.Extensions;

public static class SkillExtensions
{
    public static string ToString(this Skill skill) => skill.Name;
}
