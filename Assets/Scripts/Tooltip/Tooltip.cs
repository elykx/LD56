using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class Tooltip : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _headerField;
    [SerializeField] private TextMeshProUGUI _contentField;
    [SerializeField] private LayoutElement _layoutElement;
    [SerializeField] private RectTransform _rectTransform;

    private void Update() {
        var position = Input.mousePosition;
        var normalizedPosition = new Vector2(position.x / Screen.width, position.y / Screen.height);
        var pivot = CalculatePivot(normalizedPosition);
        _rectTransform.pivot = pivot;
        transform.position = position;
    }
    public void SetText(string content, string header = "") {
        if (string.IsNullOrEmpty(header)) {
            _headerField.gameObject.SetActive(false);
        }
        else {
            _headerField.gameObject.SetActive(true);
            _headerField.text = header;
        }
        _contentField.text = content;

        _layoutElement.enabled = Math.Max(_headerField.preferredWidth, _contentField.preferredWidth) >= _layoutElement.preferredWidth;
    }

    private Vector2 CalculatePivot(Vector2 normalizedPosition) {
        var pivotTopLeft = new Vector2(-0.05f, 1.05f);
        var pivotTopRight = new Vector2(1.05f, 1.05f);
        var pivotBottomLeft = new Vector2(-0.05f, -0.05f);
        var pivotBottomRight = new Vector2(1.05f, -0.05f);

        if (normalizedPosition.x < 0.5f && normalizedPosition.y >= 0.5f) {
            return pivotTopLeft;
        }
        else if (normalizedPosition.x > 0.5f && normalizedPosition.y >= 0.5f) {
            return pivotTopRight;
        }
        else if (normalizedPosition.x <= 0.5f && normalizedPosition.y < 0.5f) {
            return pivotBottomLeft;
        }
        else {
            return pivotBottomRight;
        }
    }
}