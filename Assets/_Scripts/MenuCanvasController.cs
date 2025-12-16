using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

/// <summary>
/// Controls the animated world-space menu canvas that appears in front of the user.
/// Handles pop-up animation and contains UI elements for the home screen.
/// </summary>
public class MenuCanvasController : MonoBehaviour
{
    [Header("Canvas Settings")]
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private float animationDuration = 0.3f;
    [SerializeField] private AnimationCurve popUpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private float distanceFromCamera = 0.6f;
    [SerializeField] private float verticalOffset = 0f;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI batteryPercentageText;
    [SerializeField] private Button settingsButton;
    [SerializeField] private ScrollRect appsScrollView;
    [SerializeField] private Transform appsContentParent;

    [Header("Camera Reference")]
    [SerializeField] private Transform mainCamera;

    private bool isMenuOpen = false;
    private Coroutine animationCoroutine;
    private Vector3 originalScale;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (menuCanvas == null)
            menuCanvas = GetComponent<Canvas>();

        if (mainCamera == null && Camera.main != null)
            mainCamera = Camera.main.transform;

        canvasGroup = menuCanvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = menuCanvas.gameObject.AddComponent<CanvasGroup>();

        originalScale = menuCanvas.transform.localScale;
        
        // Start with menu hidden
        SetMenuVisibility(false, false);
    }

    /// <summary>
    /// Toggles the menu on/off with animation
    /// </summary>
    public void ToggleMenu()
    {
        if (isMenuOpen)
            CloseMenu();
        else
            OpenMenu();
    }

    /// <summary>
    /// Opens the menu with animation
    /// </summary>
    public void OpenMenu()
    {
        if (isMenuOpen) return;

        if (mainCamera == null)
        {
            Debug.LogWarning("Main Camera not assigned! Cannot position menu.");
            return;
        }

        // Position canvas in front of camera
        PositionCanvasInFrontOfCamera();

        // Start animation
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine = StartCoroutine(AnimateMenu(true));
    }

    /// <summary>
    /// Closes the menu with animation
    /// </summary>
    public void CloseMenu()
    {
        if (!isMenuOpen) return;

        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine = StartCoroutine(AnimateMenu(false));
    }

    /// <summary>
    /// Positions the canvas in front of the camera
    /// </summary>
    private void PositionCanvasInFrontOfCamera()
    {
        Vector3 cameraForward = mainCamera.forward;
        Vector3 cameraPosition = mainCamera.position;
        
        // Position in front of camera
        Vector3 targetPosition = cameraPosition + cameraForward * distanceFromCamera;
        targetPosition.y += verticalOffset;

        // Make canvas face the camera
        menuCanvas.transform.position = targetPosition;
        menuCanvas.transform.LookAt(mainCamera.position);
        menuCanvas.transform.Rotate(0, 180, 0); // Flip to face camera correctly
    }

    /// <summary>
    /// Animates the menu pop-up/pop-down
    /// </summary>
    private IEnumerator AnimateMenu(bool opening)
    {
        float elapsed = 0f;
        Vector3 startScale = menuCanvas.transform.localScale;
        Vector3 targetScale = opening ? originalScale : Vector3.zero;
        float startAlpha = canvasGroup.alpha;
        float targetAlpha = opening ? 1f : 0f;

        SetMenuVisibility(true, true); // Enable canvas but keep it invisible initially if closing

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animationDuration;
            float curveValue = popUpCurve.Evaluate(t);

            // Animate scale
            menuCanvas.transform.localScale = Vector3.Lerp(startScale, targetScale, curveValue);
            
            // Animate alpha
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, curveValue);

            yield return null;
        }

        // Ensure final values
        menuCanvas.transform.localScale = targetScale;
        canvasGroup.alpha = targetAlpha;
        SetMenuVisibility(opening, opening);

        isMenuOpen = opening;
        animationCoroutine = null;
    }

    /// <summary>
    /// Sets the visibility and interactability of the menu
    /// </summary>
    private void SetMenuVisibility(bool visible, bool interactable)
    {
        canvasGroup.alpha = visible ? 1f : 0f;
        canvasGroup.interactable = interactable;
        canvasGroup.blocksRaycasts = interactable;
    }

    /// <summary>
    /// Updates the battery percentage display (placeholder)
    /// </summary>
    public void UpdateBatteryPercentage(float percentage)
    {
        if (batteryPercentageText != null)
        {
            batteryPercentageText.text = $"{percentage:F0}%";
        }
    }

    /// <summary>
    /// Called when settings button is pressed (placeholder)
    /// </summary>
    public void OnSettingsButtonPressed()
    {
        Debug.Log("Settings button pressed - functionality to be implemented");
    }

    private void Reset()
    {
        if (Camera.main != null)
            mainCamera = Camera.main.transform;
    }
}

