using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCanvas : MonoBehaviour
{
    [SerializeField] private Transform Character;
    [SerializeField] private EnemyRandomSkin _enemySkin;
    [SerializeField] private Image CharacterLevelBG;
    [SerializeField] private TextMeshProUGUI CharacterLevelText;
    [SerializeField] private TextMeshProUGUI CharacterName;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (Character.gameObject.CompareTag("Player"))
        {
            if (Character.gameObject.GetComponent<PlayerController>().IsDeath == true) gameObject.GetComponent<Canvas>().enabled = false;
            else gameObject.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            if (Character.gameObject.GetComponent<EnemyController>().IsDeath == true) gameObject.GetComponent<Canvas>().enabled = false;
            else gameObject.GetComponent<Canvas>().enabled = true;
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(new Vector3(GameManager.Instance.mainCamera.transform.position.x, GameManager.Instance.mainCamera.transform.position.y, Character.transform.position.z));
        transform.localScale =new Vector3(1/Character.localScale.x, 1 / Character.localScale.y, 1 / Character.localScale.z);

        if (Character.gameObject.CompareTag("Player"))
        {
            CharacterName.gameObject.GetComponent<TextMeshProUGUI>().text = "You";
            CharacterName.gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;
            CharacterLevelText.gameObject.GetComponent<TextMeshProUGUI>().text = "" + Character.GetComponent<PlayerController>().Level;
            CharacterLevelBG.color = Color.black;
        }
        else
        {
            CharacterName.gameObject.GetComponent<TextMeshProUGUI>().text = "" + Character.GetComponent<EnemyController>().enemyName;
            CharacterName.gameObject.GetComponent<TextMeshProUGUI>().color = _enemySkin.EnemyColor[Character.GetComponent<EnemyController>().EnemySkinID].color;
            CharacterLevelText.gameObject.GetComponent<TextMeshProUGUI>().text = "" + Character.GetComponent<EnemyController>().Level;
            CharacterLevelBG.color = _enemySkin.EnemyColor[Character.GetComponent<EnemyController>().EnemySkinID].color;

        }
        
    }
}
