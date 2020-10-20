using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMAgility2.Data;

namespace WMAgility2.Models
{
    public class CompRepository : ICompRepository
    {
        private readonly ApplicationDbContext db;

        public CompRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IEnumerable<Competition> AllComps => db.Competitions;

        // For Unit Testing
        public ICollection<Competition> GetComps()
        {
            return db.Competitions.ToList();
        }


        public Competition GetCompById(int compId)
        {
            return db.Competitions.FirstOrDefault(i => i.CompId == compId);
        }

        public Competition CreateComp(Competition comp)
        {
            db.Competitions.Add(comp);
            db.SaveChanges();
            return comp;
        }

        public Competition UpdateComp(Competition comp)
        {
            var foundComp = db.Competitions.FirstOrDefault(i => i.CompId == comp.CompId);
            foundComp.CompName = comp.CompName;
            db.Competitions.Update(foundComp);
            db.SaveChanges();
            return foundComp;
        }

        public Competition DeleteComp(Competition comp)
        {
            var foundComp = db.Competitions.FirstOrDefault(i => i.CompId == comp.CompId);
            db.Competitions.Remove(foundComp);
            db.SaveChanges();
            return foundComp;
        }
    }
}