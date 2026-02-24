using UnityEngine;

public class RecipeButton : MonoBehaviour
{
    [SerializeField] private Recipe recipe;

    public void OnClickCraft()
    {
        Inventory.Singleton.Craft(recipe);
    }
}