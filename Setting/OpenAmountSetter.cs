using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAmountSetter : MonoBehaviour
{


    public void ChangeToOneOpen(){
        if (Cash.preference.isOneCardOpen == true) return;

        Cash.preference.isOneCardOpen = !Cash.preference.isOneCardOpen;
        SaveManager.SaveFile_Preference();
    }




    public void ChangeToThreeOpen()
    {
        if (Cash.preference.isOneCardOpen == false) return;

        Cash.preference.isOneCardOpen = !Cash.preference.isOneCardOpen;
        SaveManager.SaveFile_Preference();
    }

}
