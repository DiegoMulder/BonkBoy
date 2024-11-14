using UnityEngine;

public class ProtectionSwitch : MonoBehaviour
{
    [SerializeField] GameObject protectionWalls;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            protectionWalls.SetActive(false);
            GameObject.Find("ProtectionWall").SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
