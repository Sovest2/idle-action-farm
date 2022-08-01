using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    Collider collider;

    void Start()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
        CutButton.CutPressed += ActivateTrigger;
    }

    private void OnDestroy()
    {
        CutButton.CutPressed -= ActivateTrigger;
    }

    void ActivateTrigger()
    {
        collider.enabled = true;
        StartCoroutine(DeactivateTrigger());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Plant plant))
        {
            plant.Harvest();
        }
    }

    IEnumerator DeactivateTrigger()
    {
        yield return new WaitForSeconds(0.8f);
        collider.enabled = false;
    }
}
