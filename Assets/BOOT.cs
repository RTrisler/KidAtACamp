using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BOOT : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);

        SceneManager.LoadScene("Main", LoadSceneMode.Additive);
        SceneManager.LoadScene("Terrain", LoadSceneMode.Additive);
        SceneManager.LoadScene("Navigation", LoadSceneMode.Additive);
        SceneManager.LoadScene("CameraZones", LoadSceneMode.Additive);
        SceneManager.LoadScene("GameTriggers", LoadSceneMode.Additive);
        SceneManager.LoadScene("AudioTriggers", LoadSceneMode.Additive);
    }
}
