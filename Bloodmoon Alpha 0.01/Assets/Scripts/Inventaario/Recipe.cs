using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Recipe")]
public class Recipe : ScriptableObject
{
    [System.Serializable]
    public class Ingredient
    {
        public Item item;
        public int amount;
    }

    public Ingredient[] ingredients;

    [Header("Result")]
    public Item result;
    public int resultAmount = 1; 
}