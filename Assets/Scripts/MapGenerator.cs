using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapGenerator : MonoBehaviour
{
    private class Availables
    {
        public bool up;
        public bool down;
        public bool left;
        public bool right;
        public int FreeSpaceCount()
        {
            int result = 0;
            if (up)
            {
                ++result;
            }
            if (down)
            {
                ++result;
            }
            if (left)
            {
                ++result;
            }
            if (right)
            {
                ++result;
            }
            return result;
        }
    }

    public GameObject[] iTiles;
    public GameObject[] lTiles;
    public GameObject[] tTiles;
    public GameObject[] xTiles;
    public GameObject[] oTiles;
    public GameObject[] endTiles;
    public Transform folder;
    public int tileSize = 5;
    public Vector2Int halfMapSize;
    public Vector2 origin;
    private Availables[,] availables;
    private int xSize = 0;
    private int ySize = 0;
    private Availables[,] GenerateLayout()
    {
        var result = new Availables[xSize, ySize];
        var x = 0f;
        var y = 0f;
        var aX = 0;
        var aY = 0;
        var open = new Stack<Vector2Int>();
        var closed = new Dictionary<int, Vector2Int>();
        var prev = new Dictionary<Vector2Int, Vector2Int>();
        for (aX = 0; aX < xSize; ++aX)
        {
            for (aY = 0; aY < ySize; ++aY)
            {
                result[aX, aY] = new Availables { up = true, down = true, left = true, right = true };
            }
        }
        x = origin.x;
        y = origin.y;
        var maxAX = halfMapSize.x * 2 + 1;
        var maxAY = halfMapSize.y * 2 + 1;
        aX = (int)x + halfMapSize.x + 1;
        aY = (int)y + halfMapSize.y + 1;
        result[aX, aY].up = false;
        result[aX, aY].down = false;
        result[aX, aY].left = false;
        result[aX, aY].right = false;
        result[aX - 1, aY].right = false;
        result[aX + 1, aY].left = false;
        result[aX, aY - 1].up = false;
        result[aX, aY + 1].down = false;
        open.Push(new Vector2Int { x = aX + 1, y = aY });
        open.Push(new Vector2Int { x = aX - 1, y = aY });
        open.Push(new Vector2Int { x = aX, y = aY + 1});
        open.Push(new Vector2Int { x = aX, y = aY - 1});
        var key = aX * 100000 + aY;
        closed.Add(key, new Vector2Int { x = aX, y = aY });
        while (open.Count > 0)
        {
            var currentPos = open.Pop();
            aX = currentPos.x;
            aY = currentPos.y;
            key = aX * 100000 + aY;
            if (!closed.ContainsKey(key) && aX > 1 && aY > 1 && aX < maxAX && aY < maxAY)
            {
                closed.Add(key, currentPos);
                var s1 = new Vector2Int { x = aX + 1, y = aY };
                var s2 = new Vector2Int { x = aX - 1, y = aY };
                var s3 = new Vector2Int { x = aX, y = aY + 1};
                var s4 = new Vector2Int { x = aX, y = aY - 1};
                open.Push(s1);
                open.Push(s2);
                open.Push(s3);
                open.Push(s4);
                if (!prev.ContainsKey(s1)) prev.Add(s1, currentPos);
                if (!prev.ContainsKey(s2)) prev.Add(s2, currentPos);
                if (!prev.ContainsKey(s3)) prev.Add(s3, currentPos);
                if (!prev.ContainsKey(s4)) prev.Add(s4, currentPos);
            }
        }
        foreach(var item in prev)
        {
            var from = item.Value;
            var to = item.Key;
            var diff = to - from;
            aX = to.x;
            aY = to.y;
            var aDX = diff.x;
            var aDY = diff.y;
            if (aDX == 1)
            {
                result[aX, aY].left = false;
                result[aX - 1, aY].right = false;
            } else if (aDX == -1)
            {
                result[aX, aY].right = false;
                result[aX + 1, aY].left = false;
            } else if (aDY == 1)
            {
                result[aX, aY].down = false;
                result[aX, aY - 1].up = false;
            } else if (aDY == -1)
            {
                result[aX, aY].up = false;
                result[aX, aY + 1].down = false;
            }
        }
        return result;
    }
    private void Start()
    {
        var position = origin;
        xSize = halfMapSize.x * 2 + 3;
        ySize = halfMapSize.y * 2 + 3;
        var a = availables = GenerateLayout();
        // NOTE(Andy): Set up tiles.
        for (var x = -halfMapSize.x; x <= halfMapSize.x; ++x) {
            for (var y = -halfMapSize.y; y <= halfMapSize.y; ++y)
            {
                var aX = x + halfMapSize.x + 1;
                var aY = y + halfMapSize.y + 1;
                var avl = a[aX, aY];
                var freeSpaceCount = avl.FreeSpaceCount();
                var yRotation = 0f;
                GameObject[] selectedTiles;
                switch(freeSpaceCount)
                {
                    case 0:
                        selectedTiles = xTiles;
                        yRotation = Random.Range(0, 4) * 90f;
                        break;
                    case 1:
                        selectedTiles = tTiles;
                        if (avl.right)
                        {
                            yRotation = 270f;
                        } else if (avl.up)
                        {
                            yRotation = 180f;
                        } else if (avl.left)
                        {
                            yRotation = 90f;
                        }
                        break;
                    case 2:
                        if (avl.up && avl.down)
                        {
                            selectedTiles = iTiles;
                            yRotation = Random.Range(0, 2) * 180f + 90f;
                            break;
                        } else if (avl.left && avl.right)
                        {
                            selectedTiles = iTiles;
                            yRotation = Random.Range(0, 2) * 180f;
                            break;
                        }
                        selectedTiles = lTiles;
                        if (avl.up && avl.right)
                        {
                            yRotation = 180f;
                        } else if (avl.right && avl.down)
                        {
                            yRotation = 270f;
                        } else if (avl.down && avl.left)
                        {
                            yRotation = 0f;
                        } else
                        {
                            yRotation = 90f;
                        }
                        break;
                    case 3:
                        selectedTiles = endTiles;
                        if (!avl.up)
                        {
                            yRotation = 180f;
                        } else if (!avl.left)
                        {
                            yRotation = 90f;
                        } else if (!avl.right)
                        {
                            yRotation = 270f;
                        } else if (!avl.down)
                        {
                            yRotation = 0f;
                        }
                        break;
                    default:
                        selectedTiles = oTiles;
                        yRotation = Random.Range(0, 4) * 90f;
                        break;
                }
                var spawnRotation = Quaternion.Euler(0f, yRotation, 0f);
                var spawnPosition = new Vector3(position.x + x * tileSize, 0f, position.y + y * tileSize);
                var tileIndex = Random.Range(0, selectedTiles.Length);
                var tilePrefab = selectedTiles[tileIndex];
                Instantiate(tilePrefab, spawnPosition, spawnRotation, folder);
            }
        }
    }
}
