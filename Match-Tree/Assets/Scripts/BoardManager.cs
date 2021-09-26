using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    #region Singleton
    private static BoardManager _instance = null;

    public static BoardManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<BoardManager>();
                if(_instance == null)
                {
                    Debug.LogError("Fatal Error: BoardManager not found");
                }
            }
            return _instance;
        }
    }
    #endregion

    [Header("Board")]
    public Vector2Int size;
    public Vector2 offsetTile;
    public Vector2 offsetBoard;

    [Header("Tile")]
    public List<Sprite> tileTypes = new List<Sprite>();
    public GameObject tilePrefab;

    public bool IsAnimating
    {
        get
        {
            return IsProcessing || IsSwapping;
        }
    }

    public bool IsProcessing { get; set; }
    public bool IsSwapping { get; set; }

    private Vector2 startPos;
    private Vector2 endPos;
    private TileController[,] tiles;
    private int combo;

    private void Start()
    {
        Vector2 tileSize = tilePrefab.GetComponent<SpriteRenderer>().size;
        CreateBoard(tileSize);
       // IsProcessing = false;
        //IsSwapping = false;
    }

    #region Generate
    private void CreateBoard(Vector2 tileSize)
    {
        tiles = new TileController[size.x, size.y];
        Vector2 totalSize = (tileSize + offsetTile) * (size - Vector2.one);
        startPos = (Vector2)transform.position - (totalSize / 2) + offsetBoard;
        endPos = startPos + totalSize;

        for(int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                TileController newTile = Instantiate(tilePrefab, new Vector2(startPos.x + ((tileSize.x + offsetTile.x) * x), startPos.y + ((tileSize.y + offsetTile.y) * y)), tilePrefab.transform.rotation, transform).GetComponent<TileController>();
                tiles[x, y] = newTile;

                List<int> possibleId = GetStartingPossibleIdList(x, y);
                int newId = possibleId[Random.Range(0, possibleId.Count)];

                newTile.ChangeId(newId, x, y);
            }
        }
    }

    private List<int> GetStartingPossibleIdList(int x, int y)
    {
        List<int> possibleId = new List<int>();
        for(int i = 0; i < tileTypes.Count; i++)
        {
            possibleId.Add(i);
        }
        if (x > 1 && tiles[x - 1, y].id == tiles[x - 2, y].id)
        {
            possibleId.Remove(tiles[x - 1, y].id);
        }

        if (y > 1 && tiles[x, y - 1].id == tiles[x, y - 2].id)
        {
            possibleId.Remove(tiles[x, y - 1].id);
        }

        return possibleId;
    }
    #endregion
}
