using UnityEngine;

namespace LD56.Assets.Scripts {
    public class Movement : MonoBehaviour {
        public Collider2D movementArea;
        public float moveSpeed = 2f;
        public float changeDirectionTime = 2f;

        private Vector2 targetPosition;
        private float timer;

        void Start() {
            SetRandomTargetPosition();
        }

        void Update() {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            timer += Time.deltaTime;

            if (timer >= changeDirectionTime || (Vector2)transform.position == targetPosition) {
                SetRandomTargetPosition();
                timer = 0f;
            }
        }

        void SetRandomTargetPosition() {
            bool validPosition = false;
            while (!validPosition) {
                float randomX = Random.Range(movementArea.bounds.min.x, movementArea.bounds.max.x);
                float randomY = Random.Range(movementArea.bounds.min.y, movementArea.bounds.max.y);
                Vector2 randomPosition = new Vector2(randomX, randomY);

                if (movementArea.OverlapPoint(randomPosition)) {
                    targetPosition = randomPosition;
                    validPosition = true;
                }
            }
        }
    }
}