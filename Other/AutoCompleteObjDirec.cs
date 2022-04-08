using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCompleteObjDirec : MonoBehaviour
{


    GameObject AutoCompObj;


    public void Initi()
    {
        AutoCompObj = GameObject.Find("AutoComp");
        AutoCompObj.SetActive(false);
    }



    public void UnableAutoCompObj(){
        AutoCompObj.SetActive(false);
    }


    public void AbleAutoCompObj()
    {
        AutoCompObj.SetActive(true);
    }

}
