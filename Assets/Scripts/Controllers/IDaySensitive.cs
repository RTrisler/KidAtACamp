using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDaySensitive : MonoBehaviour
{
    [SerializeField]
    private int _dayToSetActive;

    private Vector3 _origianlPosition;
    private Vector3 _outOfWorldSpace;
    private void Start()
    {
        DayController.Instance.OnStateChange += CheckState;
        _origianlPosition = this.transform.position;
        _outOfWorldSpace = new Vector3(0, 0, 0);
    }
    private void OnDisable()
    {
        DayController.Instance.OnStateChange -= CheckState;
    }

    private void CheckState(int day, DayState currentState)
    {
        if(day == _dayToSetActive)
        {
            this.transform.position = _origianlPosition;
        }
        else
        {
            this.transform.position = _outOfWorldSpace;
        }
    }
}
