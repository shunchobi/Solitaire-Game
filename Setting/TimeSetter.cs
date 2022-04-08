using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSetter : MonoBehaviour
{
    public void ChangeTimeSeting()
    {
        Cash.preference.isTime = !Cash.preference.isTime;
        SaveManager.SaveFile_Preference();
    }
}
