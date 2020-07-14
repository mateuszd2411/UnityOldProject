using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounterUI : MonoBehaviour {

    private Text moneyText;

    private void Awake()
    {
        moneyText = GetComponent<Text>();
    }

    private void Update()
    {
        moneyText.text = "MONEY: " + GameMaster.Money.ToString();
    }
}
