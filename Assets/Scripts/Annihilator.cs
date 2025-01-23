using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Annihilator : MonoBehaviour
{
    [SerializeField] private float shrinkTime = 1f;
    [Header("Monitoring")]
    [SerializeField] List<GameObject> shrinkingObjects = new List<GameObject>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
            return;
        
        if (shrinkingObjects.Contains(other.gameObject))
            return;
        
        shrinkingObjects.Add(other.gameObject);
        StartCoroutine(Shrink(other.gameObject, shrinkTime));
    }

    IEnumerator Shrink(GameObject obj, float time)
    {
        var endScale = Vector3.zero;
        
        Vector3 startScale = obj.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            if (!obj) // Someone else destroyed the object already.
                yield break;
                
            obj.transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale; // Ensure exact target scale
        shrinkingObjects.Remove(obj);
        Destroy(obj);    
    }
}
