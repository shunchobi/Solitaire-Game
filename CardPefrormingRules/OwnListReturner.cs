using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnListReturner : MonoBehaviour {


    /// <summary>
    /// 自身が所属するListを返す。
    /// </summary>
    public static List<GameObject> GetList(GameObject outMe){
        
        List<GameObject> ownList = null;
        string place = outMe.GetComponent<CardInfo>().place;

        //outMeが空オブジェクトの場合
        if(place == Cash.retu_empty || place == Cash.yama_empty){
            switch(outMe.name){
                case "row1":
                    ownList = GameListHolder.gameLists[0];
                    break;
                case "row2":
                    ownList = GameListHolder.gameLists[1];
                    break;
                case "row3":
                    ownList = GameListHolder.gameLists[2];
                    break;
                case "row4":
                    ownList = GameListHolder.gameLists[3];
                    break;
                case "row5":
                    ownList = GameListHolder.gameLists[4];
                    break;
                case "row6":
                    ownList = GameListHolder.gameLists[5];
                    break;
                case "row7":
                    ownList = GameListHolder.gameLists[6];
                    break;
                case "row9":
                    ownList = GameListHolder.gameLists[9];
                    break;
                case "row10":
                    ownList = GameListHolder.gameLists[10];
                    break;
                case "row11":
                    ownList = GameListHolder.gameLists[11];
                    break;
                case "row12":
                    ownList = GameListHolder.gameLists[12];
                    break;    
            }

            return ownList;
        }

        //outMeがカードの場合
        for (int i = 0; i < GameListHolder.gameLists.Count; i++){
            for (int n = 0; n < GameListHolder.gameLists[i].Count; n++){
                GameObject insideMe = GameListHolder.gameLists[i][n];
                if (insideMe == outMe){
                    ownList = GameListHolder.gameLists[i];
                    break;
                }


            }
        }

        return ownList;
    }

}
