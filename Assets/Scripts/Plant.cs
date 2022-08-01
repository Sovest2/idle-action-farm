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

    public int? Harvest(int value)
    {
        if (IsGrow) return null;

        if (Progress <= value)
        {
            IsGrow = true;
            StartCoroutine(Grow());
        }
        else
        {
            Progress -= value;
        }
        return value;
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
