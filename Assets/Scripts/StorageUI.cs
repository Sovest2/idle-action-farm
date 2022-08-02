using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StorageUI : MonoBehaviour
{

    [SerializeField] TMP_Text storageText;
    [SerializeField] Slider storageSlider;

    PlayerStorage playerStorage;


    // Start is called before the first frame update
    void Start()
    {
        playerStorage = FindObjectOfType<PlayerStorage>();
        storageSlider.maxValue = playerStorage.Capacity;
        OnCurrentCountUpdated();

        //playerStorage.CapacityUpdated +=
        playerStorage.CurrentCountUpdated += OnCurrentCountUpdated;
    }

    private void OnDestroy()
    {
        playerStorage.CurrentCountUpdated -= OnCurrentCountUpdated;
    }

    void OnCurrentCountUpdated()
    {
        storageText.text = $"Storage: {playerStorage.CurrentCount}/{playerStorage.Capacity}";
        storageSlider.value = playerStorage.CurrentCount;
    }
}
