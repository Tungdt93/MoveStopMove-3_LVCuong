using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasGameOver : UICanvas
{
    [SerializeField] private Transform GuideText;
    [SerializeField] private TextMeshProUGUI RankText;
    [SerializeField] private TextMeshProUGUI KillerName;
    [SerializeField] private TextMeshProUGUI CoinAmount;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& GuideText.gameObject.activeSelf)
        {
            GuideText.gameObject.SetActive(false);
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        StartCoroutine(ShowGuide());
        RankText.GetComponent<TextMeshProUGUI>().text = "#" + GameManager.Instance.TotalCharAlive;
        KillerName.GetComponent<TextMeshProUGUI>().text = ""+FindObjectOfType<PlayerController>().KillerName;
        CoinAmount.GetComponent<TextMeshProUGUI>().text ="" + FindObjectOfType<PlayerController>().Level;
        UIManager.Instance.coinAmount+= FindObjectOfType<PlayerController>().Level;
        PlayerPrefs.SetInt("Score", UIManager.Instance.coinAmount);
        PlayerPrefs.Save();
    }
    IEnumerator ShowGuide()
    {
        yield return new WaitForSeconds(3);
        GuideText.gameObject.SetActive(true);
    }

}
