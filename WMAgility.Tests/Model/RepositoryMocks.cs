using System.Collections.Generic;
using WMAgility2.Models;
using Moq;
using System;

namespace WMAgility.Tests.Model
{
    public class RepositoryMocks
    {
        //PRACTICES
        public static Mock<IPracticeRepository> GetPracticeRepository()
        {
            var practices = new List<Practice>
            {
            new Practice
            {
                Id = 1,
                Date = DateTime.Parse("2020-12-11"),
                Rounds = 1,
                Score = 5,
                SkillId = 2,
                Notes = "Jo missed second contact on dogwalk.",
                DogId = 3,
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            },
            new Practice
            {
                Id = 2,
                Date = DateTime.Parse("2020-12-12"),
                Rounds = 1,
                Score = 9,
                SkillId = 1,
                Notes = "Jo did very well.",
                DogId = 3,
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            },
            new Practice
            {
                Id = 3,
                Date = DateTime.Parse("2020-12-13"),
                Rounds = 1,
                Score = 8,
                SkillId = 3,
                Notes = "Jo knocked one pole",
                DogId = 3,
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            }
            };

            var mockPracticeRepository = new Mock<IPracticeRepository>();
            mockPracticeRepository.Setup(repo => repo.AllPractices).Returns(practices);
            mockPracticeRepository.Setup(repo => repo.GetPracticeById(It.IsAny<int>())).Returns(practices[0]);
            return mockPracticeRepository;
        }

        //SKILLS
        public static Mock<ISkillRepository> GetSkillRepository()
        {
            var skills = new List<Skill>
            { 
            new Skill
            {
                Id = 1,
                Name = "Weaves",
                Description = "Follow Instruction",
                IKC = "The minimum number of poles should be five...",
                Image = "weaves.jpeg"
            },

            new Skill
            {
                    Id = 2,
                    Name = "Dog Walk",
                    Description = "Contact",
                    IKC = "A walk plank of 1.2m minimum...",
                    Image = "dogwalk.jpg"
            },

            new Skill
            {
                Id = 3,
                Name = "Jumps",
                Description = "Follow Instruction",
                IKC = "Each unit a minimum length of 1.2m...",
                Image = "jump.jpg"
            }
            };

            var mockSkillRepository = new Mock<ISkillRepository>();
            mockSkillRepository.Setup(repo => repo.AllSkills).Returns(skills);
            mockSkillRepository.Setup(repo => repo.GetSkillById(It.IsAny<int>())).Returns(skills[0]);
            return mockSkillRepository;
        }

        //DOGS
        public static Mock<IDogRepository> GetDogRepository()
        {
            var dogs = new List<Dog>
            {
                new Dog()
                {
                    Id = 3,
                    DogName = "Jo",
                    Breed = "Terrier x",
                    DOB = DateTime.Parse("2012-07-26"),
                    Image = "jo.jpg",
                    ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
                },
                new Dog()
                {
                    Id = 4,
                    DogName = "Maddie",
                    Breed = "Irish Terrier",
                    DOB = DateTime.Parse("2013-09-17"),
                    Image = "maddie.jpg",
                    ApplicationUserId = "20f95e44-8f29-42d8-b3f9-ff4c3e683ce5"
                },
                new Dog()
                {
                    Id = 6,
                    DogName = "Gypsy",
                    Breed = "Border Collie",
                    DOB = DateTime.Parse("2010-04-01"),
                    Image = "gypsy.jpg",
                    ApplicationUserId = "d9f635a4-05d0-4394-a048-e3e514c74d05"
                }
            };

            var mockDogRepository = new Mock<IDogRepository>();
            mockDogRepository.Setup(repo => repo.Dogs).Returns(dogs);

            return mockDogRepository;
        }

        private static Dictionary<string, Dog> _dogs;
        public static Dictionary<string, Dog> Dogs
        {
            get
            {
                if (_dogs == null)
                {
                    var dogList = new Dog[]
                    {
                        new Dog { DogName = "Kai" },
                        new Dog { DogName = "Finn" },
                        new Dog { DogName = "Nancy" }
                    };

                    _dogs = new Dictionary<string, Dog>();

                    foreach (var dog in dogList)
                    {
                        _dogs.Add(dog.DogName, dog);
                    }
                }
                return _dogs;
            }
        }
    }
}