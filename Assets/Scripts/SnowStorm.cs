using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowStorm : MonoBehaviour
{
    public GameObject[] snowStorms;
    private int stormLevel;
    private float stormTimer;
    public float stormTimer_Editor;
    public AudioSource stormSound1, stormSound2;
    // Start is called before the first frame update
    void Start()
    {
        stormLevel = 0;
        snowStorms[0].SetActive(true);
        stormTimer = stormTimer_Editor;
    }

    // Update is called once per frame
    void Update()
    {
        StormLogic();
    }

    private void StormLogic()
	{
        stormTimer -= Time.deltaTime;
        if(stormTimer < 0)
		{
            stormLevel++;
            if (stormLevel > 2) stormLevel = 0;

            if (stormLevel == 0)
			{
                foreach(GameObject go in snowStorms) go.SetActive(false);
                if(snowStorms[0].active == false) snowStorms[0].SetActive(true);
                stormSound2.Stop();
            }
            else if(stormLevel == 1)
			{
                foreach (GameObject go in snowStorms) go.SetActive(false);
                stormSound1.Play();
                snowStorms[1].SetActive(true);
            }
            else if(stormLevel == 2)
			{
                foreach (GameObject go in snowStorms) go.SetActive(false);
                snowStorms[2].SetActive(true);
                stormSound1.Stop();
                stormSound2.Play();
            }
            stormTimer = stormTimer_Editor;
        }
	}
}
