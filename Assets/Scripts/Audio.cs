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
            musicClips.Add(G.MainTheme, Resources.Load<AudioClip>(G.MainThemePath));
            soundEffects.Add(G.Effect, Resources.Load<AudioClip>(G.EffectSoundPath));

            if (musicClips[G.MainTheme] == null)
                Debug.LogError($"Не удалось загрузить музыкальный клип {G.MainThemePath}");
            if (soundEffects[G.Effect] == null)
                Debug.LogError($"Не удалось загрузить звуковой эффект {G.EffectSoundPath}");
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