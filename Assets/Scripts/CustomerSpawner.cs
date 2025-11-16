using System.Collections;
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
        customers.Add(newCustomer);

        Vector2 targetPos = new Vector2(startX + customerCount * customerSpacing, rt.anchoredPosition.y);
        Vector2 startPos = targetPos + new Vector2(150f, 0);

        rt.anchoredPosition = startPos;

        StartCoroutine(SlideAndFadeIn(rt, targetPos));

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

    public IEnumerator SlideAndFadeIn(RectTransform rt, Vector2 targetPos, float duration = 0.5f)
    {
        CanvasGroup cg = rt.GetComponent<CanvasGroup>();

        Vector2 startPos = rt.anchoredPosition;
        float elapsed = 0f;
        cg.alpha = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            rt.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            cg.alpha = t;

            yield return null;
        }

        rt.anchoredPosition = targetPos;
        cg.alpha = 1f;
    }

}


