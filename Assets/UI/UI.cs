using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LD56.Assets.UI {
    public class UI : MonoBehaviour {
        public TextMeshProUGUI debugTextUI; // UI элемент для отображения текста
        public GameObject menuCanvas;  // Canvas для главного меню
        public GameObject gameCanvas;  // Canvas для игрового процесса
        public GameObject pauseCanvas; // Canvas для паузы

        private void Awake() {
            // Инициализация ссылки на UI в статическом классе G
            G.ui = this;
        }

        void Update() {

            UpdateUI();

            // Пример переключения состояний с клавиатуры (например, на паузу)
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

            // Вывод текста дебага
            debugTextUI.text = debugText;
        }

        // Метод для обновления состояния UI в зависимости от состояния игры
        private void UpdateUI() {
            menuCanvas.SetActive(G.currentState == GameState.Menu);
            gameCanvas.SetActive(G.currentState == GameState.Playing);
            pauseCanvas.SetActive(G.currentState == GameState.Paused);
        }

        // Метод для смены состояния игры
        public void SetGameState(GameState newState) {
            G.currentState = newState;

            // Пример логики для управления временем игры
            if (newState == GameState.Paused) {
                Time.timeScale = 0;
            }
            else {
                Time.timeScale = 1;
            }

            UpdateUI(); // Обновляем UI в зависимости от состояния
        }

        // Свойство для управления текстом дебага
        private string _debugText;
        public string debugText {
            get { return _debugText; }
            set {
                _debugText = value;
                debugTextUI.text = _debugText; // обновляем UI текст сразу
            }
        }
    }

}