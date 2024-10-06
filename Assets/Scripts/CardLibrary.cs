using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LD56.Assets.Scripts {
    public static class CardLibrary {
        public static Card foodCard = new Card(
            "Blessing of the harvest",
            "increases food <sprite=0> +20",
            CardType.Food,
            20f,
            G.ui.cardSprites.Find(s => s.name == "food"),
            G.ui.cardSprites.Find(s => s.name == "foodIcon")
        );

        public static Card foodCard2 = new Card(
            "Food supplies",
            "increases food <sprite=0> +10",
            CardType.Food,
            10f,
            G.ui.cardSprites.Find(s => s.name == "food"),
            G.ui.cardSprites.Find(s => s.name == "foodIcon")
        );

        public static Card foodCard3 = new Card(
            "The Feast of the Gods",
            "increases food <sprite=0> +30",
            CardType.Food,
            30f,
            G.ui.cardSprites.Find(s => s.name == "food"),
            G.ui.cardSprites.Find(s => s.name == "foodIcon")
        );

        public static Card hapinessCard = new Card(
            "A Word of Encouragement",
            "increases happiness <sprite=1> +20",
            CardType.Hapiness,
            20f,
            G.ui.cardSprites.Find(s => s.name == "hapiness"),
            G.ui.cardSprites.Find(s => s.name == "happiIcon")
        );

        public static Card hapinessCard2 = new Card(
            "Festival of Joy",
            "increases happiness <sprite=1> +10",
            CardType.Hapiness,
            10f,
            G.ui.cardSprites.Find(s => s.name == "hapiness"),
            G.ui.cardSprites.Find(s => s.name == "happiIcon")
        );

        public static Card hapinessCard3 = new Card(
           "A bright future",
           "increases happiness <sprite=1> +30",
           CardType.Hapiness,
           30f,
           G.ui.cardSprites.Find(s => s.name == "hapiness"),
           G.ui.cardSprites.Find(s => s.name == "happiIcon")
       );

        public static Card populationCard = new Card(
            "The Blessing of Birth",
            "increases population <sprite=2> +20",
            CardType.Number,
            20f,
            G.ui.cardSprites.Find(s => s.name == "population"),
            G.ui.cardSprites.Find(s => s.name == "popIcon")
        );

        public static Card populationCard2 = new Card(
            "Mother Earth",
            "increases population <sprite=2> +30",
            CardType.Number,
            30f,
            G.ui.cardSprites.Find(s => s.name == "population"),
            G.ui.cardSprites.Find(s => s.name == "popIcon")
        );

        public static Card populationCard3 = new Card(
            "Family Union",
            "increases population <sprite=2> +10",
            CardType.Number,
            10f,
            G.ui.cardSprites.Find(s => s.name == "population"),
            G.ui.cardSprites.Find(s => s.name == "popIcon")
        );

        public static Card godCard = new Card(
            "The Intervention of the Eternal",
            "increases <br> <sprite=0> <sprite=1> <sprite=2> +20",
            CardType.God,
            20f,
            G.ui.cardSprites.Find(s => s.name == "god"),
            G.ui.cardSprites.Find(s => s.name == "godIcon")
        );

        public static Card godCard1 = new Card(
            "Harmony",
            "increases <br> <sprite=0> <sprite=1> <sprite=2> +10",
            CardType.God,
            10f,
            G.ui.cardSprites.Find(s => s.name == "god"),
            G.ui.cardSprites.Find(s => s.name == "godIcon")
        );

        public static Card devipUpPopDebugFood = new Card(
            "The Darkness of Truth",
            "<sprite=3>",
            CardType.DevilUpPopDebugFood,
            10f,
            G.ui.cardSprites.Find(s => s.name == "devil"),
            G.ui.cardSprites.Find(s => s.name == "devilIcon")
        );

        public static Card devipUpPopDebugHappi = new Card(
            "Malicious laughter",
            "<sprite=3>",
            CardType.DevilUpPopDebugHappi,
            15f,
            G.ui.cardSprites.Find(s => s.name == "devil"),
            G.ui.cardSprites.Find(s => s.name == "devilIcon")
        );

        public static Card devipUpPopDebugHappiFood = new Card(
            "Curse",
            "<sprite=3>",
            CardType.DevilUpPopDebugFoodHappi,
            10f,
            G.ui.cardSprites.Find(s => s.name == "devil"),
            G.ui.cardSprites.Find(s => s.name == "devilIcon")
        );

        public static Card common1 = new Card(
            "Admire the sun",
            "It's good to walk in the sun",
            CardType.Common,
            0f,
            G.ui.cardSprites.Find(s => s.name == "common"),
            G.ui.cardSprites.Find(s => s.name == "commonIcon")
        );

        public static Card common2 = new Card(
            "Walking on the grass",
            "It's good to walk on the grass",
            CardType.Common,
            0f,
            G.ui.cardSprites.Find(s => s.name == "common"),
            G.ui.cardSprites.Find(s => s.name == "commonIcon")
        );

        public static List<Card> cards = new List<Card>() {
            foodCard,
            foodCard2,
            foodCard3,
            hapinessCard,
            hapinessCard2,
            hapinessCard3,
            populationCard,
            populationCard2,
            populationCard3,
            godCard,
            godCard1,
            devipUpPopDebugFood,
            devipUpPopDebugHappi,
            devipUpPopDebugHappiFood,
            common1,
            common2
        };
    }
}