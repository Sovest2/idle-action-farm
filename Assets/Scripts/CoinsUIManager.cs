using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text coinsText;

    [SerializeField] RectTransform coinsImage;
    [SerializeField] GameObject coinPrefab;

    Animator coinsIconAnimator;
    Camera mainCamera;
    GameManager gm;

    float currentAmount;
    int targetAmount;

    int isWiggle = Animator.StringToHash("Wiggle");

    void Start()
    {
        gm = GameManager.Instance;
        mainCamera = Camera.main;
        coinsIconAnimator = coinsImage.GetComponent<Animator>();
        SellCart.BlockSold += OnSellBlock;
        gm.CoinsValueChanged += OnCoinsValueChanged;
    }

    private void OnDestroy()
    {
        SellCart.BlockSold -= OnSellBlock;
        gm.CoinsValueChanged -= OnCoinsValueChanged;
    }

    private void Update()
    {
        if ((int) currentAmount != targetAmount)
        {
            currentAmount += (targetAmount - currentAmount) * 0.1f;
            coinsText.text = $"{currentAmount: 000}";
        }
    }

    void OnCoinsValueChanged()
    {
        targetAmount = gm.Coins;
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

        coinsIconAnimator.SetTrigger(isWiggle);
        yield return new WaitForSeconds(0.5f);
        gm.Coins += value;
        coinsIconAnimator.SetTrigger(isWiggle);
    }

}
