using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : UICanvas
{
    public void ContinueButton()
    {
        UIManager.Instance.OpenUI(UIName.GamePlay);
    }
    public void HomeButton()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
