using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    Collider scytheCollider;
    Coroutine triggerCoroutine;

    [SerializeField] float activateDelay = 0.2f;
    [SerializeField] float deactivateDelay = 0.5f;

    void Start()
    {
        scytheCollider = GetComponent<Collider>();
        scytheCollider.enabled = false;
        CutButton.CutPressed += Cut;
    }

    private void OnDestroy()
    {
        CutButton.CutPressed -= Cut;
    }

    void Cut()
    {
        if(triggerCoroutine!= null) StopCoroutine(triggerCoroutine);
        triggerCoroutine = StartCoroutine(EffectTrigger());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Plant plant))
        {
            plant.Harvest();
        }
    }

    IEnumerator EffectTrigger()
    {
        scytheCollider.enabled = false;
        yield return new WaitForSeconds(activateDelay);
        scytheCollider.enabled = true;
        yield return new WaitForSeconds(deactivateDelay);
        scytheCollider.enabled = false;
    }
}
