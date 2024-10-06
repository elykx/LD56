using LD56.Assets.Scripts.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD56.Assets.Scripts {
    public class Main : MonoBehaviour {
        public Data data = new Data();
        public CivilizationStats civStats;

        public List<GameObject> citizensGroups;
        public List<GameObject> buildingsGroups;
        public List<ObjectFlight> meteors;
        public List<GameObject> storms;

        public GameObject god;
        public GameObject devil;


        private int[] populationThresholds = { 100, 500, 800 };
        private bool[] eventsTriggered;

        private string[] godDialogs = new string[]
        {
        "<!wait=1.5> They're just starting... Don't be so harsh on them",
        "<!wait=1.5> I help them because they need it. Everyone deserves a chance",
        "<!wait=1.5> They have potential. Even the smallest spark can ignite the flame of life",
        "<!wait=1.5> I didn’t create them to destroy them. They need to learn, to make mistakes",
        };

        private string[] devilDialogs = new string[]
        {
        "<!wait=1.5> Potential? Ha! There's more weakness in them than strength",
        "<!wait=1.5> Mistakes are wonderful... They’ll only prove how pathetic they are",
        "<!wait=1.5> You really think you're in control of everything? Ha! I will teach you humility."
        };

        private float timeMorecardWait = 10f;
        private float timeLowcardWait = 6f;

        void Awake() {
            G.main = this;
            G.data = data;
            civStats = GetComponent<CivilizationStats>();
        }

        void Start() {
            G.ui.SetGameState(GameState.Playing);
            GiveRandomCard();
            GiveRandomCard();
            GiveRandomCard();
            StartCoroutine(InitialSequence());
            StartCoroutine(ProvideCards());

            eventsTriggered = new bool[populationThresholds.Length];
        }

        void Update() {
            ActiveCitizensGroup();
            CheckPopulationEvents();
            EndUpdate();
        }

        private void EndUpdate() {
            if (G.data.PeopleNumber <= 0) {
                G.ui.SetGameState(GameState.Lose);
            }
            if (G.data.PeopleNumber >= 1000) {
                G.ui.SetGameState(GameState.Win);
            }
        }

        private void CheckPopulationEvents() {
            float currentPopulation = G.data.PeopleNumber;

            for (int i = 0; i < populationThresholds.Length; i++) {
                if (currentPopulation >= populationThresholds[i] && !eventsTriggered[i]) {
                    if (G.ui.IsDevilDialogueFinished()) {
                        TriggerEventByIndex(i);
                        eventsTriggered[i] = true;
                    }
                }
            }
        }

        private void TriggerEventByIndex(int i) {
            if (i == 0) {
                StartCoroutine(FirstDemonEvent());
            }
            else if (i == 1) {
                StartCoroutine(SecondDemonEvent());
            }
            else {
                StartCoroutine(ThirdDemonEvent());
            }
        }

        private IEnumerator FirstDemonEvent() {
            ShowDevil();
            G.ui.StartDevilDialogue("<!wait=1.5> I'm getting a little bored... <wave> SHALL WE HAVE SOME FUN?");
            if (Random.value > 0.5f) {
                meteors.ForEach(m => m.StartFlight());
                yield return new WaitForSeconds(2f);
            }
            else {
                storms.ForEach(s => s.SetActive(true));
                yield return new WaitForSeconds(4f);
                storms.ForEach(s => s.SetActive(false));
            }
            G.data.PeopleNumber *= 0.5f;
            G.data.PeopleFood *= 0.5f;
            G.data.PeopleHappiness *= 0.5f;
            yield return new WaitUntil(() => G.ui.IsDevilDialogueFinished());
            HideDevil();
        }

        private IEnumerator SecondDemonEvent() {
            ShowDevil();
            G.ui.StartDevilDialogue("<!wait=1.5> These creatures seem absolutely worthless... What do you see in them?");
            if (Random.value > 0.5f) {
                meteors.ForEach(m => m.StartFlight());
                yield return new WaitForSeconds(2f);
            }
            else {
                storms.ForEach(s => s.SetActive(true));
                yield return new WaitForSeconds(4f);
                storms.ForEach(s => s.SetActive(false));
            }
            G.data.PeopleNumber *= 0.5f;
            G.data.PeopleFood *= 0.5f;
            G.data.PeopleHappiness *= 0.5f;
            yield return new WaitUntil(() => G.ui.IsDevilDialogueFinished());
            HideDevil();
        }

        private IEnumerator ThirdDemonEvent() {
            ShowDevil();
            G.ui.StartDevilDialogue("<!wait=1.5> Why are you helping them? Let's just end it all right now...");
            if (Random.value > 0.5f) {
                meteors.ForEach(m => m.StartFlight());
                yield return new WaitForSeconds(2f);
            }
            else {
                storms.ForEach(s => s.SetActive(true));
                yield return new WaitForSeconds(4f);
                storms.ForEach(s => s.SetActive(false));
            }
            G.data.PeopleNumber *= 0.75f;
            G.data.PeopleFood *= 0.3f;
            G.data.PeopleHappiness *= 0.3f;
            yield return new WaitUntil(() => G.ui.IsDevilDialogueFinished());
            HideDevil();
        }

        private IEnumerator InitialSequence() {
            ShowGod();
            G.ui.StartGodDialogue("<!wait=1.5> Phew, seems like it's the sixth day already... <!wait=1.5> Today, I will create unique beings");
            G.audio.PlayMusic(G.NewBeginningTheme, true);
            yield return new WaitUntil(() => G.ui.IsGodDialogueFinished());

            yield return new WaitForSeconds(5f);
            if (G.currentState == GameState.Paused) yield return null;

            ShowDevil();
            G.ui.StartDevilDialogue("<!wait=1.5> Oh, what's this interesting little thing you've got here... <wave> MIND IF I TAKE A LOOK?");
            yield return new WaitUntil(() => G.ui.IsDevilDialogueFinished());
            HideDevil();

            StartCoroutine(RandomAndFixedDialogueCoroutine());
        }

        private IEnumerator RandomAndFixedDialogueCoroutine() {
            while (true) {
                float waitTime = Random.Range(15f, 30f);
                yield return new WaitForSeconds(waitTime);
                if (G.ui.IsDevilDialogueFinished() && G.ui.IsGodDialogueFinished()) {
                    if (Random.value > 0.5f) {
                        string randomGodDialog = godDialogs[Random.Range(0, godDialogs.Length)];
                        G.ui.StartGodDialogue(randomGodDialog);
                        yield return new WaitUntil(() => G.ui.IsGodDialogueFinished());
                    }
                    else {
                        ShowDevil();
                        string randomDevilDialog = devilDialogs[Random.Range(0, devilDialogs.Length)];
                        G.ui.StartDevilDialogue(randomDevilDialog);
                        if (Random.value > 0.75f) {
                            meteors.ForEach(m => m.StartFlight());
                            yield return new WaitForSeconds(2f);
                        }
                        else if (Random.value > 0.5f) {
                            storms.ForEach(s => s.SetActive(true));
                            yield return new WaitForSeconds(4f);
                            storms.ForEach(s => s.SetActive(false));
                        }
                        G.data.PeopleNumber *= 0.8f;
                        G.data.PeopleFood *= 0.8f;
                        G.data.PeopleHappiness *= 0.8f;
                        yield return new WaitUntil(() => G.ui.IsDevilDialogueFinished());
                        HideDevil();
                    }
                }
                if (G.currentState == GameState.Paused) yield return null;
            }
        }

        private IEnumerator ProvideCards() {
            while (true) {
                int currentCardCount = G.ui.GetCurrentCardCount();

                if (currentCardCount < 3) {
                    yield return new WaitForSeconds(timeLowcardWait);
                    GiveRandomCard();
                }
                else if (currentCardCount < 5) {
                    yield return new WaitForSeconds(timeMorecardWait);
                    GiveRandomCard();
                }
                else {
                    yield return null;
                }
            }
        }

        private void GiveRandomCard() {
            if (CardLibrary.cards.Count > 0) {
                int randomIndex = Random.Range(0, CardLibrary.cards.Count);
                Card selectedCard = CardLibrary.cards[randomIndex];
                G.ui.AddNewCard(selectedCard);
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
            devil.transform.localScale = Vector3.zero;
            devil.SetActive(true);

            LeanTween.scale(devil, Vector3.one * 3.5f, 1f).setEaseOutBack();
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
                buildingsGroups[15].SetActive(true);
            }
            else if (peopleNumber >= 750) {
                citizensGroups[15].SetActive(false);
                buildingsGroups[15].SetActive(false);

                citizensGroups[14].SetActive(true);
                buildingsGroups[14].SetActive(true);
            }
            else if (peopleNumber >= 500) {
                for (int i = 14; i < citizensGroups.Count; i++) {
                    citizensGroups[i].SetActive(false);
                    buildingsGroups[i].SetActive(false);
                }
                citizensGroups[13].SetActive(true);
                buildingsGroups[13].SetActive(true);
            }
            else if (peopleNumber >= 400) {
                for (int i = 13; i < citizensGroups.Count; i++) {
                    citizensGroups[i].SetActive(false);
                    buildingsGroups[i].SetActive(false);
                }
                citizensGroups[12].SetActive(true);
                buildingsGroups[12].SetActive(true);
            }
            else if (peopleNumber >= 300) {
                for (int i = 12; i < citizensGroups.Count; i++) {
                    citizensGroups[i].SetActive(false);
                    buildingsGroups[i].SetActive(false);
                }
                citizensGroups[11].SetActive(true);
                buildingsGroups[11].SetActive(true);
            }
            else if (peopleNumber >= 200) {
                for (int i = 11; i < citizensGroups.Count; i++) {
                    citizensGroups[i].SetActive(false);
                    buildingsGroups[i].SetActive(false);
                }
                citizensGroups[10].SetActive(true);
                buildingsGroups[10].SetActive(true);
            }
            else if (peopleNumber >= 150) {
                for (int i = 10; i < citizensGroups.Count; i++) {
                    citizensGroups[i].SetActive(false);
                    buildingsGroups[i].SetActive(false);
                }
                citizensGroups[9].SetActive(true);
                buildingsGroups[9].SetActive(true);
            }
            else if (peopleNumber >= 100) {
                for (int i = 9; i < citizensGroups.Count; i++) {
                    citizensGroups[i].SetActive(false);
                    buildingsGroups[i].SetActive(false);
                }
                citizensGroups[8].SetActive(true);
                buildingsGroups[8].SetActive(true);
            }
            else if (peopleNumber >= 70) {
                for (int i = 8; i < citizensGroups.Count; i++) {
                    citizensGroups[i].SetActive(false);
                    buildingsGroups[i].SetActive(false);
                }
                citizensGroups[7].SetActive(true);
                buildingsGroups[7].SetActive(true);
            }
            else if (peopleNumber >= 50) {
                for (int i = 7; i < citizensGroups.Count; i++) {
                    citizensGroups[i].SetActive(false);
                    buildingsGroups[i].SetActive(false);
                }
                citizensGroups[6].SetActive(true);
                buildingsGroups[6].SetActive(true);
            }
            else if (peopleNumber >= 40) {
                for (int i = 6; i < citizensGroups.Count; i++) {
                    citizensGroups[i].SetActive(false);
                    buildingsGroups[i].SetActive(false);
                }
                citizensGroups[5].SetActive(true);
                buildingsGroups[5].SetActive(true);
            }
            else if (peopleNumber >= 30) {
                for (int i = 5; i < citizensGroups.Count; i++) {
                    citizensGroups[i].SetActive(false);
                    buildingsGroups[i].SetActive(false);
                }
                citizensGroups[4].SetActive(true);
                buildingsGroups[4].SetActive(true);
            }
            else if (peopleNumber >= 20) {
                for (int i = 4; i < citizensGroups.Count; i++) {
                    citizensGroups[i].SetActive(false);
                    buildingsGroups[i].SetActive(false);
                }
                citizensGroups[3].SetActive(true);
                buildingsGroups[3].SetActive(true);
            }
            else if (peopleNumber >= 12) {
                for (int i = 3; i < citizensGroups.Count; i++) {
                    citizensGroups[i].SetActive(false);
                    buildingsGroups[i].SetActive(false);
                }
                citizensGroups[2].SetActive(true);
                buildingsGroups[2].SetActive(true);
            }
            else if (peopleNumber >= 7) {
                for (int i = 2; i < citizensGroups.Count; i++) {
                    citizensGroups[i].SetActive(false);
                    buildingsGroups[i].SetActive(false);
                }
                citizensGroups[1].SetActive(true);
                buildingsGroups[1].SetActive(true);
            }
            else if (peopleNumber >= 2) {
                for (int i = 1; i < citizensGroups.Count; i++) {
                    citizensGroups[i].SetActive(false);
                    buildingsGroups[i].SetActive(false);
                }
                citizensGroups[0].SetActive(true);
                buildingsGroups[0].SetActive(true);
            }
            else {
                for (int i = 0; i < citizensGroups.Count; i++) {
                    citizensGroups[i].SetActive(false);
                    buildingsGroups[i].SetActive(false);
                }
            }
        }
        public void Restart() {
            string scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }
}
