using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public static CameraZone _currentZone = null;

    [SerializeField]
    private Camera _zoneCamera;

    public Camera cam => _zoneCamera;

    private void Start()
    {
        _zoneCamera.enabled = false;
    }

    public void DisableCamera()
    {
        _zoneCamera.enabled = false;
    }

    public void EnableCamera()
    {
        _zoneCamera.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
	        Debug.Log($"Enter camzone");
            if(_currentZone != null)
            {
                _currentZone.DisableCamera();
            }
            _currentZone = this;
            _currentZone.EnableCamera();

            AudioController.Instance.MoveToPosition(_currentZone._zoneCamera.transform.position);
			AudioController.Instance.CurrentCamera = _zoneCamera;
			if (_zoneCamera.transform.parent.name == "Cabin3 (7)" && DayController.Instance._dayState == DayState.CampFire)
			{
				AudioController.Instance.StartFireAmbience();
			}
			else if (_zoneCamera.transform.parent.name == "Cabin3 (8)" && DayController.Instance._dayState == DayState.CampFire)
			{
				AudioController.Instance.IntensifyFireAmbience();
			}
			else if (_zoneCamera.transform.parent.name == "Cabin3 (10)" && DayController.Instance._dayState == DayState.CampFire)
			{
				AudioController.Instance.LOUDER();
			}
        }
    }
}
