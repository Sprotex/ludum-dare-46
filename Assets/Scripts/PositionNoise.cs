using UnityEngine;

public class PositionNoise : MonoBehaviour
{
    public Vector3 noiseOffset;
    public float shakeScale = 1f;
    private Vector3 offset = new Vector3();
    private void Update()
    {
        offset.x = Mathf.PerlinNoise(Time.time + noiseOffset.x, Time.time + noiseOffset.x) - .5f;
        offset.y = Mathf.PerlinNoise(Time.time + noiseOffset.y, Time.time + noiseOffset.y) - .5f;
        offset.z = Mathf.PerlinNoise(Time.time + noiseOffset.z, Time.time + noiseOffset.z) - .5f;
        offset *= shakeScale;
        transform.localPosition = offset;
    }
}
