using UnityEngine;

public class ClearPlate : MonoBehaviour
{
    public void ClearFood()
    {
        GameObject[] foods = GameObject.FindGameObjectsWithTag("Food");

        foreach (GameObject f in foods)
            Destroy(f);
    }

}
