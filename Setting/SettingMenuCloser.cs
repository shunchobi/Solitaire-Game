using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenuCloser : MonoBehaviour
{


    GameObject settingPanel;



    // Start is called before the first frame update
    void Start()
    {
        settingPanel = GameObject.Find("SettingPanel");
    }



    public void CloseSettingMenu()
    {
        settingPanel.SetActive(false);
        CardsUntouchabler.TouchableAllCards();
    }
}
