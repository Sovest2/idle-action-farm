using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantUI : MonoBehaviour
{
    [SerializeField] bool isEnabled;
    [SerializeField] GameObject canvas;
    [SerializeField] TMP_Text progressText;
    [SerializeField] float textShowTime = 1f;

    Coroutine showCoroutine;

    Plant plant;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        if (!isEnabled) return;
        plant = GetComponent<Plant>();
        plant.Harvested += OnHarvested;
        
    }

    private void OnDestroy()
    {
        if (!isEnabled) return;
        plant.Harvested -= OnHarvested;
    }

    void OnHarvested()
    {
        progressText.text = $"{plant.Progress}/{plant.ProgressLimit}";

        if(showCoroutine != null) StopCoroutine(showCoroutine);
        showCoroutine = StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        canvas.SetActive(true);
        yield return new WaitForSeconds(textShowTime);
        canvas.SetActive(false);
    }
}
