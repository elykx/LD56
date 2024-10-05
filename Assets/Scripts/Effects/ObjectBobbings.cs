using UnityEngine;

namespace LD56.Assets.Scripts.Effects {
    public class ObjectBobbings : MonoBehaviour {
        public float bobbingDistance = 0.5f;  // Дистанция покачивания
        public float bobbingDuration = 1f;    // Длительность одного цикла

        void Start() {
            // Покачивание первого объекта
            LeanTween.moveY(gameObject, gameObject.transform.position.y + bobbingDistance, bobbingDuration)
                     .setEaseInOutSine()  // Плавное покачивание
                     .setLoopPingPong();  // Цикл покачивания вверх-вниз
        }
    }
}