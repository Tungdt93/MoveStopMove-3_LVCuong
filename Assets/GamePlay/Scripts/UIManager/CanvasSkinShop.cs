using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum ShopType { HatShop, PantShop, ShieldShop, SetShop }
public enum ClothState { CanBuy, Select, Equipped }
public enum ClothLockState { Lock, UnLock }
public enum ClothType { Rau, Crown, Ear, Hat, HatCap, HatYellow, Arrow, Cowboy, Headphone,
                        Chambi, Comy, Dabao, Onion, RainBow, Vantim, Batman, Pikachu, Skull,
                        Shield, Khien,
                        Devil, Angel, Witch, Deadpool, Thor}

public class CanvasSkinShop : UICanvas
{
    public int[] ClothesPrice ={
                        100, 120, 150, 160, 180, 200, 220, 250, 280,
                        250, 290, 300, 330, 350, 380, 400, 450, 550,
                        700, 850,
                        1500, 2000, 2500, 3000, 3500};
    [SerializeField] private TextMeshProUGUI _coinAmountText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Transform _Shop;
    [SerializeField] private Transform _HatShopBG;
    [SerializeField] private Transform _PantShopBG;
    [SerializeField] private Transform _ShieldShopBG;
    [SerializeField] private Transform _SetShopBG;
    [SerializeField] private Transform _HatShopSCV;
    [SerializeField] private Transform _PantShopSCV;
    [SerializeField] private Transform _ShieldShopSCV;
    [SerializeField] private Transform _SetShopSCV;
    [SerializeField] private Image[] _Frame;
    [SerializeField] private Image[] _Lock;
    [SerializeField] private Transform _CanBuy;
    [SerializeField] private Transform _Select;
    Dictionary<ClothType, ClothState> ClothesShopInfo = new Dictionary<ClothType, ClothState>();
    Dictionary<ClothType, ClothLockState> ClothesLockInfo = new Dictionary<ClothType, ClothLockState>();
    public int ClothesID;
    private void Awake()
    {
        ClothesID = 0;
        for (int i = 0; i < 25; i++)
        {
            ClothesShopInfo.Add((ClothType)i, ClothState.CanBuy);
            ClothesLockInfo.Add((ClothType)i, ClothLockState.Lock);
        }
        
    }
    public override void OnInit()
    {
        _coinAmountText.gameObject.GetComponent<TextMeshProUGUI>().text = "" + UIManager.Instance.coinAmount;
        HatShopClick();
        GetRauID();
    }
    public void XButton()
    {
        UIManager.Instance.OpenUI(UIName.MainMenu);
    }
    #region Show Shop
    public void HatShopClick()
    {
        OpenShop(ShopType.HatShop);
        _HatShopBG.gameObject.SetActive(false);
        _PantShopBG.gameObject.SetActive(true);
        _ShieldShopBG.gameObject.SetActive(true);
        _SetShopBG.gameObject.SetActive(true);

        _HatShopSCV.gameObject.SetActive(true);
        _PantShopSCV.gameObject.SetActive(false);
        _ShieldShopSCV.gameObject.SetActive(false);
        _SetShopSCV.gameObject.SetActive(false);
    }
    public void PantShopClick()
    {
        OpenShop(ShopType.PantShop);
        _HatShopBG.gameObject.SetActive(true);
        _PantShopBG.gameObject.SetActive(false);
        _ShieldShopBG.gameObject.SetActive(true);
        _SetShopBG.gameObject.SetActive(true);

        _HatShopSCV.gameObject.SetActive(false);
        _PantShopSCV.gameObject.SetActive(true);
        _ShieldShopSCV.gameObject.SetActive(false);
        _SetShopSCV.gameObject.SetActive(false);
    }
    public void ShieldShopClick()
    {
        OpenShop(ShopType.ShieldShop);
        _HatShopBG.gameObject.SetActive(true);
        _PantShopBG.gameObject.SetActive(true);
        _ShieldShopBG.gameObject.SetActive(false);
        _SetShopBG.gameObject.SetActive(true);

        _HatShopSCV.gameObject.SetActive(false);
        _PantShopSCV.gameObject.SetActive(false);
        _ShieldShopSCV.gameObject.SetActive(true);
        _SetShopSCV.gameObject.SetActive(false);
    }
    public void SetShopClick()
    {
        OpenShop(ShopType.SetShop);
        _HatShopBG.gameObject.SetActive(true);
        _PantShopBG.gameObject.SetActive(true);
        _ShieldShopBG.gameObject.SetActive(true);
        _SetShopBG.gameObject.SetActive(false);

        _HatShopSCV.gameObject.SetActive(false);
        _PantShopSCV.gameObject.SetActive(false);
        _ShieldShopSCV.gameObject.SetActive(false);
        _SetShopSCV.gameObject.SetActive(true);
    }
    void OpenShop(ShopType _shopType)
    {
        for (int i = 0; i < _Shop.childCount; i++)
        {
            if (i == (int)_shopType)
            {
                 _Shop.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    #endregion

    #region Get ID
    public void GetRauID()
    {
        ClothesID = 8;
        ChangePrice();
    }

    public void GetCrownID()
    {
        ClothesID = 2;
        ChangePrice();
    }

    public void GetEarID()
    {
        ClothesID = 3;
        ChangePrice();
    }

    public void GetHatID()
    {
        ClothesID = 4;
        ChangePrice();
    }

    public void GetHatCapID()
    {
        ClothesID = 5;
        ChangePrice();
    }

    public void GetHatYellowID()
    {
        ClothesID = 6;
        ChangePrice();
    }

    public void GetArrowID()
    {
        ClothesID = 0;
        ChangePrice();
    }

    public void GetCowboyID()
    {
        ClothesID = 1;
        ChangePrice();
    }

    public void GetHeadphoneID()
    {
        ClothesID = 7;
        ChangePrice();
    }

    public void GetChambiID()
    {
        ClothesID = 12;
        ChangePrice();
    }

    public void GetComyID()
    {
        ClothesID = 13;
        ChangePrice();
    }

    public void GetDabaoID()
    {
        ClothesID = 14;
        ChangePrice();
    }

    public void GetOnionID()
    {
        ClothesID = 15;
        ChangePrice();
    }

    public void GetRainBowID()
    {
        ClothesID = 17;
        ChangePrice();
    }

    public void GetVantimID()
    {
        ClothesID = 19;
        ChangePrice();
    }

    public void GetBatmanID()
    {
        ClothesID = 11;
        ChangePrice();
    }

    public void GetPikachuID()
    {
        ClothesID = 16;
        ChangePrice();
    }

    public void GetSkullID()
    {
        ClothesID = 18;
        ChangePrice();
    }

    public void GetShieldID()
    {
        ClothesID = 10;
        ChangePrice();
    }

    public void GetKhienID()
    {
        ClothesID = 9;
        ChangePrice();
    }

    public void GetDevilID()
    {
        ClothesID = 20;
        ChangePrice();
    }

    public void GetAngelID()
    {
        ClothesID = 21;
        ChangePrice();
    }

    public void GetWitchID()
    {
        ClothesID = 22;
        ChangePrice();
    }

    public void GetDeadpoolID()
    {
        ClothesID = 23;
        ChangePrice();
    }

    public void GetThorID()
    {
        ClothesID = 24;
        ChangePrice();
    }
    #endregion

    void ChangePrice()
    {
        for (int i = 0; i < ClothesPrice.Length; i++)
        {
            if(i== ClothesID)
            {
                _priceText.gameObject.GetComponent<TextMeshProUGUI>().text = "" + ClothesPrice[i];
                _Frame[i].gameObject.SetActive(true);
            }
            else
            {
                _Frame[i].gameObject.SetActive(false);
            }
        }
        UpdateButtonState();
    }

    public void BuyClothes()
    {
        if (UIManager.Instance.coinAmount>= ClothesPrice[ClothesID])
        {
            UIManager.Instance.coinAmount -= ClothesPrice[ClothesID];
            ClothesShopInfo.Remove((ClothType)ClothesID);
            ClothesShopInfo.Add((ClothType)ClothesID, ClothState.Select);

            ClothesLockInfo.Remove((ClothType)ClothesID);
            ClothesLockInfo.Add((ClothType)ClothesID, ClothLockState.UnLock);
        }
        UpdateButtonState();
        UpdateCoinAmount();
        UpdateUnLockState();
        PlayerPrefs.SetInt("Score", UIManager.Instance.coinAmount);
        PlayerPrefs.Save();
    }

    public void Equip()
    {
        for (int i = 0; i < ClothesPrice.Length; i++)
        {
            if (ClothesShopInfo[(ClothType)i] == ClothState.Equipped)
            {
                ClothesShopInfo.Remove((ClothType)ClothesID);
                ClothesShopInfo.Add((ClothType)ClothesID, ClothState.Select);
            }
        }
        if (ClothesShopInfo[(ClothType)ClothesID] == ClothState.Select)
        {
            ClothesShopInfo.Remove((ClothType)ClothesID);
            ClothesShopInfo.Add((ClothType)ClothesID, ClothState.Equipped);
            GameObject.FindObjectOfType<PlayerController>().ChangeClothes((PlayerController.clothesType)ClothesID);
        }
        UpdateButtonState();
    }

    void UpdateButtonState()
    {
        if(ClothesShopInfo[(ClothType)ClothesID]== ClothState.CanBuy)
        {
            _CanBuy.gameObject.SetActive(true);
            _Select.gameObject.SetActive(false);
        }
        else if (ClothesShopInfo[(ClothType)ClothesID] == ClothState.Select)
        {
            _CanBuy.gameObject.SetActive(false);
            _Select.gameObject.SetActive(true);
        }
    }

    void UpdateUnLockState()
    {
        for (int i = 0; i < ClothesPrice.Length; i++)
        {
            if(ClothesLockInfo[(ClothType)i]== ClothLockState.UnLock)
            {
                _Lock[i].gameObject.SetActive(false);
            }
        }
    }
    void UpdateCoinAmount()
    {
        _coinAmountText.gameObject.GetComponent<TextMeshProUGUI>().text = "" + UIManager.Instance.coinAmount;
    }
}
