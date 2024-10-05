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

        public Card(string text, CardType type, int value) {
            Text = text;
            Type = type;
            Value = value;
        }
    }
}