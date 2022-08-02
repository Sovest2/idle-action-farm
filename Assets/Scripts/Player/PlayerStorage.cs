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
    [SerializeField] int currentCount = 0;

    public int Capacity
    {
        get { return capacity; }
        private set 
        {
            capacity = value;
            CapacityUpdated?.Invoke();
        }
    }
    public int CurrentCount
    {

        get { return currentCount; }
        private set 
        {
            currentCount = value;
            storageRenderer.enabled = currentCount > 0;
            currentScaleY = Mathf.Lerp(0, maxScaleY, (float)currentCount / capacity);
            Vector3 originalScale = storage.transform.localScale;
            storage.transform.localScale = new Vector3(originalScale.x, currentScaleY, originalScale.z);
            CurrentCountUpdated?.Invoke();
            
        }
    }

    float maxScaleY = 0;
    float currentScaleY = 0;

    MeshRenderer storageRenderer;

    void Start()
    {
        storageRenderer = storage.GetComponent<MeshRenderer>();
        maxScaleY = storage.transform.localScale.y;
        CurrentCount = currentCount;
        Capacity = capacity;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Wheat":
                if (currentCount >= capacity) break;
                StartCoroutine(GrabWheat(other.transform));
                Destroy(other);
                break;
        }
    }

    IEnumerator GrabWheat(Transform wheat)
    {
        CurrentCount++;
        Vector3 startScale = wheat.localScale;
        Vector3 startPosition = wheat.position;
        Quaternion startRotation = wheat.rotation;
     
        for(float progress = 0f; progress <= 1f; progress+= Time.deltaTime)
        {
            wheat.rotation = Quaternion.Lerp(startRotation, storage.transform.rotation, progress);
            wheat.position = Vector3.Lerp(startPosition, storage.transform.position, progress);
            wheat.localScale = Vector3.Lerp(startScale, Vector3.zero, progress);

            yield return null;
        }

        Destroy(wheat.gameObject);
    }

     
}
