using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LD56.Assets.Scripts
{
    public class Data
    {
        public int GodPower;
        public int DevilPower;
        public int PeopleNumber;
        public float PeopleFood;
        public float PeopleHappiness;
        public float PeopleHealth;
        public float PeopleTechnology;


        public Data() {
            GodPower = 0;
            DevilPower = 0;
            PeopleNumber = 2;
            PeopleFood = 100;
            PeopleHappiness = 100;
            PeopleHealth = 100;
            PeopleTechnology = 0;
        }
    }
}