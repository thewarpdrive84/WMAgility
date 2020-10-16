using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMAgility2.Data;

namespace WMAgility2.Models
{
    public class DogRepository : IDogRepository
    {
        private readonly ApplicationDbContext db;

        public DogRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IEnumerable<Dog> Dogs => db.Dogs;

        // For Unit Testing
        public ICollection<Dog> GetDogs()
        {
            return db.Dogs.ToList();
        }


        public Dog GetDogById(int dogId)
        {
            return db.Dogs.FirstOrDefault(i => i.Id == dogId);
        }

        public Dog CreateDog(Dog dog)
        {
            db.Dogs.Add(dog);
            db.SaveChanges();
            return dog;
        }

        public Dog UpdateDog(Dog dog)
        {
            var foundDog = db.Dogs.FirstOrDefault(i => i.Id == dog.Id);
            foundDog.DogName = dog.DogName;
            db.Dogs.Update(foundDog);
            db.SaveChanges();
            return foundDog;
        }

        public Dog DeleteDog(Dog dog)
        {
            var foundDog = db.Dogs.FirstOrDefault(i => i.Id == dog.Id);
            db.Dogs.Remove(foundDog);
            db.SaveChanges();
            return foundDog;
        }
    }
}