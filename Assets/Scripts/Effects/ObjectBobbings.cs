using UnityEngine;

namespace LD56.Assets.Scripts.Effects {
    public class ObjectBobbings : MonoBehaviour {
        public float bobbingDistance = 0.5f; 
        public float bobbingDuration = 1f;    

        void Start() {
            // Покачивание первого объекта
            LeanTween.moveY(gameObject, gameObject.transform.position.y + bobbingDistance, bobbingDuration)
                     .setEaseInOutSine()  
                     .setLoopPingPong(); 
        }
    }
}