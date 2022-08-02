using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBlock : MonoBehaviour
{
    [SerializeField] PlantBlockData data;

    public PlantBlockData Data
    {
        get { return data; }
        private set { data = value; }
    }
}
