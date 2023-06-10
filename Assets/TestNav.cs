using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class TestNav : MonoBehaviour
{
    public NavMeshAgent testAgent;
    public LayerMask navLayer;

    public Transform kys;
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            testAgent.destination = kys.position;
        }
    }
}
