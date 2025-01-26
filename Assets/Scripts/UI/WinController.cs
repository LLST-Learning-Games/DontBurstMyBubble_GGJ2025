using System;
using UnityEngine;
using UnityEngine.Serialization;

public class WinController : MonoBehaviour
{
    [SerializeField] private GameObject _winRoot;

    public void TriggerWin()
    {
        _winRoot.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
