using UnityEngine;
using UnityEngine.EventSystems;

namespace LD56.Assets.Scripts.UI {

    public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Vector2 startPosition;
        private bool isDragging = false;

        public Card Card;

        void Awake() {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            startPosition = rectTransform.anchoredPosition;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            canvasGroup.alpha = 0.8f;
            canvasGroup.blocksRaycasts = false;
            isDragging = true;

        }

        public void OnDrag(PointerEventData eventData) {
            // Получаем текущую позицию мыши в экранных координатах
            Vector2 mousePosition = eventData.position;

            // Устанавливаем новую позицию карты без учета родительского RectTransform
            rectTransform.position = mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData) {
            rectTransform.anchoredPosition = startPosition;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            gameObject.SetActive(false);
            isDragging = false;

            Card.ApplyEffect();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            LeanTween.moveY(rectTransform, rectTransform.anchoredPosition.y + 50f, 0.2f).setEase(LeanTweenType.easeOutBack);
        }

        public void OnPointerExit(PointerEventData eventData) {
            if (!isDragging) { 
                LeanTween.moveY(rectTransform, rectTransform.anchoredPosition.y - 50f, 0.2f).setEase(LeanTweenType.easeInBack);
            }
        }
    }
}