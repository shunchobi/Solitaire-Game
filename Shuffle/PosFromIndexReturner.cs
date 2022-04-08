using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosFromIndexReturner : MonoBehaviour
{



    public static Vector3 GetPosFromIndex(int listIntNum, int numInList)
    {
        GameObject emptyObj = EmptyObjectReturner.GetEmptyObj(listIntNum + 1);
        Vector3 pos = emptyObj.transform.position;
        float yPos = 0f;

        if (listIntNum <= 6)
            yPos = pos.y - (numInList * Cash.cardPos.spaceBtwRetu_Back);
        else
            yPos = pos.y;
        
        return new Vector3(pos.x, yPos, 0f);
    }
}
