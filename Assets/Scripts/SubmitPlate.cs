using UnityEngine;
using TMPro;

public class SubmitPlate : MonoBehaviour
{
    public TextMeshProUGUI moneyText; 
    public int totalMoney = 1;
    public ClearPlate clearPlate; 
    void Start()
    {
        UpdateMoneyUI();
    }

    public void Submit()
    {
        GameObject[] foodsOnPlate = GameObject.FindGameObjectsWithTag("Food");

        int plateTotal = 0;

        foreach (GameObject food in foodsOnPlate)
        {
            FoodPrice priceComponent = food.GetComponent<FoodPrice>();
            if (priceComponent != null)
            {
                plateTotal += priceComponent.Price;
            }
        }
        Debug.Log("Plate total price = " + plateTotal);

        totalMoney += plateTotal;
        UpdateMoneyUI();
        clearPlate.ClearFood();
    }

    void UpdateMoneyUI()
    {
        moneyText.text = "Money: " + totalMoney;
    }
}
