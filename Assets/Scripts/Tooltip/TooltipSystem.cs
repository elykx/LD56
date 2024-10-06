using UnityEditor;
using UnityEngine;

public class TooltipSystem : MonoBehaviour {
    private static TooltipSystem tooltipSystem;

    public Tooltip tooltip;

    private void Awake() {
        tooltipSystem = this;
    }

    public static void Show(string content, string header = "") {
        tooltipSystem.tooltip.SetText(content, header);
        tooltipSystem.tooltip.gameObject.SetActive(true);
    }

    public static void Hide() {
        tooltipSystem.tooltip.gameObject.SetActive(false);
    }
}