using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMAgility2.Data;

namespace WMAgility2.Models
{
    public class PracticeRepository : IPracticeRepository
    {
        private readonly ApplicationDbContext db;

        public PracticeRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IEnumerable<Practice> AllPractices => db.Practices;

        public async Task<Practice> GetPracticeBySkillIdAsync(int? id)
        {
            return await db.Practices.Include(p => p.Skill).Include(p => p.Dog)
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        // For Unit Testing
        public ICollection<Practice> GetPractices()
        {
            return db.Practices.ToList();
        }

        public Practice GetPracticeById(int pracId)
        {
            return db.Practices.FirstOrDefault(i => i.Id == pracId);
        }

        public Practice CreatePractice(Practice practice)
        {
            db.Practices.Add(practice);
            db.SaveChanges();
            return practice;
        }

        public Practice UpdatePractice(Practice practice)
        {
            var foundPractice = db.Practices.FirstOrDefault(i => i.Id == practice.Id);
            foundPractice.Date = practice.Date;
            db.Practices.Update(foundPractice);
            db.SaveChanges();
            return foundPractice;
        }

        public Practice DeletePractice(Practice practice)
        {
            var foundPractice = db.Practices.FirstOrDefault(i => i.Id == practice.Id);
            db.Practices.Remove(foundPractice);
            db.SaveChanges();
            return foundPractice;
        }
    }
}