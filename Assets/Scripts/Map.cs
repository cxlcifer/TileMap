using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Vector2Int size;
    public Vector2Int Size => size;

    private Tile[,] _tiles;

    private void Awake()
    {
        _tiles = new Tile[Size.x, Size.y];
    }

    public void SetTile(Vector2Int index, Tile tile)
    {
        _tiles[index.x, index.y] = tile;
    }

    public bool IsCellAvailable(Vector2Int index)
    {
        var isAvailable = index.x <= 0 || index.y <= 0 || index.x > _tiles.GetLength(0) ||
                          index.y > _tiles.GetLength(1);


        if (isAvailable)
        {
            return false;
        }

        var isFree = _tiles[index.x, index.y] == null;
        return isFree;
    }
}
