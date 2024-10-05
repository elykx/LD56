using System;
using UnityEngine;

namespace LD56.Assets.Scripts {
    public class CivilizationStats : MonoBehaviour {
        // Основные параметры
        public float targetPopulation = 1000f;
        public float growthDuration = 1f;

        // Коэффициенты влияния
        public float foodFactor => data.PeopleFood / 100f;
        public float happinessFactor => data.PeopleHappiness / 100f;
        public float technologyFactor => 1f + (data.PeopleTechnology / 100f);
        public float diseaseFactor => 1f - (0.5f * (100 - data.PeopleHealth) / 100f);

        // Параметры управления
        public float happinessDecayRate = 0.1f;
        public float foodDecayRate = 0.05f;
        public float foodIncreaseAmount = 10f;
        public float technologyGrowthRate = 0.01f;
        public float healthChange = 0f;

        private float growthRate;
        private float elapsedTime;
        private Data data;

        public void SetData(Data initData) {
            data = initData;
        }

        public void SetPeopleNum(int num) {
            data.SetPeopleNumber(num);
        }

        void Update() {
            if (data == null || data.PeopleNumber == 0) return;

            growthRate = Mathf.Log(targetPopulation / data.PeopleNumber) / growthDuration;

            UpdatePopulation();
            UpdateHappiness();
            UpdateFood();
            UpdateTechnology();
            UpdateHealth();
        }

        private void UpdateHealth() {
            data.PeopleHealth -= healthChange;
            if (data.PeopleHealth < 0) {
                data.PeopleHealth = 0;
            }
        }

        private void UpdateTechnology() {
            data.PeopleTechnology += technologyGrowthRate * Time.deltaTime;
        }

        private void UpdateFood() {
            data.PeopleFood -= foodDecayRate * Time.deltaTime;

            if (data.PeopleFood < 0) {
                data.PeopleFood = 0;
                healthChange -= 5f;
            }
        }

        private void UpdateHappiness() {
            data.PeopleHappiness -= happinessDecayRate * Time.deltaTime;

            if (data.PeopleHappiness < 0) {
                data.PeopleHappiness = 0;
                healthChange -= 5f;
            }
        }

        public void IncreaseHappiness(int amount) {
            data.PeopleHappiness += amount;
            if (data.PeopleHappiness > 100)
                data.PeopleHappiness = 100;
        }

        public void IncreaseFood(int amount) {
            data.PeopleFood += amount;
            if (data.PeopleFood > 100)
                data.PeopleFood = 100;
        }

        public void IncreaseHealth(int amount) {
            data.PeopleHealth += amount;
            if (data.PeopleHealth > 100)
                data.PeopleHealth = 100;
        }
        public void IncreasePopulation(int amount) {
            data.PeopleNumber += amount;
            if (data.PeopleNumber > 1000)
                data.PeopleNumber = 1000;
        }
        public void IncreaseTechnology(int amount) {
            data.PeopleTechnology += amount;
            if (data.PeopleTechnology > 100)
                data.PeopleTechnology = 100;
        }

        private void UpdatePopulation() {
            if (data.PeopleNumber < targetPopulation && elapsedTime < growthDuration) {
                elapsedTime += Time.deltaTime / 60;

                float totalFactor = foodFactor * happinessFactor * technologyFactor * diseaseFactor;

                data.PeopleNumber = (int)(2 * Mathf.Exp(growthRate * totalFactor * elapsedTime));

                if (data.PeopleNumber > targetPopulation)
                    data.PeopleNumber = (int)targetPopulation;

                Debug.Log("Elapsed Time: " + elapsedTime + " Population: " + data.PeopleNumber);
            }
        }
    }
}