using UnityEngine;

namespace LD56.Assets.Scripts {
    public enum CardType {
        Number,
        Food,
        Hapiness,
        DevilUpPopDebugFood,
        DevilUpPopDebugHappi,
        DevilUpPopDebugFoodHappi,
        God,
        Common,
    }

    public class Card {
        public string Name { get; set; }
        public string Text { get; set; }
        public CardType Type { get; set; }
        public float Value { get; set; }
        public Sprite Image { get; private set; }
        public Sprite Icon { get; private set; }



        public Card(string name, string text, CardType type, float value, Sprite image, Sprite icon) {
            Name = name;
            Text = text;
            Type = type;
            Value = value;
            Image = image;
            Icon = icon;
        }

        public void ApplyEffect() {
            switch (Type) {
                case CardType.Food:
                    G.main.civStats.IncreaseFood(Value);
                    break;
                case CardType.Hapiness:
                    G.main.civStats.IncreaseHappiness(Value);
                    break;
                case CardType.Number:
                    G.main.civStats.IncreasePopulation(Value);
                    break;
                case CardType.Common:
                    break;
                case CardType.God:
                    G.main.civStats.IncreasePopulation(Value);
                    G.main.civStats.IncreaseHappiness(Value);
                    G.main.civStats.IncreaseFood(Value);
                    break;
                case CardType.DevilUpPopDebugFood:
                    G.main.civStats.IncreasePopulation(Value);
                    G.main.civStats.IncreaseFood(-Value);
                    break;
                case CardType.DevilUpPopDebugHappi:
                    G.main.civStats.IncreasePopulation(Value);
                    G.main.civStats.IncreaseHappiness(-Value);
                    break;
                case CardType.DevilUpPopDebugFoodHappi:
                    G.main.civStats.IncreasePopulation(Value);
                    G.main.civStats.IncreaseFood(-Value);
                    G.main.civStats.IncreaseHappiness(-Value);
                    break;
            }
        }
    }
}