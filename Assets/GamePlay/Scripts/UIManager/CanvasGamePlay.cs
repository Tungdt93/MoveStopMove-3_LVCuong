using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private FloatingJoystick _Joystick;
    [SerializeField] private TextMeshProUGUI aliveAmount;
    [SerializeField] private GameObject guide;
    [SerializeField] private Transform _image;
    private void Awake()
    {
        GameObject.FindObjectOfType<PlayerController>()._Joystick = _Joystick;
    }

    // Update is called once per frame
    void Update()
    {  
        UpdateAliveNumber();
        if (guide.activeSelf)
        {
            if (Input.GetMouseButtonDown(0)) guide.SetActive(false);
        }
        if (GameManager.Instance.gameState == GameManager.GameState.gameOver)
        {
            StartCoroutine(GameOver());
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        guide.SetActive(true);
    }

    public void OpenSetting()
    {
        UIManager.Instance.OpenUI(UIName.Setting);
    }

    public void UpdateAliveNumber()
    {
        aliveAmount.GetComponent<TextMeshProUGUI>().text = "Alive: " + GameManager.Instance.TotalCharAlive;
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        UIManager.Instance.OpenUI(UIName.GameOver);
    }
}
