using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject[] possibleItems;
    void Start()
    {
        foreach (GameObject possibleItem in possibleItems)
        {
            if (Random.value < 0.2)
            { 
               possibleItem.SetActive(true);
            }
        }
    }
}
