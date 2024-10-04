using LD56.Assets.UI;
namespace LD56.Assets {
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

        // Названия клипов
        public const string MainTheme = "Music";
        public const string Effect = "OneEffect";

        public const string AudioPath = "Audio/";

        // Полные пути к аудиофайлам
        public const string MainThemePath = AudioPath + MainTheme;
        public const string EffectSoundPath = AudioPath + Effect;
    }


    // public class ManagedBehaviour : MonoBehaviour
    // {
    //     void Update()
    //     {
    //         if (!G.isPaused)
    //         {
    //             PausableUpdate();
    //         }
    //     }
    //     public virtual void PausableUpdate()
    //     {

    //     }

    //     void FixedUpdate()
    //     {
    //         if (!G.isPaused)
    //         {
    //             PausableUpdate();
    //         }
    //     }

    //     public virtual void PausableFixedUpdate()
    //     {

    //     }
    // }



}