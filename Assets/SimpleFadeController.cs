using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class SimpleFadeController : MonoBehaviour
{
    private CanvasGroup group;
    private bool running = false;

    public static SimpleFadeController Instance;
    void Awake()
    {
        Instance = this;
        group = GetComponent<CanvasGroup>();
    }

    public IEnumerator Fade(float duration, bool fadeIn = true)
    {
        if (running) yield break;
        
        running = true;
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            var t = (Time.time - startTime) / duration;
            if (fadeIn)
            {
                group.alpha = t;    
            }
            else
            {
                group.alpha = 1 - t;
            }

            yield return null;
        }

        running = false;
    }
}
