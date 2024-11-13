using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{//In dit script worden de prefabs bewaard die in een kamer kunnen verschijnen 
    //Het zit in een los script omdat ik per kamer een andere lijst objecten wil hebben.
    [SerializeField] 
    private GameObject[] possibleObjects;
    public GameObject[] PossibleObjects { get { return possibleObjects; } private set { possibleObjects = value; } }

    [SerializeField]
    private GameObject[] alwaysObjects;
    public GameObject[] AlwaysObjects { get { return alwaysObjects; } private set { alwaysObjects = value; } }

}
