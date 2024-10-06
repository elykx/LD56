using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LD56.Assets.Scripts {
    public static class CardLibrary {
        public static Card foodCard = new Card(
            "Create Food",
            CardType.Food,
            10f
        );

        public static Card hapinessCard = new Card(
            "Entertain Peoples",
            CardType.Hapiness,
            10f
        );

        public static Card flowerCard = new Card(
            "Give people flowers",
            CardType.Hapiness,
            10f
        );

        public static Card populationCard = new Card(
            "Up people population",
            CardType.Number,
            10f
        );

        public static Card devilDebugCard = new Card(
            "Devil Debug",
            CardType.DevilDebug,
            10f
        );
    }
}