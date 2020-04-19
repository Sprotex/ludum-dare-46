using System.Collections;
using UnityEngine;

public class WormSpawner : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public GameObject wormPrefab;
    public float delayBetweenSpawns = 5f; // TODO(Andy): Optimize before build! This is good for testing only!
    public float playerDistance = 20f;
    public Transform playerTransform;
    public Transform wormFolder;

    private IEnumerator spawningCoroutine;

    private void Start()
    {
        StartSpawning();
    }

    private IEnumerator SpawningCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayBetweenSpawns);
            var tileSize = mapGenerator.tileSize;
            var halfMapSize = mapGenerator.halfMapSize;
            var mapPosition = mapGenerator.origin + new Vector2(tileSize * halfMapSize.x * (.5f - Random.value), tileSize * halfMapSize.y * (.5f - Random.value)) * 2;
            var spawnPosition = new Vector3(mapPosition.x, 0f, mapPosition.y);
            for (var i = 0; i < 5; ++i) {
                if (Physics.Raycast(spawnPosition + Vector3.up * 20f, Vector3.down, out RaycastHit hit, 100f, -1)) {
                    spawnPosition.y = hit.point.y;
                    break;
                } 
            }
            spawnPosition += Vector3.up * .2f;
            var instance = Instantiate(wormPrefab, spawnPosition, Quaternion.Euler(90f, Random.Range(0f, 360f), 0f), wormFolder);
            Destroy(instance, 60f);
        }
    }

    public void StartSpawning()
    {
        spawningCoroutine = SpawningCoroutine();
        StartCoroutine(spawningCoroutine);
    }

    public void StopSpawning()
    {
        StopCoroutine(spawningCoroutine);
    }
}
