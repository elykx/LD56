using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LD56.Assets.Scripts {
    public static class CardLibrary {
        public static Card foodCard = new Card(
            "Create Food",
            CardType.Food,
            10f,
            G.ui.cardSprites.Find(s => s.name == "food")

        );

        public static Card hapinessCard = new Card(
            "Entertain Peoples",
            CardType.Hapiness,
            10f,
            G.ui.cardSprites.Find(s => s.name == "hapiness")
        );

        public static Card flowerCard = new Card(
            "Give people flowers",
            CardType.Hapiness,
            10f,
            G.ui.cardSprites.Find(s => s.name == "hapiness")
        );

        public static Card populationCard = new Card(
            "Up people population",
            CardType.Number,
            10f,
            G.ui.cardSprites.Find(s => s.name == "population")
        );

        public static Card devilDebugCard = new Card(
            "Devil Debug",
            CardType.DevilDebug,
            10f,
            G.ui.cardSprites.Find(s => s.name == "devil")
        );

        public static List<Card> cards = new List<Card>() {
            foodCard,
            hapinessCard,
            flowerCard,
            populationCard,
            devilDebugCard
        };
    }
}