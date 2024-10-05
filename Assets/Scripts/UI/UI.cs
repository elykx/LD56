using System;
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
        public TextMeshProUGUI healthPeople;
        public TextMeshProUGUI foodPeople;
        public TextMeshProUGUI technologyPeople;

        public List<GameObject> poolCards;

        public GameObject godDialogue;
        public GameObject devilDialogue;
        public TextMeshProUGUI godDialogueText;
        public TextMeshProUGUI devilDialogueText;
        bool isGodDialogueActive = false;
        bool isDevilDialogueActive = false;
        public TMPWriter textAnimatorGod;
        public TMPWriter textAnimatorDevil;

        private float dialogueDuration = 15f;
        private float godDialogueStartTime;
        private float devilDialogueStartTime;

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
            UpdateDialogue();
        }

        private void UpdateUI() {
            menuCanvas.SetActive(G.currentState == GameState.Menu);
            gameCanvas.SetActive(G.currentState == GameState.Playing);
            pauseCanvas.SetActive(G.currentState == GameState.Paused);
        }

        private void UpdateHeader() {
            numberPeople.text = Convert.ToInt32(G.data.PeopleNumber).ToString();
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

        public void AddNewCard(Card card) {
            foreach (var cardObj in poolCards) {
                if (cardObj.activeSelf == false) {
                    var childBack = cardObj.transform.Find("Background");
                    var childImage = cardObj.transform.Find("Image");
                    var childText = cardObj.transform.Find("Text");

                    childBack.GetComponent<Image>().color = card.Color;
                    // childImage.GetComponent<Image>().sprite = card.Image;
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
                devilDialogueStartTime = Time.time;
                ShowGodDialogue(text);
            }
        }

        public void StartDevilDialogue(string text) {
            if (!isDevilDialogueActive) {
                isDevilDialogueActive = true;
                godDialogueStartTime = Time.time;
                ShowDevilDialogue(text);
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

        private void UpdateDialogue() {
            if (isGodDialogueActive) {
                if (Input.GetMouseButtonDown(0)) {
                    CloseDialogue(godDialogue);
                    isGodDialogueActive = false;
                }

                if (Time.time - godDialogueStartTime > dialogueDuration) {
                    CloseDialogue(godDialogue);
                    isGodDialogueActive = false;
                }
            }

            if (isDevilDialogueActive) {
                if (Input.GetMouseButtonDown(0)) {
                    CloseDialogue(devilDialogue);
                    isDevilDialogueActive = false;
                }

                if (Time.time - devilDialogueStartTime > dialogueDuration) {
                    CloseDialogue(devilDialogue);
                    isDevilDialogueActive = false;
                }
            }
        }

        private void CloseDialogue(GameObject dialogueObject) {
            LeanTween.scale(dialogueObject, Vector3.zero, 2f).setEaseInBack().setOnComplete(() => {
                dialogueObject.SetActive(false);
            });
        }

        // Методы для проверки завершения диалога
        public bool IsGodDialogueFinished() => !isGodDialogueActive;
        public bool IsDevilDialogueFinished() => !isDevilDialogueActive;
    }

}