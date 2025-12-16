using UnityEngine;

/// <summary>
/// Controller for the recenter button.
/// When pressed, it requests a recenter operation from the ROSConnectionController.
/// </summary>
public class RecenterButtonController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ROSConnectionController rosConnectionController;

    private void Awake()
    {
        // Try to find ROSConnectionController if not assigned
        if (rosConnectionController == null)
        {
            rosConnectionController = FindObjectOfType<ROSConnectionController>();
            if (rosConnectionController == null)
            {
                Debug.LogWarning("ROSConnectionController not found! Recenter button will not work.");
            }
        }
    }

    /// <summary>
    /// Called when the recenter button is pressed (via Ultraleap Interaction Button)
    /// </summary>
    public void OnRecenterButtonPressed()
    {
        if (rosConnectionController != null)
        {
            rosConnectionController.RequestRecenter();
            Debug.Log("Recenter requested.");
        }
        else
        {
            Debug.LogWarning("Cannot recenter - ROSConnectionController not assigned!");
        }
    }

    private void Reset()
    {
        // Try to auto-assign ROSConnectionController
        rosConnectionController = FindObjectOfType<ROSConnectionController>();
    }
}

