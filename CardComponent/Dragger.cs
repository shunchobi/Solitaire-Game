using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour {
    
    Rigidbody2D rb2D;
    CardInfo cardInfo;
    SpriteRenderer sr;

    public Vector3 oridinalPos;


    private void Start()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        cardInfo = this.gameObject.GetComponent<CardInfo>();
        sr = this.gameObject.GetComponent<SpriteRenderer>();

    }


    private void OnMouseDown()
    {
        oridinalPos = this.gameObject.transform.position;
    }






    void OnMouseDrag()
    {
        //retuの裏カードだったらドラッグできないようにする 
        if (cardInfo.isFront == false && cardInfo.place == Cash.retu)
            return;


        Vector3 objectPointInScreen = Camera.main.WorldToScreenPoint(this.rb2D.transform.position);
        Vector3 mousePointInScreen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objectPointInScreen.z);
        Vector3 mousePointInWorld = Camera.main.ScreenToWorldPoint(mousePointInScreen);
        mousePointInWorld.z = this.rb2D.transform.position.z;
        this.rb2D.transform.position = mousePointInWorld;
        sr.sortingOrder = 90;

        //自身の子供たちも一緒に移動してもらう
        if (cardInfo.place == Cash.retu)
        {
            List<GameObject> myList = OwnListReturner.GetList(this.gameObject);
            if (myList.Count - 1 > cardInfo.intInList)
            {
                for (int i = 0; i < myList.Count - cardInfo.intInList - 1; i++)
                {
                    Vector3 draggedCardPos = this.gameObject.transform.position;
                    GameObject myChild = myList[cardInfo.intInList + 1 + i];
                    myChild.GetComponent<SpriteRenderer>().sortingOrder = 91 + i;
                    myChild.GetComponent<Mover>().ChangeThePosition
                           (new Vector3(draggedCardPos.x, draggedCardPos.y - (Cash.cardPos.spaceBtwRetu_Front * (i + 1)), 0));
                }
            }
        }
    }



    public void MoveBackToOridinalPos(){

        this.gameObject.GetComponent<Mover>().
            MoveToPosition(CardPosReturner.GetCardPosFromObj(this.gameObject), Cash.speedToRetuYama);

        //自身の子供たちにも元の場所に戻ってもらう
        if (cardInfo.place == Cash.retu)
        {
            List<GameObject> myList = OwnListReturner.GetList(this.gameObject);
            if (myList.Count - 1 > cardInfo.intInList)
            {
                for (int i = 0; i < myList.Count - cardInfo.intInList - 1; i++)
                {
                    GameObject myChild = myList[cardInfo.intInList + 1 + i];
                    myChild.GetComponent<Dragger>().MoveBackToOridinalPos();
                }
            }
        }
    }


}
