using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{

    public static AutoCompleteObjDirec autoCompleteObjDirec;





    // Start is called before the first frame update
    public static void Init()
    {
        autoCompleteObjDirec = new AutoCompleteObjDirec();
        autoCompleteObjDirec.Initi();
    }
}
