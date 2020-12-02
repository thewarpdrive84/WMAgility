using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models
{
    public interface IPracticeRepository
    {
        IEnumerable<Practice> AllPractices { get; }
        Practice GetPracticeById(int Id);
        Task<Practice> GetPracticeBySkillIdAsync(int? Id);

        //Unit Testing
        Practice CreatePractice(Practice practice);

        Practice UpdatePractice(Practice practice);

        Practice DeletePractice(Practice practice);
    }
}
