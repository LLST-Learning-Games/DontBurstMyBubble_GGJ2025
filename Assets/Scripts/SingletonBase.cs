using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance { get { return _instance; } } 
    // Note we're not using lazy instantiation cause we want the singleton attached to a game object. This has the downside that calls to singletons in Awake are unreliable. --riko 6/2024

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this as T;
            //DontDestroyOnLoad(gameObject); // only works on root game objects...
        }
    }
}
