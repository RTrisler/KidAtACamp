using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class AudioController : MonoBehaviour
{
	public static AudioController Instance;
	public Camera CurrentCamera;

	[SerializeField]
	ClipRefs ClipRefs;
	[SerializeField]
	DayController DayController;
	[SerializeField]
	List<AudioSource> CameraSources;

	[SerializeField]
	AudioMixer Mixer;
	[SerializeField]
	AudioMixerGroup EnvGroup;
	[SerializeField]
	AudioMixerGroup MusicGroup;

	AudioSource EnvAmbienceSrc => CameraSources.ElementAt(0);
	AudioSource MusicSrc => CameraSources.ElementAt(1);
	AudioSource EnvEventSrc => CameraSources.ElementAt(2);

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

	public void InitializeCameraAudio()
	{
			
	}

	void Start()
	{	
		MusicSrc.outputAudioMixerGroup = MusicGroup;
		EnvAmbienceSrc.outputAudioMixerGroup = EnvGroup;
		EnvEventSrc.outputAudioMixerGroup = EnvGroup;
	}
	
	public void StartDay()
	{
		// Daycontroller singleton to differentiate (day counter)
		Debug.Log("AUDIOCONTROLLER/STARTDAY: CURRENT DAY: " + DayController.Instance._dayCounter);
		switch (DayController.Instance._dayCounter)
		{
			case 1:
				MusicSrc.clip = ClipRefs.DAYTHEMES.ElementAt(0);
				MusicSrc.Play();
				EnvAmbienceSrc.clip = ClipRefs.AMBIRDS.ElementAt(0);
				StartCoroutine(DelayEnvAmbience(MusicSrc.clip.length - 2.5f, true));
				break;
			case 2:
				break;
			case 3:
				break;
			case 4:
				break;
			case 5:
				break;
		}
	}

	IEnumerator DelayEnvAmbience(float themeLength, bool fadeIn = false)
	{
		Debug.Log("Waiting for theme to finish...");
		yield return new WaitForSeconds(themeLength);
		Debug.Log("Theme finished, triggering env ambience...");
		if (fadeIn)
		{
			Debug.Log("Fading in...");
			StartCoroutine(EnvAmbienceFadeIn(10f, 0.3f));
		}
		else
		{
			EnvAmbienceSrc.Play();
		}
	}
	
	IEnumerator EnvAmbienceFadeIn(float duration, float targetVolume)
	{
		EnvAmbienceSrc.Play();
		EnvAmbienceSrc.volume = 0.0001f;
		EnvAmbienceSrc.loop = true;

        float currentTime = 0;
        float start = EnvAmbienceSrc.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            EnvAmbienceSrc.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
			Debug.Log("Env noise volume: " + EnvAmbienceSrc.volume);
            yield return null;
        }
        yield break;
	}

	public void MoveToPosition(Vector3 cameraPosition)
	{
		transform.position = cameraPosition;
	}
}
