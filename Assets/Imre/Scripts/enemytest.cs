using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemytest : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("weapon"))
        {
            Debug.Log("Enemy hit");
        }
    }
}
