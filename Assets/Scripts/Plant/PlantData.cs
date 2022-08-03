using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Plant Data", order = 1)]
public class PlantData : ScriptableObject
{
    public int growTime;

    public GameObject ungrownPrefab;
    public GameObject grownPrefab;

    public GameObject harvestParticles;

    public PlantBlockData data;
}
