using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintCardsDisplayer : MonoBehaviour
{


    GameObject childHintCard;
    GameObject oyaHintCard;

    bool isHintCardsFound = false;



    public void DisplayHintCards(){
        FindHintCards();
        ActionHintCards();
    }




    void ActionHintCards(){
        if (isHintCardsFound){
            Debug.Log("childHintCard = " + childHintCard.GetComponent<CardInfo>().cardNum + "_" + childHintCard.GetComponent<CardInfo>().suit);
            Debug.Log("oyaHintCard = " + oyaHintCard.GetComponent<CardInfo>().cardNum + "_" + oyaHintCard.GetComponent<CardInfo>().suit);
        }
        else{
            Debug.Log("no hint cards");
        }

        isHintCardsFound = false;
    }





    void FindHintCards(){

        bool _isHintCardsFound = false;
        GameObject _childHintCard = null;
        GameObject _oyaHintCard = null;


        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //各リストの最後のカードがyamaに行けるか調べる(yamaにいけるか)
        for (int i = 9; i <= 12; i++)
        {
            if (GameListHolder.gameLists[i].Count > 0)
                _oyaHintCard = GameListHolder.gameLists[i][GameListHolder.gameLists[i].Count - 1];
            else
                _oyaHintCard = EmptyObjectReturner.GetEmptyObj(i);
            
            for (int n = 0; n <= 8; n++){
                if (n == 7) continue;
                if (GameListHolder.gameLists[n].Count > 0)
                {
                    _childHintCard = GameListHolder.gameLists[n][GameListHolder.gameLists[n].Count - 1];
                    _isHintCardsFound = RuleYama.CheckAcceptability(_childHintCard, _oyaHintCard);
                    if (_isHintCardsFound == true)
                    {
                        childHintCard = _childHintCard;
                        oyaHintCard = _oyaHintCard;
                        isHintCardsFound = _isHintCardsFound;
                        return;
                    }
                    else
                        continue;
                }
                else continue;
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////






        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //各retuの裏カードの下にあるカードが他のretuに移動できるか調べる(retuをめくれるか)
        for (int i = 0; i <= 6; i++)
        {
            if (GameListHolder.gameLists[i].Count > 0)
                _oyaHintCard = GameListHolder.gameLists[i][GameListHolder.gameLists[i].Count - 1];
            else
                _oyaHintCard = EmptyObjectReturner.GetEmptyObj(i+1);
            
            for (int n = 0; n <= 8; n++)
            {
                if (GameListHolder.gameLists[n].Count == 0) continue;
                if (i == n) continue;
                if (n == 7) continue;

                if (GameListHolder.gameLists[n].Count > 0)
                {
                    //retuで裏カードの下のカードを探す。
                    if (n != 8){
                        for (int h = 0; h < GameListHolder.gameLists[n].Count; h++)
                        {
                            GameObject target = GameListHolder.gameLists[n][h];
                            bool isFront = target.GetComponent<CardInfo>().isFront;
                            if (isFront)
                            {
                                _childHintCard = target;
                                break;
                            }
                        }
                    }
                    else if(n == 8)
                        _childHintCard = GameListHolder.gameLists[n][GameListHolder.gameLists[n].Count - 1];

                    _isHintCardsFound = RuleRetu.CheckAcceptability(_childHintCard, _oyaHintCard, false);
                    if (_isHintCardsFound == true){
                        childHintCard = _childHintCard;
                        oyaHintCard = _oyaHintCard;
                        isHintCardsFound = _isHintCardsFound;
                        return;
                    }
                    else
                        continue;
                }
                else continue;
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////





        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //deckかopenDeckはめくれるか
        if(GameListHolder.gameLists[7].Count > 0 ||
           GameListHolder.gameLists[8].Count > 0){

            isHintCardsFound = true;
            childHintCard = EmptyObjectReturner.GetEmptyObj(8);
            oyaHintCard = EmptyObjectReturner.GetEmptyObj(8);
            return;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////





        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //retuの表と表の間のカードが他のretuへ移動できるか調べる

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
    }




}
