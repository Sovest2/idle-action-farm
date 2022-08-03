using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBlock : MonoBehaviour
{
    [SerializeField] PlantBlockData data;

    public Collider BlockCollider { get; private set; }

    public PlantBlockData Data
    {
        get { return data; }
        private set { data = value; }
    }

    private void Awake()
    {
        BlockCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        BlockCollider.enabled = true;
        transform.localScale = Data.prefab.transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                PlayerStorage storage = other.GetComponent<PlayerStorage>();
                storage.PickUp(this);
                break;
        }
    }
}
