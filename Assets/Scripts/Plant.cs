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
        float offsetX = Random.Range(-2f, 2f);
        float offsetZ = Random.Range(-2f, 2f);

        Vector3 offset = new Vector3(offsetX, 0, offsetZ);

        float rotationY = Random.Range(0f, 180f);
        Instantiate(blockPrefab, transform.position + offset, Quaternion.Euler(0, rotationY, 0));

        if (Progress <= 0)
        {
            IsGrow = true;
            StartCoroutine(Grow());
        }
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
