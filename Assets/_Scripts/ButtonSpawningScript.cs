using UnityEngine;

public class SpawnPrefabOnMenuButton : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject prefab;

    [Header("Leap Hand Reference")]
    public Transform leftHandTransform;

    [Header("Menu Button Object")]
    public GameObject menuButtonObject;

    [Header("Hand-Relative Offsets")]
    public Vector3 localPositionOffset = new Vector3(0.05f, 0f, 0f);
    public Vector3 localRotationOffset = Vector3.zero;

    [Header("Calibration")]
    [Tooltip("Enable to freely move/rotate the spawned prefab in Play Mode")]
    public bool calibrationMode = false;

    private GameObject spawnedPrefab;
    private bool isToggledOn = false;

    void Update()
    {
        // Hide when menu button disappears
        if (!menuButtonObject.activeInHierarchy)
        {
            if (spawnedPrefab != null)
                spawnedPrefab.SetActive(false);
            return;
        }

        if (spawnedPrefab == null || !isToggledOn)
            return;

        // Position always follows
        spawnedPrefab.transform.localPosition = localPositionOffset;

        // Rotation control
        if (!calibrationMode)
        {
            spawnedPrefab.transform.localRotation = Quaternion.Euler(localRotationOffset);
        }
        else
        {
            // Capture live rotation while calibrating
            localRotationOffset = spawnedPrefab.transform.localRotation.eulerAngles;
        }
    }

    public void OnMenuButtonPressed()
    {
        isToggledOn = !isToggledOn;

        if (isToggledOn)
            ShowPrefab();
        else
            HidePrefab();
    }

    void ShowPrefab()
    {
        if (spawnedPrefab == null)
        {
            spawnedPrefab = Instantiate(prefab, leftHandTransform);
        }

        spawnedPrefab.SetActive(true);
    }

    void HidePrefab()
    {
        if (spawnedPrefab != null)
            spawnedPrefab.SetActive(false);
    }
}