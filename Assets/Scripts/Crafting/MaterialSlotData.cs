using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialSlotData : MonoBehaviour
{
    [SerializeField] Image materialImage;
    [SerializeField] TextMeshProUGUI materialName;
    [SerializeField] TextMeshProUGUI materialAmount;

    public void SetContentData(RecipeItem material, int multiplier = 1)
    {
        print("Setting content");
        var currentAmt = InventoryManager.Instance.GetTotalInventoryCount(material.Item);

        materialImage.sprite = material.Item.Icon;
        materialName.text = material.Item.DisplayName;
        materialAmount.text = $"{currentAmt} / {material.AmountRequiredToCraft * multiplier}";
    }


}
