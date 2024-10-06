using System.Collections.Generic;
using UnityEngine;

namespace LD56.Assets.Scripts {
    public class Audio : MonoBehaviour {
        public AudioSource musicSource;
        public AudioSource effectsSource;

        public Dictionary<string, AudioClip> musicClips = new Dictionary<string, AudioClip>();
        public Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();

        private void Awake() {
            G.audio = this;
            musicClips.Add(G.NewBeginningTheme, Resources.Load<AudioClip>(G.NewBeginningPath));
            musicClips.Add(G.EndingTheme, Resources.Load<AudioClip>(G.EndingThemePath));
            soundEffects.Add(G.Effect, Resources.Load<AudioClip>(G.EffectSoundPath));
        }

        public void PlayMusic(string clipName, bool loop = false) {
            if (musicClips.ContainsKey(clipName)) {
                musicSource.clip = musicClips[clipName];
                musicSource.loop = loop;
                musicSource.Play();
            }
            else {
                Debug.LogWarning($"Музыка с ключом {clipName} не найдена");
            }
        }

        public void StopMusic() {
            musicSource.Stop();
        }

        public void PlaySoundEffect(string clipName) {
            if (soundEffects.ContainsKey(clipName)) {
                effectsSource.PlayOneShot(soundEffects[clipName]);
            }
            else {
                Debug.LogWarning($"Звуковой эффект с ключом {clipName} не найден");
            }
        }

        public void SetMusicVolume(float volume) {
            musicSource.volume = volume;
        }

        public void SetEffectsVolume(float volume) {
            effectsSource.volume = volume;
        }
    }
}