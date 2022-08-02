using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text coinsText;

    [SerializeField] RectTransform coinsImage;
    [SerializeField] GameObject coinPrefab;
    Camera mainCamera;

    GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;
        mainCamera = Camera.main;
        SellCart.BlockSold += OnSellBlock;
        gm.CoinsValueChanged += OnCoinsValueChanged;
    }

    private void OnDestroy()
    {
        SellCart.BlockSold -= OnSellBlock;
        gm.CoinsValueChanged -= OnCoinsValueChanged;
    }

    void OnCoinsValueChanged()
    {
        coinsText.text = $"{gm.Coins}";
    }

    void OnSellBlock(int value, Vector3 position)
    {
        StartCoroutine(GainCoin(value, position));
    }

    IEnumerator GainCoin(int value, Vector3 position)
    {
        yield return null;
        RectTransform coinTransform = Instantiate(coinPrefab).GetComponent<RectTransform>();
        coinTransform.SetParent((RectTransform)transform, false);
        
        Vector2 startPosition = mainCamera.WorldToScreenPoint(position);
        for (float i = 0; i < 1f; i += Time.deltaTime)
        {
            coinTransform.position = Vector2.Lerp(startPosition, coinsImage.position, i);
            yield return null;
        }
        Destroy(coinTransform.gameObject);

        gm.Coins += value;
    }

}
