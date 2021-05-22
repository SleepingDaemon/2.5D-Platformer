using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _coinText = null;

    private Player _player = null;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
            Debug.Log("Player is null");
    }

    private void Update()
    {
        _coinText.text = "Coins: " + _player.GetCoinAmount();
    }
}
