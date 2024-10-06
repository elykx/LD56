using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Header;
    public string Content;

    private static LTDescr delay;

    public void OnPointerEnter(PointerEventData eventData) {
        delay = LeanTween.delayedCall(0.5f, () =>
        {
            TooltipSystem.Show(Content, Header);
        });
    }

    public void OnPointerExit(PointerEventData eventData) {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }
}