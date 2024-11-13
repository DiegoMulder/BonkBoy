using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject[] possibleItems;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

        foreach (GameObject possibleItem in possibleItems)
        {
            if (Random.value < 0.2)
            { 
               possibleItem.SetActive(true);
            }
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Player")) animator.SetTrigger("OpenChest");
	}
}
