using System.Collections;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] GameObject bubblePrefab;
    [SerializeField] Vector2 minMaxWaitTime = new Vector2(0.01f, 0.05f);
    [SerializeField] Vector2 minMaxScale = new Vector2(0.8f, 1.2f);
    [SerializeField] float spawnRadius = 1f;

    [SerializeField] private int maxBubbles = 10000;
    [Header("Monitoring")]
    [SerializeField] private int currentCount;
    [SerializeField] private bool _spawningActive = true;
    
    private const float SPAWN_TOGGLE_POLLING_TIME = 0.01f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnBubblesCoroutine());
    }
    
    public void SetSpawningActive(bool active) => _spawningActive = active;
    
    IEnumerator SpawnBubblesCoroutine()
    {
        currentCount = 0;
        
        do
        {
            if (!_spawningActive)
            {
                yield return new WaitForSeconds(SPAWN_TOGGLE_POLLING_TIME);
                continue;
            }
            
            SpawnBubble();
            currentCount++;
            var waitTime = Random.Range(minMaxWaitTime.x, minMaxWaitTime.y);
            yield return new WaitForSeconds(waitTime);
        } while (currentCount < maxBubbles);
    }
    
    void SpawnBubble()
    {
        var spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.z = 0; // we're in 2D.
        var newBubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity, transform);
        
        var scaleMultiplier = Random.Range(minMaxScale.x, minMaxScale.y);
        newBubble.transform.localScale *= scaleMultiplier;
    }
}
