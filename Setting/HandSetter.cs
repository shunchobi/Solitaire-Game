using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSetter : MonoBehaviour
{

    HandChanger handChanger;


    private void Start()
    {
        handChanger = new HandChanger();
        handChanger.Init();
    }


    public void ChangeToRightHand()
    {
        if (Cash.preference.isRightHand == true) return;

        Cash.preference.isRightHand = !Cash.preference.isRightHand;
        handChanger.ChangePlayHand(Cash.preference.isRightHand);
        SaveManager.SaveFile_Preference();
    }




    public void ChangeToLeftHand()
    {
        if (Cash.preference.isRightHand == false) return;

        Cash.preference.isRightHand = !Cash.preference.isRightHand;
        handChanger.ChangePlayHand(Cash.preference.isRightHand);
        SaveManager.SaveFile_Preference();
    }

}
