using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public Action CoinsValueChanged;
    public static GameManager Instance { get; private set; }

    [SerializeField] int coins;

    

    public int Coins
    {
        get { return coins; }
        set 
        { 
            coins = value;
            CoinsValueChanged?.Invoke();
        }
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Coins = coins;
    }





}
