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
    public Transform freeRoamNodesParent;
    public Transform campFireNodesParent;


    [HideInInspector]
    public List<Transform> cabinStartNodes = new();
    [HideInInspector]
    public List<Transform> messHallNodes = new();
    [HideInInspector]
    public List<Transform> freeRoamNodes = new();
    [HideInInspector]
    public List<Transform> campFireNodes = new();

    public List<Camper> camperPrefabs;
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
        foreach (Transform t in freeRoamNodesParent)
        {
            freeRoamNodes.Add(t);
        }
        foreach (Transform t in campFireNodesParent)
        {
            campFireNodes.Add(t);
        }

        if (camperPrefabs.Count != messHallNodes.Count
            || camperPrefabs.Count != freeRoamNodes.Count
            || camperPrefabs.Count != campFireNodes.Count
            || camperPrefabs.Count != cabinStartNodes.Count)
        {
            Debug.LogError($"Mismatch in node counts & campers");
            return;
        }

        // inst campers at spawn points, shuffle the others, use id to key into the others
        for(int i = 0; i < camperPrefabs.Count; i++)
        {
            var camper = GameObject.Instantiate(camperPrefabs[i], cabinStartNodes[i].position, Quaternion.identity);
            camper.camperIndex = i;
            campers.Add(camper);
        }
        
        messHallNodes.Shuffle(rng);
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
