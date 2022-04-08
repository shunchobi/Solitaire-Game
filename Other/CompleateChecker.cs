using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleateChecker : MonoBehaviour
{



    public static bool GetIsComplete()
    {
        bool isComplete = false;
        int yamaCardsAmount = 0;

        for(int i = 9; i <= 12; i++)
        {
            yamaCardsAmount = yamaCardsAmount + GameListHolder.gameLists[i].Count;
        }

        if (yamaCardsAmount == 52)
            isComplete = true;
        else
            isComplete = false;

        return isComplete;
    }




}
