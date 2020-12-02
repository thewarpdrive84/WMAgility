using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models
{
    public interface IDogRepository 
    {
        IEnumerable<Dog> Dogs { get; }

        // For Unit Testing
        ICollection<Dog> GetDogs();

        Dog GetDogById(int dogId);

        Dog CreateDog(Dog dog);

        Dog UpdateDog(Dog dog);

        Dog DeleteDog(Dog dog);
    }
}