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

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnCustomer();
            timer = 0f;
        }
    }

    private void SpawnCustomer()
    {
        GameObject newCustomer = Instantiate(customerPrefab, canvas);
        RectTransform rt = newCustomer.GetComponent<RectTransform>();
        customerCount++;

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
}

