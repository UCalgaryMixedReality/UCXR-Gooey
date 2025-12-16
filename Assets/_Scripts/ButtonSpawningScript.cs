using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles menu button press events and toggles the menu canvas.
/// This script connects the Ultraleap menu button to the MenuCanvasController.
/// </summary>
public class SpawnCubeOnMenuButton : MonoBehaviour
{
    [Header("Menu Canvas Reference")]
    [SerializeField] private MenuCanvasController menuCanvasController;

    [Header("Legacy Settings (for backwards compatibility)")]
    public GameObject cubePrefab;
    public float spawnDistanceFromCamera = 0.5f;
    public Transform mainCamera;
    public UnityEvent onMenuButtonPressed;

    private void Reset()
    {
        // Automatically tries to fill in camera reference
        if (Camera.main != null)
            mainCamera = Camera.main.transform;
    }

    private void Awake()
    {
        // Try to find MenuCanvasController if not assigned
        if (menuCanvasController == null)
        {
            menuCanvasController = FindObjectOfType<MenuCanvasController>();
        }

        // Keep legacy event system for backwards compatibility
        if (onMenuButtonPressed != null)
            onMenuButtonPressed.AddListener(SpawnCube);
    }

    /// <summary>
    /// Called from the Ultraleap Menu Button - toggles the menu canvas
    /// </summary>
    public void OnMenuButtonPressed()
    {
        Debug.Log("Menu Button Pressed");
        
        if (menuCanvasController != null)
        {
            menuCanvasController.ToggleMenu();
        }
        else
        {
            Debug.LogWarning("MenuCanvasController not assigned! Falling back to legacy cube spawn.");
            // Fallback to legacy behavior
            onMenuButtonPressed?.Invoke();
        }
    }

    /// <summary>
    /// Legacy method - kept for backwards compatibility
    /// </summary>
    public void SpawnCube()
    {
        if (cubePrefab == null)
        {
            Debug.LogWarning("Cube Prefab is missing!");
            return;
        }

        if (mainCamera == null)
        {
            Debug.LogWarning("Main Camera missing!");
            return;
        }

        // Spawn cube in front of camera (works well in the Playground scene)
        Vector3 spawnPos = mainCamera.position + mainCamera.forward * spawnDistanceFromCamera;

        GameObject cube = Instantiate(cubePrefab, spawnPos, Quaternion.identity);

        // Ensure cube has physics
        if (cube.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = cube.AddComponent<Rigidbody>();
            rb.mass = 0.5f;
            rb.angularDamping = 0.05f;
        }

        // Add collider if missing
        if (cube.GetComponent<Collider>() == null)
        {
            BoxCollider col = cube.AddComponent<BoxCollider>();
            col.size = Vector3.one;
        }

        Debug.Log("Spawned cube from menu button.");
    }
}
