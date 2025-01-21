using System.Collections;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] GameObject bubblePrefab;
    [SerializeField] Vector2 minMaxWaitTime = new Vector2(0.01f, 0.05f);
    [SerializeField] float spawnRadius = 1f;

    [SerializeField] private int maxBubbles = 10000;
    [Header("Monitoring")]
    [SerializeField] private int currentCount;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnBubblesCoroutine());
    }

    IEnumerator SpawnBubblesCoroutine()
    {
        currentCount = 0;
        
        do
        {
            SpawnBubble();
            currentCount++;
            var waitTime = Random.Range(minMaxWaitTime.x, minMaxWaitTime.y);
            yield return new WaitForSeconds(waitTime);
        } while (currentCount < maxBubbles);
    }
    
    void SpawnBubble()
    {
        var spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
    }
}
