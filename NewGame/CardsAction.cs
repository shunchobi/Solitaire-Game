using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsAction : MonoBehaviour
{


    public static void MoveAllCards(Vector3 pos){
        for (int i = 0; i < GameListHolder.gameLists.Count; i++){
            for (int n = 0; n < GameListHolder.gameLists[i].Count; n++){
                GameObject target = GameListHolder.gameLists[i][n];
                target.GetComponent<Mover>().ChangeThePosition(pos);
            }
        }
    }




}
