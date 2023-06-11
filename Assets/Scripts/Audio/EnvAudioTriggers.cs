using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvAudioTriggers : MonoBehaviour
{
	bool IsMessAmbienceInitiated = false;
	void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			Debug.Log("IsMessAmbienceInitiated : " + IsMessAmbienceInitiated);
			Debug.Log("From env audio trigger, current game state : " + DayController.Instance._dayState);
		}
		// Player enters mess for breakfast...
		if (collider.CompareTag("Player") && (DayController.Instance._dayState == DayState.Breakfast || DayController.Instance._dayState == DayState.Dinner) && !IsMessAmbienceInitiated)
		{
			Debug.Log("Player hit mess hall trigger, started mess breakfast ambience");
			IsMessAmbienceInitiated = true;
			AudioController.Instance.BeginMessHallAmbience();
		}
		// Player exits mess for morning meeting...
		else if (collider.CompareTag("Player") && (DayController.Instance._dayState == DayState.MorningMeeting || DayController.Instance._dayState == DayState.CampFire) && IsMessAmbienceInitiated)
		{
			Debug.Log("Player hit mess hall trigger, ending mess breakfast ambience");
			IsMessAmbienceInitiated = false;
			AudioController.Instance.EndMessHallAmbience();
		}
	}
}
