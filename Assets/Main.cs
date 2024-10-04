using UnityEngine;
namespace LD56.Assets.UI {
    public class Main : MonoBehaviour {
        void Awake() {
            G.main = this;
        }

        void Start() {
            G.ui.SetGameState(GameState.Playing);

        }

        void Update() {
            G.ui.debugText = G.currentState.ToString();
        }
    }

}