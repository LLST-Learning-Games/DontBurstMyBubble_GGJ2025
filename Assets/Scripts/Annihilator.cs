using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Annihilator : MonoBehaviour
{
    [SerializeField] private float shrinkTime = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(Shrink(other.gameObject, shrinkTime));
    }

    IEnumerator Shrink(GameObject obj, float time)
    {
        var endScale = Vector3.zero;
        
        Vector3 startScale = obj.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            obj.transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale; // Ensure exact target scale
        Destroy(obj);    
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
