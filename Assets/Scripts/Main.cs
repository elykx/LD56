using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD56.Assets.Scripts {
    public class Main : MonoBehaviour {
        public Data data = new Data();
        public CivilizationStats civStats;

        public List<GameObject> citizensGroups;

        public GameObject god;
        public GameObject devil;

        // Список диалогов
        private string[] godDialogs = new string[]
        {
        "<!wait=1.5> Фух, кажется уже шестой день... <!wait=1.5> Сегодня я создам уникальных существ.",
        "<!wait=1.5> Я вижу, ты тоже не бездельничаешь. <!wait=1.5> Продолжай в том же духе!",
        "<!wait=1.5> Помни, что каждая ошибка - это возможность научиться."
        };

        private string[] devilDialogs = new string[]
        {
        "<!wait=1.5> Зачем тебе уникальные существа? Я могу сделать их сильнее!",
        "<!wait=1.5> Не забывай, мир полон тьмы, но также и света.",
        "<!wait=1.5> Ты думаешь, что всё контролируешь? Я научу тебя смирению."
        };

        void Awake() {
            G.main = this;
            G.data = data;
            civStats = GetComponent<CivilizationStats>();
        }

        void Start() {
            G.ui.SetGameState(GameState.Playing);
            G.ui.AddNewCard(CardLibrary.populationCard);
            G.ui.AddNewCard(CardLibrary.flowerCard);
            G.ui.AddNewCard(CardLibrary.devilDebugCard);
            StartCoroutine(InitialSequence());
            StartCoroutine(ProvideCards());

        }

        private IEnumerator InitialSequence() {
            ShowGod();
            G.ui.StartGodDialogue("<!wait=1.5> Фух, кажется уже шестой день... <!wait=1.5> Сегодня я создам уникальных существ.");
            G.audio.PlayMusic(G.NewBeginningTheme, true);
            yield return new WaitUntil(() => G.ui.IsGodDialogueFinished());

            // Устанавливаем количество людей после диалога
            civStats.SetPeopleNum(2);

            // Ждем 30 секунд перед появлением Дьявола
            yield return new WaitForSeconds(5f);
            if (G.currentState == GameState.Paused) yield return null;

            ShowDevil();
            G.ui.StartDevilDialogue("<!wait=1.5> Ого, у тебя тут что-то интересненькое... <wave>Я ПОГЛЯЖУ?!");
            yield return new WaitUntil(() => G.ui.IsDevilDialogueFinished());

            // Запуск корутины для рандомных диалогов и фиксированных по времени событий
            StartCoroutine(RandomAndFixedDialogueCoroutine());
        }

        private IEnumerator RandomAndFixedDialogueCoroutine() {
            // Запускаем фиксированные диалоги по времени
            StartCoroutine(FixedDialogueCoroutine());

            while (true) {
                if (G.ui.IsDevilDialogueFinished() && G.ui.IsGodDialogueFinished()) {
                    float waitTime = Random.Range(15f, 30f);
                    yield return new WaitForSeconds(waitTime);

                    if (Random.value > 0.5f) {
                        string randomGodDialog = godDialogs[Random.Range(0, godDialogs.Length)];
                        G.ui.StartGodDialogue(randomGodDialog);
                        yield return new WaitUntil(() => G.ui.IsGodDialogueFinished());
                    }
                    else {
                        ShowDevil();
                        string randomDevilDialog = devilDialogs[Random.Range(0, devilDialogs.Length)];
                        G.ui.StartDevilDialogue(randomDevilDialog);
                        yield return new WaitUntil(() => G.ui.IsDevilDialogueFinished());
                        HideDevil();
                    }
                }
                if (G.currentState == GameState.Paused) yield return null;

            }
        }

        private IEnumerator ProvideCards() {
            while (true) {
                int currentCardCount = G.ui.GetCurrentCardCount(); // Получаем текущее количество карточек

                // Проверяем условия для выдачи карточек
                if (currentCardCount < 3) {
                    // Выдача по одной карточке каждые 7 секунд
                    yield return new WaitForSeconds(7);
                    GiveRandomCard();
                }
                else if (currentCardCount < 5) {
                    // Выдача по одной карточке каждые 12 секунд
                    yield return new WaitForSeconds(12);
                    GiveRandomCard();
                }
                else {
                    // Если 5 или более, не выдавать карты
                    yield return null; // Ждем следующего кадра
                }
            }
        }

        private void GiveRandomCard() {
            Debug.Log("Give random card");
            if (CardLibrary.cards.Count > 0) {
                int randomIndex = Random.Range(0, CardLibrary.cards.Count);
                Card selectedCard = CardLibrary.cards[randomIndex];
                G.ui.AddNewCard(selectedCard);
            }
        }


        void Update() {
            ActiveCitizensGroup();
        }

        private IEnumerator FixedDialogueCoroutine() {
            // Диалог через 2 минуты
            yield return new WaitForSeconds(120f);
            if (G.currentState == GameState.Paused) yield return null;
            G.ui.StartGodDialogue("<!wait=1.5> Конкретный диалог через 2 минуты.");
            yield return new WaitUntil(() => G.ui.IsGodDialogueFinished());

            // Диалог через 5 минут
            yield return new WaitForSeconds(180f);
            if (G.currentState == GameState.Paused) yield return null;
            G.ui.StartDevilDialogue("<!wait=1.5> Конкретный диалог через 5 минут.");
            yield return new WaitUntil(() => G.ui.IsDevilDialogueFinished());

            // Диалог через 10 минут
            yield return new WaitForSeconds(300f);
            if (G.currentState == GameState.Paused) yield return null;
            G.ui.StartGodDialogue("<!wait=1.5> Конкретный диалог через 10 минут.");
            yield return new WaitUntil(() => G.ui.IsGodDialogueFinished());
        }

        private void ShowGod() {
            god.transform.localScale = Vector3.zero;
            god.SetActive(true);

            LeanTween.scale(god, Vector3.one * 3f, 1f).setEaseOutBack();
        }

        private void HideGod() {
            LeanTween.scale(god, Vector3.zero, 1f).setEaseInBack().setOnComplete(() => {
                god.SetActive(false);
            });
        }

        private void ShowDevil() {
            Debug.Log("showing devil");
            devil.transform.localScale = Vector3.zero;
            devil.SetActive(true);

            LeanTween.scale(devil, Vector3.one * 3f, 1f).setEaseOutBack();
        }

        private void HideDevil() {
            LeanTween.scale(devil, Vector3.zero, 1f).setEaseInBack().setOnComplete(() => {
                devil.SetActive(false);
            });
        }

        private void ActiveCitizensGroup() {
            var peopleNumber = G.data.PeopleNumber;

            if (peopleNumber >= 900) {
                citizensGroups[15].SetActive(true);
            }
            else if (peopleNumber >= 750) {
                citizensGroups[14].SetActive(true);
            }
            else if (peopleNumber >= 500) {
                citizensGroups[13].SetActive(true);
            }
            else if (peopleNumber >= 400) {
                citizensGroups[12].SetActive(true);
            }
            else if (peopleNumber >= 300) {
                citizensGroups[11].SetActive(true);
            }
            else if (peopleNumber >= 200) {
                citizensGroups[10].SetActive(true);
            }
            else if (peopleNumber >= 150) {
                citizensGroups[9].SetActive(true);
            }
            else if (peopleNumber >= 100) {
                citizensGroups[8].SetActive(true);
            }
            else if (peopleNumber >= 70) {
                citizensGroups[7].SetActive(true);
            }
            else if (peopleNumber >= 50) {
                citizensGroups[6].SetActive(true);
            }
            else if (peopleNumber >= 40) {
                citizensGroups[5].SetActive(true);
            }
            else if (peopleNumber >= 30) {
                citizensGroups[4].SetActive(true);
            }
            else if (peopleNumber >= 20) {
                citizensGroups[3].SetActive(true);
            }
            else if (peopleNumber >= 12) {
                citizensGroups[2].SetActive(true);
            }
            else if (peopleNumber >= 7) {
                citizensGroups[1].SetActive(true);
            }
            else if (peopleNumber >= 2) {
                citizensGroups[0].SetActive(true);
            }
        }
    }
}