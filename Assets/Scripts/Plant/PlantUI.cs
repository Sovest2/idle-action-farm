using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantUI : MonoBehaviour
{
    [SerializeField] TMP_Text progressText;
    [SerializeField] float textShowTime = 1f;

    Coroutine showCoroutine;

    Plant plant;

    // Start is called before the first frame update
    void Start()
    {
        plant = GetComponent<Plant>();
        plant.Harvested += OnHarvested;
        progressText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
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
        progressText.gameObject.SetActive(true);
        yield return new WaitForSeconds(textShowTime);
        progressText.gameObject.SetActive(false);
    }
}
