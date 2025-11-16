using System.Collections.Generic;
using UnityEngine;
public enum Order
{
    Worm,
    Fish,
    Food3,
    Food4,
    Food5
}

[System.Serializable]
public class Customer : MonoBehaviour
{
    public List<Order> orders = new List<Order>();
    public const float basePatience = 10f;

    private void Awake()
    {
        //Add a random order to the list, 1 for now
        Order randomOrder = (Order)Random.Range(0, System.Enum.GetValues(typeof(Order)).Length);
        orders.Add(randomOrder);
    }
    public List<Order> getOrder() => orders;
    public float getPatience() => basePatience * (1 + (orders.Count - 1) * 0.5f);

}
