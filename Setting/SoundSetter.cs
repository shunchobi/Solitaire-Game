using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSetter : MonoBehaviour
{


    public void ChangeSoundSeting(){
        Cash.preference.isSound = !Cash.preference.isSound;
        SaveManager.SaveFile_Preference();
    }


}
