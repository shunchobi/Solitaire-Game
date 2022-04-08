using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandChanger : MonoBehaviour
{
    
    List<GameObject> deckEmptyObjs;
    List<GameObject> yamaEmptyObjs;


    public void Init()
    {
        deckEmptyObjs = new List<GameObject>();
        yamaEmptyObjs = new List<GameObject>();

        deckEmptyObjs.Add(GameObject.Find("row8"));
        deckEmptyObjs.Add(GameObject.Find("row8_1"));
        deckEmptyObjs.Add(GameObject.Find("row8_2"));
        deckEmptyObjs.Add(GameObject.Find("row8_3"));

        yamaEmptyObjs.Add(GameObject.Find("row9"));
        yamaEmptyObjs.Add(GameObject.Find("row10"));
        yamaEmptyObjs.Add(GameObject.Find("row11"));
        yamaEmptyObjs.Add(GameObject.Find("row12"));

    }




    public void ChangePlayHand(bool isRightHand){
        List<GameObject> deckEmptyObjs = new List<GameObject>();
        List<GameObject> yamaEmptyObjs = new List<GameObject>();

        List<Vector3> newDeckPos = new List<Vector3>();
        List<Vector3> newYamaPos = new List<Vector3>();

        if(isRightHand == true){
            newDeckPos = Cash.cardPos.deck;
            newYamaPos = Cash.cardPos.yama;

            deckEmptyObjs.Add(GameObject.Find("row8"));
            deckEmptyObjs.Add(GameObject.Find("row8_1"));
            deckEmptyObjs.Add(GameObject.Find("row8_2"));
            deckEmptyObjs.Add(GameObject.Find("row8_3"));

            yamaEmptyObjs.Add(GameObject.Find("row9"));
            yamaEmptyObjs.Add(GameObject.Find("row10"));
            yamaEmptyObjs.Add(GameObject.Find("row11"));
            yamaEmptyObjs.Add(GameObject.Find("row12"));

        }
        if(isRightHand == false){
            newDeckPos = Cash.cardPos.deck_Left;
            newYamaPos = Cash.cardPos.yama_Left;

            deckEmptyObjs.Add(GameObject.Find("row8"));
            deckEmptyObjs.Add(GameObject.Find("row8_3"));
            deckEmptyObjs.Add(GameObject.Find("row8_2"));
            deckEmptyObjs.Add(GameObject.Find("row8_1"));

            yamaEmptyObjs.Add(GameObject.Find("row12"));
            yamaEmptyObjs.Add(GameObject.Find("row11"));
            yamaEmptyObjs.Add(GameObject.Find("row10"));
            yamaEmptyObjs.Add(GameObject.Find("row9"));

        }


        for (int i = 0; i < deckEmptyObjs.Count; i++){
            GameObject deckTarget = deckEmptyObjs[i];
            Vector3 newPos = newDeckPos[i];
            deckTarget.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
        }

        for (int i = 0; i < yamaEmptyObjs.Count; i++){
            GameObject yamaTarget = yamaEmptyObjs[i];
            yamaTarget.transform.position = newYamaPos[i];
        }

        if (Cash.isStaredGame == true)
        {
            //deck
            for (int n = 0; n < GameListHolder.gameLists[7].Count; n++)
            {
                GameObject target = GameListHolder.gameLists[7][n];
                target.GetComponent<Mover>().MoveToPosition(newDeckPos[0], Cash.speedDeckToOpenDeck);
            }
            //openDeck
            for (int n = 0; n < GameListHolder.gameLists[8].Count; n++)
            {
                GameObject target = GameListHolder.gameLists[8][n];
                target.GetComponent<Mover>().MoveToPosition(newDeckPos[1], Cash.speedDeckToOpenDeck);
            }
            RuleOpenDeck.MoveOpenDeckCardsOpen();
            //yama
            for (int n = 0; n < 4; n++)
            {
                for (int h = 0; h < GameListHolder.gameLists[9 + n].Count; h++)
                {
                    GameObject target = GameListHolder.gameLists[9 + n][h];
                    if (isRightHand == true)
                        target.GetComponent<Mover>().MoveToPosition(newYamaPos[n], Cash.speedDeckToOpenDeck);
                    else
                        target.GetComponent<Mover>().MoveToPosition(newYamaPos[3 - n], Cash.speedDeckToOpenDeck);

                }
            }
        }
    }

   
}
