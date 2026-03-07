using UnityEngine;
using Leap;
using System;

public class PalmUITrigger : MonoBehaviour
{
    // add PalmUIManager
    private PalmUIManager palmUIManager;

    [SerializeField] private float _maxRadius = 1.0f;

    private bool _triggered = false;
    private LeapProvider _provider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _provider = Hands.Provider;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
