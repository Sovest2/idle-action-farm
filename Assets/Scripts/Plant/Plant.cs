using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Plant : MonoBehaviour
{
    public Action Harvested;

    [SerializeField] int progressLimit = 10;
    [SerializeField] int progress = 0;


    [SerializeField] GameObject ungrownPrefab;
    [SerializeField] GameObject grownPrefab;
    [SerializeField] GameObject blockPrefab;

    GameObject model;

    [SerializeField] bool isGrow = true;

    public bool IsGrow 
    {
        get { return isGrow; }
        private set 
        {
            isGrow = value;
        } 
    }
    
    public int Progress 
    { 
        get { return progress; }
        private set
        {
            progress = value;
            model.transform.localScale = Vector3.Lerp(ungrownPrefab.transform.localScale, grownPrefab.transform.localScale, (float) Progress / progressLimit);
        }
    }

    public int ProgressLimit
    {
        get { return progressLimit; }
        private set 
        {
            progressLimit = value; 
        }
    }

    void OnEnable()
    {
        StartCoroutine(Grow());
    }

    private void Start()
    {
        ChooseModel(Instantiate(Progress >= ProgressLimit ? grownPrefab : ungrownPrefab));
        Progress = progress;
    }

    public void Harvest()
    {
        if (IsGrow) return;
        Progress--;
        Harvested?.Invoke();
        StartCoroutine(SpawnBlock());

        if (Progress <= 0)
        {
            IsGrow = true;
            StartCoroutine(Grow());
        }
    }

    IEnumerator SpawnBlock()
    {
        Transform block = Instantiate(blockPrefab).transform;
        Collider blockCollider = block.GetComponent<Collider>();
        blockCollider.enabled = false;

        

        float rotationY = UnityEngine.Random.Range(0f, 360f);
        Quaternion targetRotation = Quaternion.Euler(0, rotationY, 0);

        float offset = UnityEngine.Random.Range(1, 2f);
        Vector3 targetPosition = transform.position + (targetRotation * transform.forward * offset);

        for (float i = 0f; i <= 1; i+= Time.deltaTime)
        {
            block.position = Vector3.Lerp(transform.position, targetPosition, i);
            block.rotation = Quaternion.Lerp(transform.rotation, targetRotation, i);
            yield return null;
        }

        blockCollider.enabled = true;

    }

    IEnumerator Grow()
    {     
        if (!IsGrow) yield break;

        ChooseModel(Instantiate(ungrownPrefab));

        for (Progress = 0; Progress < progressLimit; Progress++)
            yield return new WaitForSeconds(1f);

        ChooseModel(Instantiate(grownPrefab));
        IsGrow = false;
    }

    void ChooseModel(GameObject currentState)
    {
        if(model != null) Destroy(model);
        model = currentState;
        model.transform.position = transform.position;
        model.transform.parent = transform;
    }
}
