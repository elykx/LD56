using UnityEngine;

namespace LD56.Assets.Scripts {
    public enum CardType {
        Number,
        Food,
        Hapiness,
        DevilDebug
    }

    public class Card {
        public string Text { get; set; }
        public CardType Type { get; set; }
        public float Value { get; set; }
        public Sprite Image { get; private set; }



        public Card(string text, CardType type, float value, Sprite image) {
            Text = text;
            Type = type;
            Value = value;
            Image = image;
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
                case CardType.DevilDebug:
                    break;
            }
        }
    }
}