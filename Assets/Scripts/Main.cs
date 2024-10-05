using System.Collections.Generic;
using UnityEngine;

namespace LD56.Assets.Scripts  {
    public class Main : MonoBehaviour {
        public Data data = new Data();

        public CivilizationStats populationGrowth;
        void Awake() {
            G.main = this;
            G.data = data;
        }

        void Start() {
            G.ui.SetGameState(GameState.Playing);

        }

        void Update() {
            G.ui.debugText = G.currentState.ToString();
             if (Input.GetKeyDown(KeyCode.F) && G.currentState == GameState.Playing) {
                G.audio.PlaySoundEffect(G.Effect);
                populationGrowth.SetData(data);
            }
        }
    }
}