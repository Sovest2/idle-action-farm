using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SellCart : MonoBehaviour
{
    Coroutine unloadCoroutine;

    public Action<int> BlockSold;

    

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
            BlockSold?.Invoke(blockData.cost);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
