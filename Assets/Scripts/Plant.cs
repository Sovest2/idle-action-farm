using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Plant : MonoBehaviour
{
    [SerializeField] int progressLimit = 10;
    [SerializeField] TMP_Text progressText;

    [SerializeField] GameObject ungrownPrefab;
    [SerializeField] GameObject grownPrefab;
    [SerializeField] GameObject blockPrefab;

    GameObject model;

    public bool IsGrow { get; private set; } = true;
    private int progress = 0;
    public int Progress 
    { 
        get { return progress; }
        private set
        {
            progress = value;
            progressText.text = $"{progress}/{progressLimit}";
        }
    }

    


    void OnEnable()
    {
        StartCoroutine(Grow());
    }

    public void Harvest()
    {
        if (IsGrow) return;
        Progress--;
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

        

        float rotationY = Random.Range(0f, 360f);
        Quaternion targetRotation = Quaternion.Euler(0, rotationY, 0);

        float offset = Random.Range(1, 2f);
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
            yield return new WaitForSeconds(0.1f);

        ChooseModel(Instantiate(grownPrefab));
        IsGrow = false;
    }

    void ChooseModel(GameObject currentState)
    {
        Destroy(model);
        model = currentState;
        model.transform.position = transform.position;
        model.transform.parent = transform;
    }
}
