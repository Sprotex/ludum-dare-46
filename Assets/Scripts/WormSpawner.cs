using System.Collections;
using UnityEngine;

public class WormSpawner : MonoBehaviour
{
    public GameObject wormPrefab;
    public float delayBetweenSpawns = 5f; // TODO(Andy): Optimize before build! This is good for testing only!
    public float playerDistance = 20f;
    public Transform playerTransform;

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
            var spawnPosition = playerTransform.position + (Vector3)(Random.insideUnitCircle * playerDistance);
            for (var i = 0; i < 5; ++i) {
                if (Physics.Raycast(spawnPosition + Vector3.up * 20f, Vector3.down, out RaycastHit hit, 100f, -1)) {
                    spawnPosition.y = hit.point.y;
                    break;
                } 
            }
            spawnPosition += Vector3.up * .2f;
            Instantiate(wormPrefab, spawnPosition, Quaternion.Euler(90f, Random.Range(0f, 360f), 0f), null);
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
