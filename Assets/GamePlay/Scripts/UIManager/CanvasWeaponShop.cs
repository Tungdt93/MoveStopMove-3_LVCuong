using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum WeaponState { CantBuy, CanBuy, Select, Equipped }
public enum weaponType { ARROW, AXE, BATTLEAXE, BOOMERANG, LOLLIPOP, CANDYCANE, ICECREAMCONE, SWIRLYPOP, HAMMER, KNIFE, UZI, Z }

public class CanvasWeaponShop : UICanvas
{
    public int[] weaponPrices = {2500, 800, 1500, 400, 100, 200, 1000, 600, 30, 200, 3000, 2000};
    [SerializeField] private TextMeshProUGUI _coinAmountText;
    [SerializeField] private TextMeshProUGUI _weaponName;
    [SerializeField] private TextMeshProUGUI _canBuyText;
    [SerializeField] private TextMeshProUGUI _cantBuyText;
    [SerializeField] private Image shopCurrentWeapon;
    [SerializeField] private Transform _ListWeapon;
    [SerializeField] private Transform weaponStateButton;
    [SerializeField] private Transform CantBuy;
    [SerializeField] private Transform CanBuy;
    [SerializeField] private Transform Select;
    [SerializeField] private Transform Equipped;
    Dictionary<weaponType, WeaponState> WeaponShopInfo = new Dictionary<weaponType, WeaponState>();
    private int ShopWeaponID;
    private void Awake()
    {
        ShopWeaponID = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < weaponPrices.Length; i++)
        {
            WeaponShopInfo.Add((weaponType)i, WeaponState.CantBuy);
        }
    }
    public void OpenMainMenu()
    {
        UIManager.Instance.OpenUI(UIName.MainMenu);
    }

    public override void OnInit()
    {
        _coinAmountText.gameObject.GetComponent<TextMeshProUGUI>().text = "" + UIManager.Instance.coinAmount;
        ShowWeaponImage((weaponType)ShopWeaponID);
    }

    public void NextButton()
    {
        
        if (ShopWeaponID < 11)
        {
            ShopWeaponID++;
            ShowWeaponImage((weaponType)ShopWeaponID);
            UpdateWeaponShopState();
            SetPriceText(weaponPrices[ShopWeaponID]);
            ShowState();
        }
    }
    public void BackButton()
    {
        if (ShopWeaponID > 0)
        {
            ShopWeaponID--;
            ShowWeaponImage((weaponType)ShopWeaponID);
            UpdateWeaponShopState();
            SetPriceText(weaponPrices[ShopWeaponID]);
            ShowState();
            
        }
    }
    public void ShowWeaponImage(weaponType weaponType)
    {
        for (int i = 0; i < _ListWeapon.childCount; i++)
        {
            if (i == (int)weaponType)
            {
                _ListWeapon.GetChild(i).gameObject.SetActive(true);
                _weaponName.GetComponent<TextMeshProUGUI>().text = "" + weaponType;

            }
            else 
            {
                _ListWeapon.GetChild(i).gameObject.SetActive(false);
            } 
        }
    }
    public void UpdateWeaponShopState()
    {
        for (int i = 0; i < weaponPrices.Length; i++)
        {
            if (UIManager.Instance.coinAmount >= weaponPrices[i] && WeaponShopInfo[(weaponType)i] == WeaponState.CantBuy)
            {
                WeaponShopInfo.Remove((weaponType)i);
                WeaponShopInfo.Add((weaponType)i, WeaponState.CanBuy);
            }
            else if (UIManager.Instance.coinAmount<weaponPrices[i] && WeaponShopInfo[(weaponType)i] == WeaponState.CanBuy)
            {
                WeaponShopInfo.Remove((weaponType)i);
                WeaponShopInfo.Add((weaponType)i, WeaponState.CantBuy);
            } 
        }
    }

    public void BuyWeapon()
    {
        if(WeaponShopInfo[(weaponType)ShopWeaponID] == WeaponState.CanBuy)
        {
            WeaponShopInfo.Remove((weaponType)ShopWeaponID);
            WeaponShopInfo.Add((weaponType)ShopWeaponID, WeaponState.Select);
            UIManager.Instance.coinAmount -= weaponPrices[(int)((weaponType)ShopWeaponID)];
            ShowState();
            UpdateCoinAmount();
            PlayerPrefs.SetInt("Score", UIManager.Instance.coinAmount);
            PlayerPrefs.Save();
        }
    }
    public void SelectWeapon()
    {
        if (WeaponShopInfo[(weaponType)ShopWeaponID] == WeaponState.Select)
        {
            for (int i = 0; i < WeaponShopInfo.Count; i++)
            {
                if (WeaponShopInfo[(weaponType)i] == WeaponState.Equipped)
                {
                    WeaponShopInfo.Remove((weaponType)i);
                    WeaponShopInfo.Add((weaponType)i, WeaponState.Select);
                }
            }
            GameObject.FindObjectOfType<PlayerController>().weaponSwitching((Character.weaponType)ShopWeaponID, new Character.weaponMaterialsType[] { Character.weaponMaterialsType.Arrow });
            WeaponShopInfo.Remove((weaponType)ShopWeaponID);
            WeaponShopInfo.Add((weaponType)ShopWeaponID, WeaponState.Equipped);
            ShowState();
        }
    }

    void ShowState()
    {
        for (int i = 0; i < weaponStateButton.transform.childCount; i++)
        {
            if (i == (int)WeaponShopInfo[(weaponType)ShopWeaponID])
            {
                weaponStateButton.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                weaponStateButton.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void SetPriceText(int price)
    {
        _canBuyText.GetComponent<TextMeshProUGUI>().text =""+ price;
        _cantBuyText.GetComponent<TextMeshProUGUI>().text =""+ price;
    }

    void UpdateCoinAmount()
    {
        _coinAmountText.gameObject.GetComponent<TextMeshProUGUI>().text = "" + UIManager.Instance.coinAmount;
    }
}
