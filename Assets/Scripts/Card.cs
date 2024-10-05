using UnityEngine;

namespace LD56.Assets.Scripts {
    public enum CardType {
        Number,
        Food,
        Hapiness,
        Health,
        Technology,
        DevilDebug
    }

    public class Card {
        public string Text { get; set; }
        public CardType Type { get; set; }
        public int Value { get; set; }
        public Color Color;

        public Card(string text, CardType type, int value) {
            Text = text;
            Type = type;
            Value = value;
            Color = G.ColorFromRGB(224, 223, 223);
        }

        public void ApplyEffect() {
            switch (Type) {
                case CardType.Food:
                    G.main.civStats.IncreaseFood(Value);
                    break;
                case CardType.Hapiness:
                    G.main.civStats.IncreaseHappiness(Value);
                    break;
                case CardType.Health:
                    G.main.civStats.IncreaseHealth(Value);
                    break;
                case CardType.Technology:
                    G.main.civStats.IncreaseTechnology(Value);
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