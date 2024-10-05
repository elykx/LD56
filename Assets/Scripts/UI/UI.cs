using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LD56.Assets.Scripts.UI {
    public class UI : MonoBehaviour {
        public TextMeshProUGUI debugTextUI; 
        public GameObject menuCanvas;  
        public GameObject gameCanvas;  
        public GameObject pauseCanvas;

        public TextMeshProUGUI numberPeople;
        public TextMeshProUGUI hapinessPeople;
        public TextMeshProUGUI healthPeople;
        public TextMeshProUGUI foodPeople;
        public TextMeshProUGUI technologyPeople;

        private void Awake() {
            G.ui = this;
        }

        void Update() {
            UpdateUI();

            if (Input.GetKeyDown(KeyCode.Escape) && G.currentState == GameState.Playing) {
                SetGameState(GameState.Paused);
                G.audio.PlaySoundEffect(G.Effect);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && G.currentState == GameState.Paused) {
                SetGameState(GameState.Playing);
                G.audio.PlayMusic(G.MainTheme, true);
            }
            else if (Input.GetKeyDown(KeyCode.E) && G.currentState == GameState.Paused) {
                SetGameState(GameState.Menu);
                G.audio.PlaySoundEffect(G.Effect);
            }
            else if (Input.GetKeyDown(KeyCode.E) && G.currentState == GameState.Menu) {
                SetGameState(GameState.Playing);
                G.audio.PlayMusic(G.MainTheme, true);
            }

            UpdateHeader();

            debugTextUI.text = debugText;
        }

        private void UpdateUI() {
            menuCanvas.SetActive(G.currentState == GameState.Menu);
            gameCanvas.SetActive(G.currentState == GameState.Playing);
            pauseCanvas.SetActive(G.currentState == GameState.Paused);
        }

        private void UpdateHeader() {
            numberPeople.text = G.data.PeopleNumber.ToString();
            hapinessPeople.text = Convert.ToInt32(G.data.PeopleHappiness).ToString();
            healthPeople.text = Convert.ToInt32(G.data.PeopleHealth).ToString();
            foodPeople.text = Convert.ToInt32(G.data.PeopleFood).ToString();
            technologyPeople.text = Convert.ToInt32(G.data.PeopleTechnology).ToString();
        }

        public void SetGameState(GameState newState) {
            G.currentState = newState;

            if (newState == GameState.Paused) {
                Time.timeScale = 0;
            }
            else {
                Time.timeScale = 1;
            }

            UpdateUI();
        }

        private string _debugText;
        public string debugText {
            get { return _debugText; }
            set {
                _debugText = value;
                debugTextUI.text = _debugText;
            }
        }
    }

}