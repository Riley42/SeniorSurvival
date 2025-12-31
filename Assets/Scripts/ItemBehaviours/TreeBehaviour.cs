using UnityEngine;
using UnityEngine.Rendering;

public class TreeBehaviour : MonoBehaviour, IHarvestable
{
    [SerializeField] TreeSO treeData;
    float currentHealth;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        currentHealth = treeData.MaxHealth;
    }

    public void HarvestResource(HarvestItemSO toolUsed)
    {
        bool isOptimalTool = toolUsed.ToolType == treeData.OptimalTool;

        //Calculate damage based on tool used
        float damage = isOptimalTool ? toolUsed.OptimalDamage : toolUsed.BaseDamage;
        currentHealth = Mathf.Max(currentHealth - damage, 0f);

        // Calcualate yield based on tool used
        int yield = isOptimalTool ? treeData.OptimalYieldPerHit : treeData.BaseYieldPerHit;

        // Add resources to inventory
        if (treeData.ResourceDrop != null)
            InventoryManager.Instance.AddItems(treeData.ResourceDrop, yield);
        


        if (currentHealth <= 0f)
        {
            DestroyObject();
        }
        else
        {
            string message = $"{treeData.DisplayName}\n{currentHealth}/{treeData.MaxHealth}";
            print(message);
        }
    }

    void DestroyObject()
    {
        Collider[] collider = GetComponents<Collider>();
        if (collider != null)
        {
            foreach (var col in collider)
            {
                col.enabled = false;
            }
        }

        if (anim != null)
        {
            anim.SetTrigger("Fall");
        }

        Destroy(gameObject, 2f);
    }

}
