using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class CheckWinCondition : MonoBehaviour
{

    public Tilemap gamePlayTileMap;
    public Tilemap targetTileMap;

    public Tile boxPlaceTile;
    public Tile boxTile;

    List<Vector3Int> boxPlaces = new List<Vector3Int>();

    // Use this for initialization
    void Start()
    {
        if(PlayerPrefs.GetInt(SceneManager.GetActiveScene().buildIndex.ToString()) != 1) PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex.ToString(), 0);
        BoundsInt bounds = targetTileMap.cellBounds;

        int i, k;

        for (i = bounds.xMin; i <= bounds.xMax; i++)
        {
            for (k = bounds.yMin; k <= bounds.yMax; k++)
            {
                Vector3Int pos = new Vector3Int(i, k, 0);
                Tile tile = targetTileMap.GetTile<Tile>(pos);
                if (tile == boxPlaceTile) boxPlaces.Add(pos);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int cnt = 0;
        foreach (Vector3Int item in boxPlaces)
        {
            if (boxTile == gamePlayTileMap.GetTile<Tile>(item)) cnt++;
        }
        if (cnt == boxPlaces.Count)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex.ToString(), 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


    }
}
