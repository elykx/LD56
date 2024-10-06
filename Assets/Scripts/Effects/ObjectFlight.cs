using UnityEngine;

namespace LD56.Assets.Scripts.Effects {
    public class ObjectFlight : MonoBehaviour {
        private Vector3 startPoint;

        public Transform targetPoint;

        public float flightDuration = 1f;

        public float deactivateDelay = 1f;

        void Start() {
            startPoint = transform.position;
        }

        public void StartFlight() {
            gameObject.SetActive(true);

            LeanTween.move(gameObject, targetPoint.position, flightDuration)
                .setOnComplete(OnFlightComplete);
        }

        private void OnFlightComplete() {
            LeanTween.delayedCall(deactivateDelay, () => {
                gameObject.SetActive(false);

                transform.position = startPoint;
            });
        }
    }
}