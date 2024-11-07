using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Node _emptyTilePrefab;
    [SerializeField] int height = 9, width = 16;

    [SerializeField] GameObject[] RoomList; 

    [SerializeField] Transform Camera; // de transform van de camera

    public Node[,] GenerateGrid(int width, int height)
    {
       Node[,] nodeArray = new Node[width, height];
       for (int y = 0; y < height; y++) 
       {
            for (int x = 0; x < width; x++)
            {
                GameObject currentRoom;
                int[] Rotations = { 90,180,270};

                var Tile = Instantiate(_emptyTilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                nodeArray[x, y] = Tile;
                Tile.name = $"Tile {x} {y}"; //Dit is gewoon om de coords makkelijker te zien in de editor


                //Zorgt ervoor dat de kamers aan de rand van de map allemaal een doodlopende kamer worden
                if (x == this.width-1 || y == this.height-1 || y == 0 || x == 0)
                {
                    currentRoom = RoomList[2];
                    Tile.transform.Rotate(-90, 0, Rotations[Random.Range(0, Rotations.Length)]);
                }
                else
                {
                    currentRoom = RoomList[Random.Range(0, RoomList.Length - 1)];
                    Tile.transform.Rotate(-90,0,0);
                }
                if (y == this.height)
                {
                    Tile.transform.Rotate(-90, 0, Rotations[Random.Range(0, 1)]);
                }

                Tile.GetComponent<MeshFilter>().mesh = currentRoom.GetComponent<MeshFilter>().mesh;
                Tile.GetComponent<MeshRenderer>().materials = currentRoom.GetComponent<MeshRenderer>().materials;
                Tile.GetComponent<MeshCollider>().sharedMesh = currentRoom.GetComponent<MeshCollider>().sharedMesh;
                

                Tile.transform.localScale = Vector3.one * 50;

                //Maakt een checker patroon in de grid om de kamers net iets meer variatie te geven
                if (y % 2 == 0 && x % 2 == 0 || y % 2 != 0 && x % 2 != 0) //Als allebei de x en y even zijn of als allebei de x en y oneven zijn maak ze zwart
                {
                    Tile.GetComponent<MeshRenderer>().material.color = Color.black;
                }
                Tile.OriginalColor = Tile.GetComponent<MeshRenderer>().material.color;
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
