using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DragGhost : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI quantityText;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        if (canvasGroup.alpha > 0)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void Show(Sprite icon, int quantity = 0)
    {
        iconImage.sprite = icon;
        iconImage.enabled = true;

        if (quantity > 1)
        {
            quantityText.transform.parent.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }
        else
        {
            quantityText.transform.parent.gameObject.SetActive(false);
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = false;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }
}
