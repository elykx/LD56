using System;
using UnityEngine;

namespace LD56.Assets.Scripts {
    public class CivilizationStats : MonoBehaviour {
        // Основные параметры
        private float targetPopulation = 1000f;

        // Коэффициенты влияния
        private float foodFactor = 0.1f;
        private float happinessFactor = 0.1f;

        // Параметры управления
        private float happinessDecayRate = 0.35f;
        private float foodDecayRate = 0.35f;
        private float populationToAddBuff = 0f;

        public void SetPeopleNum(int num) {
            G.data.SetPeopleNumber(num);
        }

        void Update() {
            if (G.data == null) return;

            UpdatePopulation();
            UpdateHappiness();
            UpdateFood();
        }

        private void UpdateFood() {
            G.data.PeopleFood -= foodDecayRate * Time.deltaTime;
            if (G.data.PeopleFood < 0) {
                G.data.PeopleFood = 0;
                IncreasePopulation(-5f);
            }
        }

        private void UpdateHappiness() {
            G.data.PeopleHappiness -= happinessDecayRate * Time.deltaTime;

            if (G.data.PeopleHappiness < 0) {
                G.data.PeopleHappiness = 0;
                IncreasePopulation(-5f);
            }
        }

        public void IncreaseHappiness(float amount) {
            G.data.PeopleHappiness += amount;
            if (G.data.PeopleHappiness > 100)
                G.data.PeopleHappiness = 100;
        }

        public void IncreaseFood(float amount) {
            G.data.PeopleFood += amount;
            if (G.data.PeopleFood > 100)
                G.data.PeopleFood = 100;
        }

        public void IncreasePopulation(float amount) {
            populationToAddBuff += amount;
        }

        private void UpdatePopulation() {
            if (populationToAddBuff != 0) {
                G.data.PeopleNumber += populationToAddBuff;
                if (G.data.PeopleNumber < 0) {
                    G.data.PeopleNumber = 0;
                }
                populationToAddBuff = 0f;
            }
            if (G.data.PeopleNumber < targetPopulation && G.data.PeopleNumber > 0) {
                float growthRate;

                float foodInfluence = G.data.PeopleFood * foodFactor;
                float happinessInfluence = G.data.PeopleHappiness * happinessFactor;

                if (G.data.PeopleNumber < 50) {
                    growthRate = 1f * foodInfluence * happinessInfluence;
                    G.data.PeopleNumber += growthRate * Time.deltaTime / 20f;
                }
                else if (G.data.PeopleNumber < 100) {
                    growthRate = 1.5f * foodInfluence * happinessInfluence;
                    G.data.PeopleNumber += growthRate * Time.deltaTime / 15f;
                }
                else if (G.data.PeopleNumber < 250) {
                    growthRate = 2 * foodInfluence * happinessInfluence;
                    G.data.PeopleNumber += growthRate * Time.deltaTime / 10f;
                }
                else {
                    growthRate = 3 * foodInfluence * happinessInfluence;
                    G.data.PeopleNumber += growthRate * Time.deltaTime / 5f;
                }

                if (G.data.PeopleNumber < 0) {
                    Debug.Log("lower");

                    G.data.PeopleNumber = 0;
                }
                if (G.data.PeopleNumber > 1000)
                    G.data.PeopleNumber = 1000;
            }
        }
        private float Sigmoid(float x) {
            return 1f / (1f + Mathf.Exp(-12f * (x - 0.5f)));
        }
    }
}