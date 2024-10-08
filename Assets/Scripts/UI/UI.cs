using System;
using System.Collections;
using System.Collections.Generic;
using TMPEffects.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LD56.Assets.Scripts.UI {
    public class UI : MonoBehaviour {
        public GameObject gameCanvas;
        public GameObject failCanvas;
        public GameObject winCanvas;

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
        private Vector3 godDialogueStartPosition;
        private Vector3 devilDialogueStartPosition;

        private void Awake() {
            G.ui = this;
        }

        private void Start() {
            godDialogueStartPosition = godDialogue.transform.localPosition;
            devilDialogueStartPosition = devilDialogue.transform.localPosition;
        }

        void Update() {
            UpdateUI();
            UpdateHeader();
        }

        private void UpdateUI() {
            gameCanvas.SetActive(G.currentState == GameState.Playing);
            winCanvas.SetActive(G.currentState == GameState.Win);
            failCanvas.SetActive(G.currentState == GameState.Lose);
        }

        private void UpdateHeader() {
            numberPeople.text = Convert.ToInt32(G.data.PeopleNumber).ToString() + "/1000";
            hapinessPeople.text = Convert.ToInt32(G.data.PeopleHappiness).ToString();
            foodPeople.text = Convert.ToInt32(G.data.PeopleFood).ToString();
        }

        public void FinishGameLose() {
            SetGameState(GameState.Lose);
        }


        public void FinishGameWin() {
            SetGameState(GameState.Win);
        }

        public void SetGameState(GameState newState) {
            G.currentState = newState;

            if (newState == GameState.Paused || newState == GameState.Win || newState == GameState.Lose) {
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
                    var childName = cardObj.transform.Find("Name");

                    childBack.GetComponent<Image>().sprite = card.Image;
                    childImage.GetComponent<Image>().sprite = card.Icon;
                    childText.GetComponent<TextMeshProUGUI>().text = card.Text;
                    childName.GetComponent<TextMeshProUGUI>().text = card.Name;
                    cardObj.GetComponent<CardDragHandler>().Card = card;
                    cardObj.SetActive(true);
                    break;
                }
            }
        }

        public void StartGodDialogue(string text) {
            if (!isGodDialogueActive) {
                isGodDialogueActive = true;
                StartCoroutine(AutoCloseDialogue(godDialogue));
                ShowGodDialogue(text);
            }
        }

        public void StartDevilDialogue(string text) {
            if (!isDevilDialogueActive) {
                isDevilDialogueActive = true;
                ShowDevilDialogue(text);
                StartCoroutine(AutoCloseDialogue(devilDialogue));
            }
        }

        private IEnumerator AutoCloseDialogue(GameObject dialogueObject) {
            yield return new WaitForSeconds(dialogueDuration);
            CloseDialogue(dialogueObject);

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

            LeanTween.scale(godDialogue, Vector3.one, 2f)
                .setEaseOutBack()
                .setOnComplete(() => {
                    godDialogue.transform.localPosition = godDialogueStartPosition;
                });

            godDialogueText.text = text;
            textAnimatorGod.StartWriter();
        }

        private void ShowDevilDialogue(string text) {
            devilDialogue.transform.localScale = Vector3.zero;
            devilDialogueText.text = "";
            devilDialogue.SetActive(true);

            LeanTween.scale(devilDialogue, Vector3.one, 2f)
                .setEaseOutBack()
                .setOnComplete(() => {
                    devilDialogue.transform.localPosition = devilDialogueStartPosition;
                });

            devilDialogueText.text = text;
            textAnimatorDevil.StartWriter();
        }

        private void CloseDialogue(GameObject dialogueObject) {
            if (!dialogueObject.activeSelf) return;

            LeanTween.scale(dialogueObject, Vector3.zero, 2f).setEaseInBack().setOnComplete(() => {
                dialogueObject.SetActive(false);
            });
        }

        public bool IsGodDialogueFinished() => !isGodDialogueActive;
        public bool IsDevilDialogueFinished() => !isDevilDialogueActive;

        public int GetCurrentCardCount() {
            int activeCount = 0;

            foreach (GameObject card in poolCards) {
                if (card.activeSelf) {
                    activeCount++;
                }
            }

            return activeCount;
        }
    }
}