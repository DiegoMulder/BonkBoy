using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrabVisual : MonoBehaviour
{
    public GameObject handOpen;
    public GameObject handClosed;

    public void GrabObject()
    {
        handOpen.SetActive(false);
        handClosed.SetActive(true);
    }

    public void LetGoObject()
    {
        handOpen.SetActive(true);
        handClosed.SetActive(false);
    }
}
