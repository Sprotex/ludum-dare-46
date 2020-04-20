using System.Collections;
using UnityEngine;

public class CrowSpawner : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public GameObject crowPrefab;
    public float delayBetweenSpawns = 30f; 
    public int maxWormCount = 10;
    public Health playerHealth;
    public Transform crowFolder;

    private IEnumerator spawningCoroutine;

    private void Start()
    {
        StartSpawning();
    }

    private IEnumerator SpawningCoroutine()
    {
        while (true)
        {

           /* if (FeedingPoints.instance.food.Count >= maxWormCount)
                continue;*/

            var tileSize = mapGenerator.tileSize;
            var halfMapSize = mapGenerator.halfMapSize;
            var mapPosition = mapGenerator.origin + new Vector2(tileSize * halfMapSize.x * (.5f - Random.value), tileSize * halfMapSize.y * (.5f - Random.value)) * 2;
            var spawnPosition = new Vector3(mapPosition.x, 60.0f, mapPosition.y);
        
            
            var instance = Instantiate(crowPrefab, spawnPosition, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f), crowFolder);
            instance.GetComponent<CrowMovement>().playerHealth = playerHealth;
            Destroy(instance, 60f);
            yield return new WaitForSeconds(delayBetweenSpawns);
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
