using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBacker : MonoBehaviour
{



    public void BackToHome()
    {
        UiManager.ClearUi(false);
        UiManager.HomeUi(true);
    }



}
