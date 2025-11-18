using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SubmitPlate : MonoBehaviour
{
    public TextMeshProUGUI moneyText; 
    public int totalMoney = 1;
    public ClearPlate clearPlate;
    public CustomerSpawner customerSpawner;
    
    void Start()
    {
        UpdateMoneyUI();
    }

    public void Submit()
    {
        if (customerSpawner.customerCount <= 0)
        {
            Debug.Log("No customers to serve!");
            return;
        }

        GameObject[] foodsOnPlate = GameObject.FindGameObjectsWithTag("Food");
        
        GameObject firstCustomer = customerSpawner.customers[0];
        Customer customerScript = firstCustomer.GetComponent<Customer>();
        List<Order> requiredOrder = customerScript.getOrder();

        List<Order> plateFood = new List<Order>();
        foreach (GameObject food in foodsOnPlate)
        {
            FoodPrice foodPrice = food.GetComponent<FoodPrice>();
            if (foodPrice != null)
            {
                plateFood.Add(foodPrice.foodOrder);
            }
        }

        bool orderMatches = CheckOrderMatch(requiredOrder, plateFood);

        if (orderMatches)
        {
            int plateTotal = 0;
            foreach (GameObject food in foodsOnPlate)
            {
                FoodPrice priceComponent = food.GetComponent<FoodPrice>();
                if (priceComponent != null)
                {
                    plateTotal += priceComponent.Price;
                }
            }
            
            totalMoney += plateTotal;
        }
        else
        {
            totalMoney -= 10;
        }

        UpdateMoneyUI();
        clearPlate.ClearFood();
        customerSpawner.DecrementCustomer();
        CheckLevelProgress();
    }

    private void CheckLevelProgress()
    {
        if (totalMoney < 0)
        {
            SceneManager.LoadScene(4);
            return;
        }

        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Difficulty 1" && totalMoney >= 10)
        {
            totalMoney = 0;
            SceneManager.LoadScene(2);
        }
        else if (currentScene == "Difficulty 2" && totalMoney >= 50)
        {
            totalMoney = 0;
            SceneManager.LoadScene(3);
        }
        else if (currentScene == "Level3" && totalMoney >= 100)
        {
            SceneManager.LoadScene(4);
        }
    }

    private bool CheckOrderMatch(List<Order> required, List<Order> provided)
    {
        if (required.Count != provided.Count)
            return false;

        List<Order> sortedRequired = new List<Order>(required);
        List<Order> sortedProvided = new List<Order>(provided);
        
        sortedRequired.Sort();
        sortedProvided.Sort();

        for (int i = 0; i < sortedRequired.Count; i++)
        {
            if (sortedRequired[i] != sortedProvided[i])
                return false;
        }

        return true;
    }

    void UpdateMoneyUI()
    {
        moneyText.text = "$ " + totalMoney;
    }
}