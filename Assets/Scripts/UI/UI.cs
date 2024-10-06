using System;
using System.Collections;
using System.Collections.Generic;
using TMPEffects.Components;
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
        public TextMeshProUGUI foodPeople;


        public List<GameObject> poolCards;

        public GameObject godDialogue;
        public GameObject devilDialogue;
        public TextMeshProUGUI godDialogueText;
        public TextMeshProUGUI devilDialogueText;
        bool isGodDialogueActive = false;
        bool isDevilDialogueActive = false;
        public TMPWriter textAnimatorGod;
        public TMPWriter textAnimatorDevil;

        public List<Sprite> cardSprites;


        private float dialogueDuration = 6f;

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
            }
            else if (Input.GetKeyDown(KeyCode.E) && G.currentState == GameState.Paused) {
                SetGameState(GameState.Menu);
                G.audio.PlaySoundEffect(G.Effect);
            }
            else if (Input.GetKeyDown(KeyCode.E) && G.currentState == GameState.Menu) {
                SetGameState(GameState.Playing);
            }

            UpdateHeader();
        }

        private void UpdateUI() {
            menuCanvas.SetActive(G.currentState == GameState.Menu);
            gameCanvas.SetActive(G.currentState == GameState.Playing);
            pauseCanvas.SetActive(G.currentState == GameState.Paused);
        }

        private void UpdateHeader() {
            Debug.Log(G.data.PeopleNumber);
            numberPeople.text = Convert.ToInt32(G.data.PeopleNumber).ToString();
            hapinessPeople.text = Convert.ToInt32(G.data.PeopleHappiness).ToString();
            foodPeople.text = Convert.ToInt32(G.data.PeopleFood).ToString();
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

        public void AddNewCard(Card card) {
            Debug.Log("Give  card " + card.Text.ToString());
            foreach (var cardObj in poolCards) {
                if (cardObj.activeSelf == false) {
                    var childBack = cardObj.transform.Find("Background");
                    var childImage = cardObj.transform.Find("Image");
                    var childText = cardObj.transform.Find("Text");

                    childBack.GetComponent<Image>().sprite = card.Image;
                    childText.GetComponent<TextMeshProUGUI>().text = card.Text;
                    cardObj.GetComponent<CardDragHandler>().Card = card;
                    cardObj.SetActive(true);
                    break;
                }
            }
        }

        public void StartGodDialogue(string text) {
            if (!isGodDialogueActive) {
                isGodDialogueActive = true;
                StartCoroutine(AutoCloseDialogue(godDialogue)); // Запускаем корутину
                ShowGodDialogue(text);
            }
        }

        public void StartDevilDialogue(string text) {
            if (!isDevilDialogueActive) {
                isDevilDialogueActive = true;
                ShowDevilDialogue(text);
                StartCoroutine(AutoCloseDialogue(devilDialogue)); // Запускаем корутину
            }
        }

        private IEnumerator AutoCloseDialogue(GameObject dialogueObject) {
            Debug.Log("start timer");
            yield return new WaitForSeconds(dialogueDuration);
            Debug.Log("end timer");
            CloseDialogue(dialogueObject);

            // Обновляем состояние активности диалога
            if (dialogueObject == godDialogue) {
                isGodDialogueActive = false;
            }
            else if (dialogueObject == devilDialogue) {
                isDevilDialogueActive = false;
            }
        }
        private void ShowGodDialogue(string text) {
            godDialogue.transform.localScale = Vector3.zero;
            godDialogueText.text = "";
            godDialogue.SetActive(true);

            LeanTween.scale(godDialogue, Vector3.one, 2f).setEaseOutBack();

            godDialogueText.text = text;
            textAnimatorGod.StartWriter();
        }

        private void ShowDevilDialogue(string text) {
            devilDialogue.transform.localScale = Vector3.zero; // Устанавливаем начальный масштаб 0
            devilDialogueText.text = "";
            devilDialogue.SetActive(true);

            LeanTween.scale(devilDialogue, Vector3.one, 2f).setEaseOutBack();

            devilDialogueText.text = text;
            textAnimatorDevil.StartWriter();
        }

        private void CloseDialogue(GameObject dialogueObject) {
            Debug.Log("Attempting to close dialogue for object: " + dialogueObject.name + " Active: " + dialogueObject.activeSelf);
            // Проверяем, активен ли объект, чтобы избежать попытки закрытия уже неактивного объекта
            if (!dialogueObject.activeSelf) return;

            Debug.Log("close dialogue");
            LeanTween.scale(dialogueObject, Vector3.zero, 2f).setEaseInBack().setOnComplete(() => {
                dialogueObject.SetActive(false); // Отключаем объект после завершения анимации
                Debug.Log("dialogue closed");
            });
        }

        // Методы для проверки завершения диалога
        public bool IsGodDialogueFinished() => !isGodDialogueActive;
        public bool IsDevilDialogueFinished() => !isDevilDialogueActive;

        public int GetCurrentCardCount() {
            int activeCount = 0;

            // Проходим по всем карточкам и считаем только активные
            foreach (GameObject card in poolCards) {
                if (card.activeSelf) {
                    activeCount++;
                }
            }

            return activeCount;
        }
    }
}