using UnityEngine;

public class ClearPlate : MonoBehaviour
{
    public void ClearFood()
    {
        GameObject[] foods = GameObject.FindGameObjectsWithTag("Food");

        foreach (GameObject f in foods)
        {
            foreach (var comp in f.GetComponents<MonoBehaviour>())
            {
                comp.enabled = false;
            }

            Destroy(f); 
        }
    }


}
