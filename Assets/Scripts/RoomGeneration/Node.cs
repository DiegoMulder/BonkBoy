using UnityEngine;

public class Node : MonoBehaviour
{
    static int ClickCount;

    MeshRenderer meshRenderer;
    public Color OriginalColor;
    private void Start()
    {
        ClickCount = 0;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void OnMouseEnter()
    {
        meshRenderer.material.color = Color.white;
    }

    private void OnMouseExit() 
    {
        meshRenderer.material.color = OriginalColor;
    }

    private void OnMouseDown()
    {
        if (ClickCount == 0)
        {
            OriginalColor = Color.yellow;
            meshRenderer.material.color = Color.yellow;
           
            ClickCount++;
        }
        else if (ClickCount == 1)
        { 
            OriginalColor = Color.red;
            meshRenderer.material.color = Color.red;
            ClickCount++;
        }
    }
}
