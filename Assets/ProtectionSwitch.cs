using UnityEngine;

public class ProtectionSwitch : MonoBehaviour
{
    [SerializeField] GameObject protectionWalls;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            protectionWalls.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
