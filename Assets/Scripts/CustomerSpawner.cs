using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform canvas;
    public float spawnInterval = 5f;

    private int customerCount = 0;
    private float timer = 0f;
    private float startX = 0f;
    private float customerSpacing = 150f;
    private List<GameObject> customers = new List<GameObject>();

    /*void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnCustomer();
            timer = 0f;
        }
    }*/

    private void SpawnCustomer()
    {
        GameObject newCustomer = Instantiate(customerPrefab, canvas);
        RectTransform rt = newCustomer.GetComponent<RectTransform>();
        customerCount++;
        customers.Add(newCustomer);

        //Spawn customers in a line
        if (rt != null)
            rt.anchoredPosition = new Vector2(startX + customerCount * customerSpacing, rt.anchoredPosition.y);

        Customer customerScript = newCustomer.GetComponent<Customer>();
        if (customerScript != null)
        {
            List<Order> orders = customerScript.getOrder();
            TextMeshProUGUI orderText = newCustomer.GetComponentInChildren<TextMeshProUGUI>();

            if (orderText != null)
                orderText.text = string.Join(", ", orders);

            Debug.Log("Spawned customer with order: " + string.Join(", ", orders));
        }

    }
    public void DecrementCustomer()
    {
        if (customerCount <= 0)
        {
            Debug.Log("No customers to delete.");
            return;
        }
            
        customerCount--;
        GameObject servedCustomer = customers[0];
        customers.Remove(servedCustomer);
        List<Order> order = servedCustomer.GetComponent<Customer>().getOrder();
        Debug.Log("Served customer with order " + string.Join(", ", order));
        Destroy(servedCustomer);
        
        foreach (GameObject customer in customers)
        {
            RectTransform rt = customer.GetComponent<RectTransform>();
            if (rt != null)
                rt.anchoredPosition = new Vector2(rt.anchoredPosition.x - customerSpacing, rt.anchoredPosition.y);
        }
    }
}


