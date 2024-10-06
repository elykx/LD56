using System;
using UnityEngine;

namespace LD56.Assets.Scripts {
    public class CivilizationStats : MonoBehaviour {
        // Основные параметры
        public float targetPopulation = 1000f;
        public float growthDuration = 3000f; // Время для достижения цели (5 минут = 300 секунд)

        // Коэффициенты влияния
        public float foodFactor => Mathf.Clamp(G.data.PeopleFood / 100f, 0.5f, 1f); // Ограничиваем до 0.5-1
        public float happinessFactor => Mathf.Clamp(G.data.PeopleHappiness / 100f, 0.5f, 1f); // Ограничиваем до 0.5-1

        // Параметры управления
        public float happinessDecayRate = 0.1f;
        public float foodDecayRate = 0.05f;
        public float foodIncreaseAmount = 10f;

        private float elapsedTime;
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
                populationToAddBuff = 0f;
            }
            if (G.data.PeopleNumber < targetPopulation && G.data.PeopleNumber > 0) {
                elapsedTime += Time.deltaTime;

                // Рассчитываем текущий процент времени, прошедший от всего времени роста
                float growthProgress = elapsedTime / (growthDuration * 60f); // growthDuration = 5 минут

                // Экспоненциальный рост по формуле P = P0 * e^(r * t), где P0 = 2, targetPopulation = 1000
                if (growthProgress <= 1f) {
                    // Рассчитываем новое население с учетом факторов
                    float newPopulation = Mathf.Lerp(G.data.PeopleNumber, targetPopulation, Mathf.Pow(growthProgress, 2f));

                    // Увеличиваем текущее население до нового значения, ограниченного целевым
                    var populationToAdd = Mathf.Clamp((int)(newPopulation * foodFactor * happinessFactor), G.data.PeopleNumber, (int)targetPopulation);
                    var increaseAmount = populationToAdd - G.data.PeopleNumber; // Рассчитываем, на сколько нужно увеличить

                    Debug.Log("populationToAdd " + populationToAdd + " increaseAmount " + increaseAmount);
                    G.data.PeopleNumber += increaseAmount;

                }

                if (G.data.PeopleNumber >= targetPopulation) {
                    G.data.PeopleNumber = (int)targetPopulation;
                }
            }
        }
    }
}