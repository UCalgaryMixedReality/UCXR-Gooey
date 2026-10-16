using System;
using System.Data;
using System.Threading.Tasks;
using Unity.Loading;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalSceneManager : MonoBehaviour
{
    public static GlobalSceneManager Instance;
    [SerializeField] private RawImage loadingIcon;
    [SerializeField] private GameObject loadingCanvas;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadScene("Main");
    }

    void Update()
    {
        float speed = 100;
        loadingIcon.transform.Rotate(0,0, -speed * Time.deltaTime);
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
                loadingCanvas.SetActive(true);
        do
        {
            // custom logic if needed for loading screen
        } while (scene.progress < 0.9f);
        await Task.Delay(5000);
        scene.allowSceneActivation = true;
        loadingCanvas.SetActive(false);
    }
}
