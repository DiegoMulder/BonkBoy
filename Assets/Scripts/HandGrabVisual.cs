using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrabVisual : MonoBehaviour
{
    public GameObject handOpenLeft;
    public GameObject handClosedLeft;
    public GameObject handOpenRight;
    public GameObject handClosedRight;

    public void GrabObjectLeft()
    {
        handOpenLeft.SetActive(false);
        handClosedLeft.SetActive(true);
    }

    public void LetGoObjectLeft()
    {
        handOpenLeft.SetActive(true);
        handClosedLeft.SetActive(false);
    }

    public void GrabObjectRight()
    {
        handOpenRight.SetActive(false);
        handClosedRight.SetActive(true);
    }

    public void LetGoObjectRight()
    {
        handOpenRight.SetActive(true);
        handClosedRight.SetActive(false);
    }
}
