using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private TextMeshProUGUI _coinAmountText;
    [SerializeField] private TextMeshProUGUI _zonebest;
    [SerializeField] private Transform OpenSound, NoSound;
    [SerializeField] private Transform vibration, noVibration;
    [SerializeField] private Transform woodRank, silverRank, goldRank;
    [SerializeField] private Slider playerEXPSlider;
    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.playerEXP > 100) UpdatePlayerRank(25);
    }

    public void StartGame()
    {

    }

    public override void OnInit()
    {
        _coinAmountText.gameObject.GetComponent<TextMeshProUGUI>().text = "" + UIManager.Instance.coinAmount;
    }

    public void PlayGame()
    {
        UIManager.Instance.OpenUI(UIName.GamePlay);
        GameManager.Instance.gameState = GameManager.GameState.gameStarted;
    }

    public void OpenWeaponShop()
    {
        UIManager.Instance.OpenUI(UIName.WeaponShop);
    }

    public void OpenSkinShop()
    {
        UIManager.Instance.OpenUI(UIName.SkinShop);
    }

    public void OpenCoinShop()
    {
        UIManager.Instance.OpenUI(UIName.CoinShop);
    }

    public void ChangeOpenSoundState()
    {
        UIManager.Instance.SoundState = !UIManager.Instance.SoundState;
        if (UIManager.Instance.SoundState)
        {
            OpenSound.gameObject.SetActive(true);
            NoSound.gameObject.SetActive(false);
        }
        else
        {
            OpenSound.gameObject.SetActive(false);
            NoSound.gameObject.SetActive(true);
        }
    }
    public void ChangeVibrationState()
    {
        UIManager.Instance.vibrationState = !UIManager.Instance.vibrationState;
        if (UIManager.Instance.vibrationState)
        {
            vibration.gameObject.SetActive(true);
            noVibration.gameObject.SetActive(false);
        }
        else
        {
            vibration.gameObject.SetActive(false);
            noVibration.gameObject.SetActive(true);
        }
    }

    public void UpdatePlayerRank(int EXP)
    {
        woodRank.gameObject.SetActive(true);
        silverRank.gameObject.SetActive(false);
        goldRank.gameObject.SetActive(false);
        UIManager.Instance.playerEXP += EXP;
        if (UIManager.Instance.playerEXP > 100)
        {
            UIManager.Instance.playerEXP -= 100;
            if (UIManager.Instance.playerRank == PlayerRank.Wood)
            {
                UIManager.Instance.playerRank = PlayerRank.Silver;
                woodRank.gameObject.SetActive(false);
                silverRank.gameObject.SetActive(true);
            }
            else if (UIManager.Instance.playerRank == PlayerRank.Silver)
            {
                UIManager.Instance.playerRank = PlayerRank.Gold;
                silverRank.gameObject.SetActive(false);
                goldRank.gameObject.SetActive(true);
            }
        }
        playerEXPSlider.GetComponent<Slider>().value = UIManager.Instance.playerEXP;

    }

    public void UpdateBest(int zone, int best)
    {
        _zonebest.GetComponent<TextMeshProUGUI>().text = "ZONE: " + zone + " - BEST:#" + best;
    }
}
