using UnityEngine;

[CreateAssetMenu(menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    [System.Serializable]
    public class Ingredient
    {
        public Item item;
        public int amount;
    }

    public Ingredient[] ingredients;
    public Item result;
}