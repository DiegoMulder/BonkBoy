using UnityEngine;

public class GridManager : MonoBehaviour
{
   [SerializeField] Node _emptyTilePrefab;
   [SerializeField] int height , width = 16;

   [SerializeField] Transform main;

    public void GenerateGrid()
    { 
       for (int x = 0; x < height; x++) 
       {
            for (int y = 0; y < width; y++)
            {
                var Tile = Instantiate(_emptyTilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                if (x % 2 == 0 && y % 2 == 0 || x % 2 != 0 && y % 2 != 0) //Als allebei de x en y even zijn of als allebei de x en y oneven zijn maak ze zwart
                {
                    Tile.GetComponent<MeshRenderer>().material.color = Color.black;
                }
                Tile.OriginalColor = Tile.GetComponent<MeshRenderer>().material.color;
                Tile.name = $"Tile {x} {y}";
            }
       }
       main.transform.position = new Vector3((float)height / 2 -0.5f, 10, (float)width / 2 - 0.5f);
    }

    private void Start()
    {
        GenerateGrid();
    }
}
