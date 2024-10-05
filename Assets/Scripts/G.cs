using LD56.Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;

namespace LD56.Assets.Scripts {
    public enum GameState {
        Menu,
        Playing,
        Paused
    }

    public static class G {
        public static Main main;
        public static UI.UI ui;
        public static Audio audio;

        public static GameState currentState;
        public static Data data;

        // Названия клипов
        public const string MainTheme = "Music";
        public const string Effect = "OneEffect";

        public const string AudioPath = "Audio/";

        // Полные пути к аудиофайлам
        public const string MainThemePath = AudioPath + MainTheme;
        public const string EffectSoundPath = AudioPath + Effect;

        public static Color ColorFromRGB(int r, int g, int b) {
            return new Color(r / 255f, g / 255f, b / 255f);
        }
    }


}