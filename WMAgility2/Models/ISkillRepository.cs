using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models
{
    public interface ISkillRepository
    {
        IEnumerable<Skill> AllSkills { get; }
        Skill GetSkillById(int Id);
        //void UpSkill(Skill skill);

        //Unit Testing
        Skill CreateSkill(Skill skill);
        Skill UpdateSkill(Skill skill);
        Skill DeleteSkill(Skill skill);
    }
}
