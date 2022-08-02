using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Plant Block Data", order = 1)]
public class PlantBlockData : ScriptableObject
{
    public string title;
    public int cost;
    public GameObject prefab;
}
