using UnityEngine;
using Leap;
using Leap.Attachments;
using System.Collections.Generic;

public class PalmUIManager : MonoBehaviour
{
    private AttachmentHands attachmentHands;
    private Dictionary<string, bool> _triggeredObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attachmentHands = GetComponent<AttachmentHands>();
        _triggeredObjects = new Dictionary<string, bool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateHandUI()
    {
        int triggeringObjects = 0;

        foreach (var triggeredObject in _triggeredObjects)
        {
            if (triggeredObject.Value)
            {
                triggeringObjects++;
            }

            attachmentHands?.gameObject.SetActive(triggeringObjects <= 0);
        }
    }

    public void AddTriggeredObject(string name)
    {
        if (_triggeredObjects.ContainsKey(name))
        {
            _triggeredObjects[name] = true;
        } else
        {
            _triggeredObjects.Add(name, true);
        }

        // update ui
        UpdateHandUI();
    }

    public void RemoveTriggeredObject(string name)
    {
        if (_triggeredObjects.ContainsKey(name))
        {
            _triggeredObjects[name] = false;
        } else
        {
            _triggeredObjects.Remove(name);
        }

        // update ui
        UpdateHandUI() ;
    }


}
