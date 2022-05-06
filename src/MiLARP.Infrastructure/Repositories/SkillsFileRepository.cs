using System.Text.Json;
using System.Text.RegularExpressions;
using MiLARP.Application.DataAccess;
using MiLARP.Domain.Models;

namespace MiLARP.Infrastructure.Repositories;

public class SkillsFileRepository : ISkillsRepository
{
    private readonly string _path;
    private int nextId = 0;

    public SkillsFileRepository(string path)
    {
        _path = $"{path}\\skills";
        
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
        
        SetNextId();
    }

    private void SetNextId()
    {
        var filenames = Directory.GetFiles(_path);
        var highestId = 0;
        foreach (var filename in filenames)
        {
            var match = Regex.Match(filename, @"\d+");
            if (match.Success)
            {
                var numText = match.Value;
                var num = int.Parse(numText);
                if (num > highestId)
                {
                    highestId = num;
                }
            }
        }

        nextId = 1 + highestId;
    }

    public IList<Skill?> GetAllSkills()
    {
        var skillList = new List<Skill?>();
        var filenames = Directory.GetFiles(_path);
        foreach (var filename in filenames)
        {
            skillList.Add(ReadFileIntoSkill(filename));
        }

        return skillList;
    }

    public Skill? GetSkillById(int id)
    {
        var filepath = GetSkillFilePath(id);
        if (!File.Exists(filepath))
        {
            return null;
        }

        return ReadFileIntoSkill(filepath);
    }

    private Skill? ReadFileIntoSkill(string filepath)
    {
        var rawData = File.ReadAllText(filepath);
        return JsonSerializer.Deserialize<Skill>(rawData);
    }

    public IList<Skill?> AddSkill(Skill skill)
    {
        skill.Id = nextId;
        nextId++;

        return UpdateSkill(skill);
    }

    public IList<Skill?> UpdateSkill(Skill skill)
    {
        var filepath = GetSkillFilePath(skill.Id);
        
        return UpsertSkillAtFilePath(skill, filepath);
    }

    private IList<Skill?> UpsertSkillAtFilePath(Skill skill, string filepath)
    {
        var data = JsonSerializer.Serialize(skill);
        
        if (!File.Exists(filepath))
        {
            File.Create(filepath).Close();
        }
                
        File.WriteAllText(filepath, data);
        
        return GetAllSkills();
    }

    public IList<Skill?> RemoveSkill(Skill skill)
    {
        var filepath = GetSkillFilePath(skill.Id);

        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }
        
        return GetAllSkills();
    }

    private string GetSkillFilePath(int id)
    {
        return $"{_path}\\{id}.json";
    }
}