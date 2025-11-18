using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public enum Order
{
    Food1,
    Food2,
    Food3,
    Food4,
    Food5,
    Food6
}

[System.Serializable]
public class Customer : MonoBehaviour
{
    public List<Order> orders = new List<Order>();
    public List<RectTransform> orderImages; //3 pre-set icons to match food sprites to
    private Dictionary<Order, Sprite> spriteMap;

    public Image patienceBar;

    //Change as foods are added
    [SerializeField] private Sprite food1Sprite;
    [SerializeField] private Sprite food2Sprite; 
    [SerializeField] private Sprite food3Sprite;
    [SerializeField] private Sprite food4Sprite;
    [SerializeField] private Sprite food5Sprite;
    [SerializeField] private Sprite food6Sprite;

    public const float basePatience = 10f;
    private float currentPatience;
    private bool served = false;
    public List<Order> getOrder() => orders;
    public float getPatience() => basePatience * (1 + (orders.Count - 1) * 0.5f);
    private void Awake()
    {
        spriteMap = new Dictionary<Order, Sprite>
        {
            { Order.Food1, food1Sprite },
            { Order.Food2, food2Sprite },
            { Order.Food3, food3Sprite },
            { Order.Food4, food4Sprite },
            { Order.Food5, food5Sprite },
            { Order.Food6, food6Sprite }
        };

        //Generate 1-3 items for customer order
        int numOrders = Random.Range(1, 4);
        for (int i = 0; i < numOrders; i++)
        {
            Order randomOrder = (Order)Random.Range(0, System.Enum.GetValues(typeof(Order)).Length);
            orders.Add(randomOrder);
        }
        LayoutOrder();

        currentPatience = getPatience();
        UpdatePatienceBar();
    }

    private void Update()
    {
        if (served) return;
        currentPatience -= Time.deltaTime;

        UpdatePatienceBar();

        if (currentPatience <= 0f)
        {
            CustomerSpawner.Instance.RemoveCustomer(this);
            Debug.Log("Customer " + gameObject.name + "left angry!");
            served = true;
        }
    }

    private void UpdatePatienceBar()
    {
        float percentage = Mathf.Clamp01(currentPatience / getPatience());
        patienceBar.fillAmount = percentage;

        //Color transition
        if (percentage > 0.5f)
            patienceBar.color = Color.Lerp(Color.yellow, Color.green, percentage);
        else
            patienceBar.color = Color.Lerp(Color.red, Color.yellow, percentage);
    }

    public void LayoutOrder()
    {
        foreach (var icon in orderImages)
            icon.gameObject.SetActive(false);

        for (int i = 0; i < orders.Count; i++)
        {
            Image img = orderImages[i].GetComponent<Image>();
            img.sprite = spriteMap[orders[i]];
            img.color = Color.white;

            if (orders[i] == Order.Food1) //Worm
                img.rectTransform.sizeDelta = new Vector2(60, 60);
        }

        if (orders.Count == 1)
        {
            orderImages[0].anchoredPosition = Vector2.zero;
            orderImages[0].localScale = Vector3.one * 2f;
            orderImages[0].gameObject.SetActive(true);
        }
        else if (orders.Count == 2)
        {
            orderImages[0].anchoredPosition = new Vector2(-25, 0);
            orderImages[1].anchoredPosition = new Vector2(25, 0);

            orderImages[0].localScale = Vector3.one * 1.3f;
            orderImages[1].localScale = Vector3.one * 1.3f;

            orderImages[0].gameObject.SetActive(true);
            orderImages[1].gameObject.SetActive(true);
        }
        else if (orders.Count == 3)
        {
            orderImages[0].localScale = Vector3.one * 1.3f;
            orderImages[1].localScale = Vector3.one * 1.3f;
            orderImages[2].localScale = Vector3.one * 1.3f;

            orderImages[0].gameObject.SetActive(true);
            orderImages[1].gameObject.SetActive(true);
            orderImages[2].gameObject.SetActive(true);
        }
    }


}
