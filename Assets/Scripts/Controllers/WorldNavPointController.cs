using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class WorldNavPointController : MonoBehaviour
{
    public static WorldNavPointController Instance;
    public Transform cabinStartNodesParent;
    public Transform messHallNodesParent;
    public Transform morningMeetingNodesParent;
    public Transform freeRoamNodesParent;
    public Transform campFireNodesParent;


    [HideInInspector]
    public List<Transform> cabinStartNodes = new();
    [HideInInspector]
    public List<Transform> messHallNodes = new();
    [HideInInspector]
    public List<Transform> morningMeetingNodes = new();
    [HideInInspector]
    public List<Transform> freeRoamNodes = new();
    [HideInInspector]
    public List<Transform> campFireNodes = new();

    public Camper camperPrefab;
    private List<Camper> campers = new List<Camper>();

    private System.Random rng;

    void Awake()
    {
        Instance = this;
        
        rng = new System.Random();

        foreach (Transform t in cabinStartNodesParent)
        {
            cabinStartNodes.Add(t);
        }
        foreach (Transform t in messHallNodesParent)
        {
            messHallNodes.Add(t);
        }
        foreach (Transform t in morningMeetingNodesParent)
        {
            morningMeetingNodes.Add(t);
        }
        foreach (Transform t in freeRoamNodesParent)
        {
            freeRoamNodes.Add(t);
        }
        foreach (Transform t in campFireNodesParent)
        {
            campFireNodes.Add(t);
        }

        if (cabinStartNodes.Count != messHallNodes.Count
            || cabinStartNodes.Count != morningMeetingNodes.Count
            || cabinStartNodes.Count != freeRoamNodes.Count
            || cabinStartNodes.Count != campFireNodes.Count)
        {
            Debug.LogError($"Mismatch in node counts; ({cabinStartNodes.Count}) ({messHallNodes.Count}) ({morningMeetingNodes.Count}) ({freeRoamNodes.Count}) ({campFireNodes.Count})");
            return;
        }

        // inst campers at spawn points, shuffle the others, use id to key into the others
        for(int i = 0; i < cabinStartNodes.Count; i++)
        {
            var camper = GameObject.Instantiate(camperPrefab, cabinStartNodes[i].position, Quaternion.identity);
            camper.camperIndex = i;
            campers.Add(camper);
        }
        
        messHallNodes.Shuffle(rng);
        morningMeetingNodes.Shuffle(rng);
        freeRoamNodes.Shuffle(rng);
        campFireNodes.Shuffle(rng);
    }

    public bool CampersHaveArrived()
    {
        foreach (var camper in campers)
        {
            if (!camper.Arrived()) return false;
        }

        return true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
        // === DEBUG ===
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     Goto(DayState.Breakfast);
        // }
        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     Goto(DayState.MorningMeeting);
        // }
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     Goto(DayState.FreeRoam);
        // }
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     Goto(DayState.FreeTimeMeetup);
        // }
        // if (Input.GetKeyDown(KeyCode.T))
        // {
        //     Goto(DayState.Dinner);
        // }
        // if (Input.GetKeyDown(KeyCode.Y))
        // {
        //     Goto(DayState.CampFire);
        // }
        // if (Input.GetKeyDown(KeyCode.U))
        // {
        //     Goto(DayState.Bedtime);
        // }
    }

    // public void Goto(DayState state)
    // {
    //     switch (state)
    //     {
    //             case DayState.WakeUp:
    //             case DayState.Breakfast:
    //                 for (int i = 0; i < campers.Count; i++)
    //                 {
    //                     campers[i].Goto(messHallNodes[i].position);
    //                 }
    //                 break;
    //             case DayState.MorningMeeting:
    //                 for (int i = 0; i < campers.Count; i++)
    //                 {
    //                     campers[i].Goto(morningMeetingNodes[i].position);
    //                 }
    //                 break;
    //             case DayState.FreeRoam:
    //                 for (int i = 0; i < campers.Count; i++)
    //                 {
    //                     campers[i].Goto(freeRoamNodes[i].position);
    //                 }
    //                 break;
    //             case DayState.FreeTimeMeetup:
    //                 for (int i = 0; i < campers.Count; i++)
    //                 {
    //                     campers[i].Goto(campFireNodes[i].position);
    //                 }
    //                 break;
    //             case DayState.GuidedTask:
    //             case DayState.Dinner:
    //                 for (int i = 0; i < campers.Count; i++)
    //                 {
    //                     campers[i].Goto(messHallNodes[i].position);
    //                 }
    //                 break;
    //             case DayState.CampFire:
    //                 for (int i = 0; i < campers.Count; i++)
    //                 {
    //                     campers[i].Goto(campFireNodes[i].position);
    //                 }
    //                 break;
    //             case DayState.Bedtime:
    //                 for (int i = 0; i < campers.Count; i++)
    //                 {
    //                     campers[i].Goto(cabinStartNodes[i].position);
    //                 }
    //                 break;
    //             default:
    //                 break;
    //     }
    // }

    
}

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list, System.Random rng)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
}
