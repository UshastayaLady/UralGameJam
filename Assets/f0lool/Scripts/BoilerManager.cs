using System.Collections.Generic;
using UnityEngine;

public class BoilerManager : MonoBehaviour
{
    public static BoilerManager Instance;

    [SerializeField] private List<GameObject> _ingredients;
    [SerializeField] private GameObject _newItem;

    private Stack<string> _ingredientsStack = new Stack<string>();

    private Collider2D _collider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(Instance);
            Instance = this;
        }
    }

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();

        _ingredientsStack.Push("Ingredient7");
        _ingredientsStack.Push("Ingredient6");
        _ingredientsStack.Push("Ingredient5");
        _ingredientsStack.Push("Ingredient4");
        _ingredientsStack.Push("Ingredient3");
        _ingredientsStack.Push("Ingredient2");
        _ingredientsStack.Push("Ingredient1");
        _newItem.SetActive(false);
    }

    public bool IsCollideBoiler(Bounds bounds)
    {
        var boilerBounds = _collider.bounds;

        return bounds.Intersects(boilerBounds);
    }

    public bool IsCorrectIngredient(GameObject ingredient)
    {
        if(ingredient.name == _ingredientsStack.Peek())
        {
            _ingredientsStack.Pop();
            CheckComplete();
            return true;
        }

        return false;
    }

    public void RestartMiniGame()
    {
        _ingredientsStack.Push("Ingredient7");
        _ingredientsStack.Push("Ingredient6");
        _ingredientsStack.Push("Ingredient5");
        _ingredientsStack.Push("Ingredient4");
        _ingredientsStack.Push("Ingredient3");
        _ingredientsStack.Push("Ingredient2");
        _ingredientsStack.Push("Ingredient1");

        foreach(var ingredient in _ingredients)
        {
            ingredient.SetActive(true);
        }
    }

    private void CheckComplete()
    {
        if(_ingredientsStack.Count == 0)
        {
            _newItem.SetActive(true);
        }
    }
}
