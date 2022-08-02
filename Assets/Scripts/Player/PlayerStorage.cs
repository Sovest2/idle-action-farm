using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStorage : MonoBehaviour
{

    public Action CapacityUpdated;
    public Action CurrentCountUpdated;

    [SerializeField] GameObject storage;
    [SerializeField] int capacity;

    

    public Queue<PlantBlockData> BlockDataStorage { get; private set; } = new Queue<PlantBlockData>();
    public int CurrentCount { get; private set; }

    public int Capacity
    {
        get { return capacity; }
        private set 
        {
            capacity = value;
            CapacityUpdated?.Invoke();
        }
    }

    float maxScaleY = 0;
    float currentScaleY = 0;

    MeshRenderer storageRenderer;

    void Start()
    {
        storageRenderer = storage.GetComponent<MeshRenderer>();
        maxScaleY = storage.transform.localScale.y;
        Capacity = capacity;
        OnListChanged();
    }

    private void Update()
    {
        if(CurrentCount != BlockDataStorage.Count) 
            OnListChanged();
    }

    void OnListChanged()
    {
        CurrentCount = BlockDataStorage.Count;
        storageRenderer.enabled = CurrentCount > 0;
        currentScaleY = Mathf.Lerp(0, maxScaleY, (float)CurrentCount / capacity);
        Vector3 originalScale = storage.transform.localScale;
        storage.transform.localScale = new Vector3(originalScale.x, currentScaleY, originalScale.z);
        CurrentCountUpdated?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PlantBlock":
                if (BlockDataStorage.Count >= capacity) break;
                StartCoroutine(GrabWheat(other.gameObject));
                Destroy(other);
                break;
        }
    }

    IEnumerator GrabWheat(GameObject wheat)
    {
        PlantBlockData blockData = wheat.GetComponent<PlantBlock>().Data;
        BlockDataStorage.Enqueue(blockData);
        Transform wheatTransform = wheat.transform; ;
        Vector3 startScale = wheatTransform.localScale;
        Vector3 startPosition = wheatTransform.position;
        Quaternion startRotation = wheatTransform.rotation;
     
        for(float progress = 0f; progress <= 1f; progress+= Time.deltaTime)
        {
            wheatTransform.rotation = Quaternion.Lerp(startRotation, storage.transform.rotation, progress);
            wheatTransform.position = Vector3.Lerp(startPosition, storage.transform.position, progress);
            wheatTransform.localScale = Vector3.Lerp(startScale, Vector3.zero, progress);

            yield return null;
        }

        Destroy(wheat.gameObject);
    }

     
}
