using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GridManager : MonoBehaviour
{
    [SerializeField] Node _emptyTilePrefab;
    [SerializeField] int height = 9, width = 16;

    [SerializeField] GameObject[] RoomList; 

    [SerializeField] Transform Camera; // de transform van de camera

    [SerializeField] GameObject startKamer;
    [SerializeField] GameObject eindKamer;

    GameObject[] objectsPossible;

    int eindkamerCount = 0;

    public Node[,] GenerateGrid(int width, int height)
    {
       Node[,] nodeArray = new Node[width, height];
       for (int y = 0; y < height; y++) 
       {
            for (int x = 0; x < width; x++)
            {
                GameObject currentRoom;
                int[] Rotations = { 90, 180, 270 };

                var Tile = Instantiate(_emptyTilePrefab, new Vector3(x*5, 0, y*5), Quaternion.identity);
                nodeArray[x, y] = Tile;
                Tile.name = $"Tile {x} {y}"; //Dit is om de coords makkelijker te zien in de editor


                //Zorgt ervoor dat de kamers aan de rand van de map allemaal een doodlopende kamer worden
                if (x == this.width - 1 || y == this.height - 1 || y == 0 || x == 0)
                {
                    currentRoom = RoomList[2];
                    Tile.transform.Rotate(-90, 0, Rotations[Random.Range(0, Rotations.Length)]);

                    if (Random.value < 0.2 && eindkamerCount == 0 || x == this.width -2 && y == this.height -2 && eindkamerCount == 0)
                    {//maakt een willekeurige kamer aan de rand van de map de eindkamer en als er geen willekuerige kamer is gekozen
                     // dan wordt de laatste kamer de eindkamer
                        currentRoom = eindKamer;
                        eindkamerCount++;
                    }
                }
                else
                { // maakt de rotatie van alle kamers die niet kamer 3 zijn een klein beetje meer willekuerig
                    currentRoom = RoomList[Random.Range(0, RoomList.Length - 1)];
                    Tile.transform.Rotate(-90,0,0);
                }
                if (y == this.height)
                {
                    Tile.transform.Rotate(-90, 0, Rotations[Random.Range(0, 1)]);
                }

                if (x == 8 && y == 4)
                { 
                    currentRoom = startKamer;
                }


                Tile.GetComponent<MeshFilter>().mesh = currentRoom.GetComponent<MeshFilter>().mesh;
                Tile.GetComponent<MeshRenderer>().materials = currentRoom.GetComponent<MeshRenderer>().materials;
                Tile.GetComponent<MeshCollider>().sharedMesh = currentRoom.GetComponent<MeshCollider>().sharedMesh;
                
                
                Tile.transform.localScale = Vector3.one * 250;


                //Maakt een checker patroon in de grid om de kamers net iets meer variatie te geven
                if (y % 2 == 0 && x % 2 == 0 || y % 2 != 0 && x % 2 != 0) //Als allebei de x en y even zijn of als allebei de x en y oneven zijn maak ze zwart
                {
                    if (currentRoom != eindKamer)
                    { 
                        Tile.GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                }
                Tile.OriginalColor = Tile.GetComponent<MeshRenderer>().material.color;


                objectsPossible = currentRoom.GetComponent<ObjectGenerator>().PossibleObjects;
                foreach (GameObject possibleObject in objectsPossible)
                {
                    if (Random.value < 0.2)
                    {
                        Instantiate(possibleObject, Tile.transform, false);
                    }
                }

                if (currentRoom == startKamer)
                {
                    foreach (GameObject possibleObject in objectsPossible)
                    { 
                        Instantiate(possibleObject, Tile.transform, false);
                    }
                }

                foreach (GameObject alwaysObject in currentRoom.GetComponent<ObjectGenerator>().AlwaysObjects)
                { 
                        Instantiate(alwaysObject, Tile.transform, false);

                }

                // Tile.GetComponent<NavMeshSurface>().BuildNavMesh();
            }
       }
       Camera.transform.position = new Vector3((float)width / 2 -0.5f, 10, (float)height / 2 - 0.5f);

       return nodeArray;
    }

    private void Start()
    {
        GenerateGrid(width, height);
    }
}
