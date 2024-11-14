using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoom : MonoBehaviour
{
    public static bool theEnd;
    // Start is called before the first frame update
    void Start() => theEnd = false;

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Player")) theEnd = true;
	}
}
