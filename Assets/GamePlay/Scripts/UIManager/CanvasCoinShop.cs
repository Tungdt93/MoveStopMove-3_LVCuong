using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasCoinShop : UICanvas
{
    [SerializeField] private TextMeshProUGUI _coinAmountText;
    public override void OnInit()
    {
        _coinAmountText.gameObject.GetComponent<TextMeshProUGUI>().text = "" + UIManager.Instance.coinAmount;
    }
    public void XButton()
    {
        UIManager.Instance.OpenUI(UIName.MainMenu);
    }

    public void BuyCoin()
    {
        UIManager.Instance.coinAmount += 25000;
        PlayerPrefs.SetInt("Score", UIManager.Instance.coinAmount);
        PlayerPrefs.Save();
        _coinAmountText.GetComponent<TextMeshProUGUI>().text = "" + UIManager.Instance.coinAmount;
    }

    public void NoAds()
    {
        UIManager.Instance.coinAmount = 0;
        PlayerPrefs.SetInt("Score", UIManager.Instance.coinAmount);
        PlayerPrefs.Save();
        _coinAmountText.GetComponent<TextMeshProUGUI>().text = "" + UIManager.Instance.coinAmount;
    }
}
