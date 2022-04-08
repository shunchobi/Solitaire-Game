using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenuOpener : MonoBehaviour
{


    GameObject settingPanel;



    // Start is called before the first frame update
    void Start()
    {
        settingPanel = GameObject.Find("SettingPanel");
        settingPanel.SetActive(false);
    }



    public void OpenSettingMenu(){
        settingPanel.SetActive(true);
        CardsUntouchabler.UntouchableAllCards();

    }


}
