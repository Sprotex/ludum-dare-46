using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> tilePrefabs;
    public Transform folder;
    public Transform playerTransform;
    public int tileSize = 5;
    public Vector2Int halfMapSize;
    public Vector2 origin;

    private void Start()
    {
        var position = origin;
        for (var x = -halfMapSize.x; x <= halfMapSize.x; ++x) {
            for (var y = -halfMapSize.y; y <= halfMapSize.y; ++y)
            {
                var spawnPosition = new Vector3(position.x + x * tileSize, 0f, position.y + y * tileSize);
                var spawnRotation = Quaternion.Euler(0f, Random.Range(0, 4) * 90f, 0f);
                Instantiate(tilePrefabs[0], spawnPosition, spawnRotation, folder);
            }
        }
    }
}
