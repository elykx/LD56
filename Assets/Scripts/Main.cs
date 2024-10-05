using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD56.Assets.Scripts {
    public class Main : MonoBehaviour {
        public Data data = new Data();
        public CivilizationStats civStats;

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
            G.ui.AddNewCard(CardLibrary.healthCard);
            G.ui.AddNewCard(CardLibrary.flowerCard);
            G.ui.AddNewCard(CardLibrary.devilDebugCard);
            civStats.SetData(data);
            ShowGod();
            G.ui.StartGodDialogue("<!wait=1.5> Фух, кажется уже шестой день... <!wait=1.5> Сегодня я создам уникальных существ");
            civStats.SetPeopleNum(2);

            // Запускаем корутину для случайных диалогов
            StartCoroutine(RandomDialogueCoroutine());
        }

        void Update() {
        }

        private IEnumerator RandomDialogueCoroutine() {
            while (true) // Бесконечный цикл для постоянного вызова
            {
                if (G.ui.IsDevilDialogueFinished() && G.ui.IsGodDialogueFinished()) {
                    float waitTime = Random.Range(5f, 10f); // Рандомный интервал от 15 до 30 секунд
                    yield return new WaitForSeconds(waitTime);

                    // Выбор случайного персонажа и диалога
                    if (Random.value > 0.5f) // 50% шанс на выбор Бога или Дьявола
                    {
                        string randomGodDialog = godDialogs[Random.Range(0, godDialogs.Length)];
                        G.ui.StartGodDialogue(randomGodDialog);
                        yield return new WaitUntil(() => G.ui.IsGodDialogueFinished()); // Ждем завершения диалога
                    }
                    else {
                        ShowDevil();
                        string randomDevilDialog = devilDialogs[Random.Range(0, devilDialogs.Length)];
                        G.ui.StartDevilDialogue(randomDevilDialog);
                        yield return new WaitUntil(() => G.ui.IsDevilDialogueFinished()); // Ждем завершения диалога
                        HideDevil();
                    }
                }

            }
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
            god.transform.localScale = Vector3.zero;
            god.SetActive(true);

            LeanTween.scale(god, Vector3.one * 3f, 1f).setEaseOutBack();
        }

        private void HideDevil() {
            LeanTween.scale(god, Vector3.zero, 1f).setEaseInBack().setOnComplete(() => {
                god.SetActive(false);
            });
        }
    }
}