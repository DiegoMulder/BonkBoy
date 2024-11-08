using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] 
    private GameObject[] possibleObjects;
    public GameObject[] PossibleObjects { get { return possibleObjects; } private set { possibleObjects = value; } }
}
