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
        public float PeopleNumber;
        public float PeopleFood;
        public float PeopleHappiness;


        public Data() {
            GodPower = 0;
            DevilPower = 0;
            PeopleNumber = 2;
            PeopleFood = 25f;
            PeopleHappiness = 25f;
        }

        public void SetPeopleNumber(int peopleNumber) {
            PeopleNumber = peopleNumber;
        }
    }
}