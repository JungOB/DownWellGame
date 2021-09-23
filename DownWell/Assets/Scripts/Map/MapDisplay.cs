using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    MapManager mapManager;

    public Vector3 offset;
    public GameObject[] tileObject;

    private void Start()
    {
        mapManager = MapManager.instance;
    }

    public void Display(Tile[,] generatedTiles)
    {
        for (int y = 0; y < mapManager.height; y++)
        {
            for (int x = 0; x < mapManager.width; x++)
            {
                Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x
                                                    , -y + offset.y);

                if (generatedTiles[x, y].style == TileStyle.Wall)
                    Instantiate(tileObject[0], tilePosition, Quaternion.identity);
                else if (generatedTiles[x, y].style == TileStyle.Block)
                    Instantiate(tileObject[1], tilePosition, Quaternion.identity);
                else if (generatedTiles[x, y].style == TileStyle.Enemy)
                    Instantiate(tileObject[2], tilePosition, Quaternion.identity);
            }
        }
    }

    public void Display(int[,] generatedLevel)
    {
        for (int y = 0; y < mapManager.height; y++)
        {
            for (int x = 0; x < mapManager.width; x++)
            {
                Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x
                                                    , -y + offset.y);

                if (generatedLevel[x, y] == 1)
                    Instantiate(tileObject[0], tilePosition, Quaternion.identity);
                else if (generatedLevel[x, y] == 2)
                    Instantiate(tileObject[1], tilePosition, Quaternion.identity);
                else if (generatedLevel[x, y] == 3)
                    Instantiate(tileObject[2], tilePosition, Quaternion.identity);
            }
        }
    }
}
