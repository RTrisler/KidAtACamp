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

	AudioSource EnvAmbienceSrc1 => CameraSources.ElementAt(0);
	AudioSource EnvAmbienceSrc2 => CameraSources.ElementAt(1);
	AudioSource MusicSrc => CameraSources.ElementAt(2);
	AudioSource EnvEventSrc => CameraSources.ElementAt(3);

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
		EnvAmbienceSrc1.outputAudioMixerGroup = EnvGroup;
		EnvAmbienceSrc2.outputAudioMixerGroup = EnvGroup;
		EnvEventSrc.outputAudioMixerGroup = EnvGroup;
	}
	
	public void StartDay()
	{
		// Daycontroller singleton to differentiate (day counter)
		Debug.Log("AUDIOCONTROLLER/STARTDAY: CURRENT DAY: " + DayController.Instance._dayCounter);
		MusicSrc.loop = false;
		switch (DayController.Instance._dayCounter)
		{
			case 1:
				MusicSrc.clip = ClipRefs.DAYTHEMES.ElementAt(0);				
				MusicSrc.Play();
				EnvAmbienceSrc1.clip = ClipRefs.AMBIRDS.ElementAt(0);
				StartCoroutine(DelaySourceStart(EnvAmbienceSrc1, MusicSrc.clip.length - 2.5f, true));
				break;
			case 2:
				MusicSrc.clip = ClipRefs.DAYTHEMES.ElementAt(1);				
				StartCoroutine(SourceFadeIn(MusicSrc, 2.5f, 0.3f));
				EnvAmbienceSrc1.clip = ClipRefs.AMBIRDS.ElementAt(0);
				StartCoroutine(DelaySourceStart(EnvAmbienceSrc1, MusicSrc.clip.length - 2.5f, true));
				break;
			case 3:
				MusicSrc.clip = ClipRefs.DAYTHEMES.ElementAt(2);				
				StartCoroutine(SourceFadeIn(MusicSrc, 2.5f, 0.3f));
				EnvAmbienceSrc1.clip = ClipRefs.AMBIRDS.ElementAt(0);
				StartCoroutine(DelaySourceStart(EnvAmbienceSrc1, MusicSrc.clip.length - 2.5f, true));
				break;
			case 4:
				MusicSrc.clip = ClipRefs.DAYTHEMES.ElementAt(3);
				StartCoroutine(SourceFadeIn(MusicSrc, 2.5f, 0.3f));
				EnvAmbienceSrc1.clip = ClipRefs.AMBIRDS.ElementAt(0);
				StartCoroutine(DelaySourceStart(EnvAmbienceSrc1, MusicSrc.clip.length - 2.5f, true));
				break;
			case 5:
				Debug.Log("day5 from audio controller");
				MusicSrc.clip = ClipRefs.DAYTHEMES.ElementAt(4);
				StartCoroutine(SourceFadeIn(MusicSrc, 2.5f, 0.3f));
				EnvAmbienceSrc1.clip = ClipRefs.AMBIRDS.ElementAt(0);
				StartCoroutine(DelaySourceStart(EnvAmbienceSrc1, MusicSrc.clip.length - 2.5f, true));
				break;
		}
	}

	public void SummonChildren()
	{
		EnvEventSrc.clip = ClipRefs.EVENTS.ElementAt(0);
		EnvEventSrc.volume = 0.25f;
		EnvEventSrc.Play();
	}
	
	void StartGuidedTaskMusic()
	{
		StartCoroutine(SourceFadeOut(EnvAmbienceSrc1, 5f, 0.1f, false));
		switch (DayController._dayCounter)
		{
			case 1:
				Debug.Log("Soon to be playing guided task music for day 1...");
				MusicSrc.clip = ClipRefs.TASKTHEMES.ElementAt(0);
				break;
			case 2:
				Debug.Log("Soon to be playing guided task music for day 2...");
				MusicSrc.clip = ClipRefs.TASKTHEMES.ElementAt(1);
				break;
			case 3:
				Debug.Log("Soon to be playing guided task music for day 3...");
				MusicSrc.clip = ClipRefs.TASKTHEMES.ElementAt(2);
				break;
			case 4:
				Debug.Log("Soon to be playing guided task music for day 4...");
				MusicSrc.clip = ClipRefs.TASKTHEMES.ElementAt(3);
				break;
			case 5:
				Debug.Log("Soon to be playing guided task music for day 5...");
				break;
		}
		MusicSrc.loop = true;
		StartCoroutine(SourceFadeIn(MusicSrc, 15f, 0.3f));
	}

	void EndGuidedTaskMusic()
	{
		MusicSrc.loop = false;
		StartCoroutine(SourceFadeOut(MusicSrc, 5f, 0f));
	}

	// Free roam time CONSTANT 1 MINUTE
	void StartFreeRoamMusic()
	{
		StartCoroutine(SourceFadeOut(EnvAmbienceSrc1, 5f, 0.1f, false));
		MusicSrc.clip = ClipRefs.FREEROAM.ElementAt(0); 	
		StartCoroutine(SourceFadeIn(MusicSrc, 15f, 0.2f));
	}

	public void StateChangeInit(DayState newState)
	{
		switch (newState)
		{
			case DayState.WakeUp:
				if (DayController.Instance._dayCounter > 1)
				{
					StartCoroutine(SourceFadeOut(EnvAmbienceSrc2, 3f, 0f));
				}
				Debug.Log("Waking up from audio controller");
				StartDay();
				break;
			case DayState.Breakfast:
				// Breakfast audio state change is taken care of in the EnvAudioTrigger for the MessHall
				break;
			case DayState.MorningMeeting:
				// Morning meeting audio state change is taken care of in the EnvAudioTrigger for the MessHall
				break;
			case DayState.FreeRoam:
				StartFreeRoamMusic();
				break;
			case DayState.FreeTimeMeetup:
				SummonChildren();
				break;
			case DayState.GuidedTask:
				StartGuidedTaskMusic();
				break;
			case DayState.Dinner:
				EndGuidedTaskMusic();
				BeginDuskAmbience();
				break;
			case DayState.CampFire:
				break;
			case DayState.Bedtime:
				Debug.Log("(AC) Bedtime! Campfire sound dies in 10 seconds");
				EnvEventSrc.loop = false;
				StartCoroutine(SourceFadeOut(EnvEventSrc, 10f, 0f));
				break;
		}
	}

	public void PlayFinaleMusic()
	{
		StartCoroutine(SourceFadeOut(EnvAmbienceSrc1, 10f, 0f));
		MusicSrc.clip = ClipRefs.DAYTHEMES.ElementAt(5);
		MusicSrc.loop = true;
		StartCoroutine(SourceFadeIn(MusicSrc, 10f, 0.25f));
	}

	void BeginDuskAmbience()
	{	
		Debug.Log("Beginning dusk ambience...");
		StartCoroutine(SourceFadeOut(EnvAmbienceSrc1, 10f, 0f));
		EnvAmbienceSrc2.clip = ClipRefs.PMCRICKETS.ElementAt(0);
		StartCoroutine(SourceFadeIn(EnvAmbienceSrc2, 10f, 0.5f));
	}

	public void BeginMessHallAmbience()
	{	
		Debug.Log("AC: Hit BeginMessHallAmbience");
		if (DayController.Instance._dayState == DayState.Breakfast)
		{
			// false => do not disable bird sounds loop
			StartCoroutine(SourceFadeOut(EnvAmbienceSrc1, 5f, 0.01f, false));
			// true => loop mess hall commotion
			EnvAmbienceSrc2.clip = ClipRefs.COMMOTION.ElementAt(0);
			StartCoroutine(SourceFadeIn(EnvAmbienceSrc2, 5f, 0.095f, true));
		}
		else if (DayController.Instance._dayState == DayState.Dinner)
		{
			// Fade out cricket src at dinner
			StartCoroutine(SourceFadeOut(EnvAmbienceSrc2, 5f, 0.01f, false));
			// Fade in commotion src at dinner
			EnvAmbienceSrc1.clip = ClipRefs.COMMOTION.ElementAt(0);
			StartCoroutine(SourceFadeIn(EnvAmbienceSrc1, 5f, 0.095f, true));
		}
	}

	public void EndMessHallAmbience()
	{
		Debug.Log("AC: Hit EndMessHallAmbience");
		if (DayController.Instance._dayState == DayState.MorningMeeting)
		{
			// Fade out mess hall commotion, disable loop
			StartCoroutine(SourceFadeOut(EnvAmbienceSrc2, 20f, 0f, true));
			// Fade back in bird noises, do not disable loop
			StartCoroutine(SourceFadeIn(EnvAmbienceSrc1, 20f, 0.3f, false));
		}
		else if (DayController.Instance._dayState == DayState.CampFire)
		{
			// Fade out mess hall commotion, disable loop
			StartCoroutine(SourceFadeOut(EnvAmbienceSrc1, 20f, 0f, true));
			// Fade back in cricket noises, do not disable loop
			StartCoroutine(SourceFadeIn(EnvAmbienceSrc2, 20f, 0.3f, false));	
		}
	}

	public void StartFireAmbience()
	{
		Debug.Log("Starting fire ambience (AC)... crickets should be quieter");
		StartCoroutine(SourceFadeOut(EnvAmbienceSrc2, 5f, 0.25f, false));
		EnvEventSrc.loop = true;
		EnvEventSrc.clip = ClipRefs.CAMPFIRES.ElementAt(0);
		StartCoroutine(SourceFadeIn(EnvEventSrc, 0.5f, 0.15f));
	}

	public void IntensifyFireAmbience()
	{
		Debug.Log("Intensifying fire ambience (AC)... fire should be louder");
		StartCoroutine(SourceFadeIn(EnvEventSrc, 0.5f, 0.45f));	
	}

	public void LOUDER()
	{
		Debug.Log("Intensifying fire ambience (AC)... fire should be LOUDERLOUDER");
		StartCoroutine(SourceFadeIn(EnvEventSrc, 1.5f, 0.45f));	
	}

	IEnumerator DelaySourceStart(AudioSource source, float themeLength, bool fadeIn = false)
	{
		Debug.Log("Waiting for theme to finish...");
		yield return new WaitForSeconds(themeLength);
		Debug.Log("Theme finished, triggering env ambience...");
		if (fadeIn)
		{
			Debug.Log("Fading in...");
			StartCoroutine(SourceFadeIn(source, 10f, 0.3f));
		}
		else
		{
			EnvAmbienceSrc1.Play();
		}
	}
	
	IEnumerator SourceFadeIn(AudioSource source, float duration, float targetVolume, bool changeLoop = true)
	{
		if (!source.isPlaying)
		{
			source.Play();
			source.volume = 0.0001f;
		}

		if (changeLoop && (source == EnvAmbienceSrc1 || source == EnvAmbienceSrc2))
		{
			source.loop = true;
		}

        float currentTime = 0;
        float start = source.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
			//Debug.Log("Env noise 1 volume: " + EnvAmbienceSrc1.volume);
			//Debug.Log("Env noise 2 volume: " + EnvAmbienceSrc2.volume);
            yield return null;
        }
        yield break;
	}

	IEnumerator SourceFadeOut(AudioSource source, float duration, float targetVolume, bool changeLoop = true)
	{
		if (changeLoop && (source == EnvAmbienceSrc1 || source == EnvAmbienceSrc2))
		{
			source.loop = false;
		}

        float currentTime = 0;
        float start = source.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
			//Debug.Log("Env noise 1 volume: " + EnvAmbienceSrc1.volume);
			//Debug.Log("Env noise 2 volume: " + EnvAmbienceSrc2.volume);
            yield return null;
        }
        yield break;
	}

	public void MoveToPosition(Vector3 cameraPosition)
	{
		transform.position = cameraPosition;
	}
}
