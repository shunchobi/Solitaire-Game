using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    Rigidbody2D rb2D;
    CardInfo cardInfo;
    SpriteRenderer sr;



    private void Start()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        cardInfo = this.gameObject.GetComponent<CardInfo>();
        sr = this.gameObject.GetComponent<SpriteRenderer>();
    }



    public void ChangeThePosition(Vector3 endPos){
        rb2D.transform.position = endPos;
    }



    public void MoveToPosition(Vector3 _endPos, float _timeToArrive)
    {
        endPos = _endPos;
        timeToArrive = _timeToArrive;
        isMoving = true;
    }


    bool isMoving = false;
    Vector3 endPos = Vector3.zero;
    float timeToArrive = 0;
    float elapsedTime = 0f;


    private void Update()
    {
        if (isMoving == true)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / timeToArrive;
            if (t > 1.0f)
                t = 1.0f;
            float rate = t * t * (3.0f - 2.0f * t);
            transform.position = transform.position * (1.0f - rate) + endPos * rate;

            if(cardInfo.place == Cash.retu || cardInfo.place == Cash.yama)
                sr.sortingOrder = 200 + cardInfo.intInList;
            else if(cardInfo.place == Cash.opendDeck)
                sr.sortingOrder = 100 + cardInfo.intInList;

            if (elapsedTime >= timeToArrive)
            {
                transform.position = endPos;
                elapsedTime = 0f;
                sr.sortingOrder = cardInfo.intInList;
                isMoving = false;
            }
        }
    }


}
