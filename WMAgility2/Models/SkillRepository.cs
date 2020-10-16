using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMAgility2.Data;

namespace WMAgility2.Models
{
    public class SkillRepository : ISkillRepository
    {
        private readonly ApplicationDbContext db;

        public SkillRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IEnumerable<Skill> AllSkills => db.Skills;

        // For Unit Testing
        public ICollection<Skill> GetSkills()
        {
            return db.Skills.ToList();
        }


        public Skill GetSkillById(int skillId)
        {
            return db.Skills.FirstOrDefault(i => i.Id == skillId);
        }

        public Skill CreateSkill(Skill skill)
        {
            db.Skills.Add(skill);
            db.SaveChanges();
            return skill;
        }

        public Skill UpdateSkill(Skill skill)
        {
            var foundSkill = db.Skills.FirstOrDefault(i => i.Id == skill.Id);
            foundSkill.Name = skill.Name;
            db.Skills.Update(foundSkill);
            db.SaveChanges();
            return foundSkill;
        }

        public Skill DeleteSkill(Skill skill)
        {
            var foundSkill = db.Skills.FirstOrDefault(i => i.Id == skill.Id);
            db.Skills.Remove(foundSkill);
            db.SaveChanges();
            return foundSkill;
        }
    }
}