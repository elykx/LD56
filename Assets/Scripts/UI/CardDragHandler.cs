using UnityEngine;
using UnityEngine.EventSystems;

namespace LD56.Assets.Scripts.UI {

    public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Vector2 startPosition;
        private Vector2 offset;

        public Card Card;

        void Awake() {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            startPosition = rectTransform.anchoredPosition;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            canvasGroup.alpha = 0.8f;
            canvasGroup.blocksRaycasts = false;
            offset = new Vector2(rectTransform.rect.width / 2, rectTransform.rect.height / 2);

        }

        public void OnDrag(PointerEventData eventData) {
            rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData) {
            rectTransform.anchoredPosition = startPosition - offset;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            gameObject.SetActive(false);

            Card.ApplyEffect();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            LeanTween.moveY(rectTransform, rectTransform.anchoredPosition.y + 20f, 0.2f).setEase(LeanTweenType.easeOutBack);
        }

        public void OnPointerExit(PointerEventData eventData) {
            LeanTween.moveY(rectTransform, rectTransform.anchoredPosition.y - 20f, 0.2f).setEase(LeanTweenType.easeInBack);
        }
    }
}