using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SellCart : MonoBehaviour
{
    Coroutine unloadCoroutine;

    public static Action<int, Vector3> BlockSold;

    [SerializeField] Transform unloadTarget;

    

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                unloadCoroutine = StartCoroutine(UnloadStorage(other.GetComponent<PlayerStorage>()));
                break;
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                if (unloadCoroutine != null) StopCoroutine(unloadCoroutine);
                break;
        }
        
    }

    IEnumerator UnloadStorage(PlayerStorage playerStorage)
    {
        while (playerStorage.BlockDataStorage.Count > 0)
        {
            var blockData = playerStorage.BlockDataStorage.Dequeue();
            StartCoroutine(UnloadBlock(blockData, playerStorage.transform));
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator UnloadBlock(PlantBlockData blockData, Transform storage)
    {
        Transform block = Instantiate(blockData.prefab, storage.position, storage.rotation, null).transform;
        block.GetComponent<Collider>().enabled = false;
        for (float i = 0f; i < 1f; i += Time.deltaTime)
        {
            block.position = Vector3.Lerp(storage.position, unloadTarget.position, i);
            block.rotation = Quaternion.Lerp(storage.rotation, unloadTarget.rotation, i);
            yield return null;
        }
        Destroy(block.gameObject);
        BlockSold?.Invoke(blockData.cost, unloadTarget.position);
    }
}
