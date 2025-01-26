using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Annihilator : MonoBehaviour
{
    [SerializeField] private TextMeshPro statusText;
    [SerializeField] private TextMeshPro timeText;
    [SerializeField] private int max = 10;
    [SerializeField] private float timeLimit = 60;
    [SerializeField] private bool addtoScoreOnAnnihilate = false;
    [SerializeField] private float shrinkTime = 1f;
    public int Index;

    [FormerlySerializedAs("acceptingBubbles")] [Header("Monitoring")] [SerializeField]
    private bool _acceptingBubbles = true;
    [SerializeField] List<GameObject> shrinkingObjects = new List<GameObject>();

    [SerializeField] private int count;

    public bool IsFull => count >= max;
    public TimeSpan TimeRemaining { get; private set; }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (timeLimit != 0)
        {
            TimeRemaining = TimeSpan.FromSeconds(timeLimit);
            StartCoroutine(TimerCoroutine());
            EventManager.Instance.AddTime.AddListener(OnAddTimeEvent);
        }
    }

    void OnAddTimeEvent(float timeAdd)
    {
        timeRemaining += timeAdd;
    }

    private float timeRemaining;

    IEnumerator TimerCoroutine()
    {
        timeRemaining = timeLimit;
        while (timeRemaining > 0)
        {
            timeText.text = Mathf.RoundToInt(timeRemaining).ToString(); // Update the UI text
            timeRemaining -= 1f; // Decrease time by 1 second
            TimeRemaining = TimeSpan.FromSeconds(timeRemaining);
            yield return new WaitForSeconds(1f); // Wait for 1 second
        }

        timeText.text = "LOCKED"; 
        StopAcceptingBubbles();
    }

    void StopAcceptingBubbles()
    {
        _acceptingBubbles = false;
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_acceptingBubbles)//count >= max)
            return;
        
        if (other.isTrigger)
            return;
        
        if (shrinkingObjects.Contains(other.gameObject))
            return;

        if (other.GetComponent<Player>() != null)
            return;
        // if (PhysicsUtility.IsPlayerOrAttachedTo(other.gameObject))
        //     return;
        
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
        UpCount();
        if (addtoScoreOnAnnihilate)
            PlayerState.Current.Score += WinCollider.pointsPerBubble;
    }

    void UpCount()
    {
        count++;
        statusText.text = count.ToString();

        if (count >= max)
        {
            //StopAllCoroutines();
            StopAcceptingBubbles();
            statusText.text = "FULL";
        }
    }
}
