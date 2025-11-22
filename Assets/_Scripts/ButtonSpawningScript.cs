using UnityEngine;
using UnityEngine.Events;

public class SpawnCubeOnMenuButton : MonoBehaviour
{
    [Header("Cube Settings")]
    public GameObject cubePrefab;
    public float spawnDistanceFromCamera = 0.5f;

    [Header("References")]
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
        // Hook event so calling OnMenuButtonPressed() spawns cube
        onMenuButtonPressed.AddListener(SpawnCube);
    }

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

    // This gets called from the Ultraleap Menu Button
    public void OnMenuButtonPressed()
    {
        Debug.Log("Buttom Pressed");
        onMenuButtonPressed?.Invoke();
    }
}
