using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour {

    private void Start()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        cardInfo = this.gameObject.GetComponent<CardInfo>();
        bc2d = this.gameObject.GetComponent<BoxCollider2D>();
    }






    Rigidbody2D rb2D;
    SpriteRenderer sr;
    CardInfo cardInfo;
    BoxCollider2D bc2d;

    float nameraka = 0.05f;
    float speed = 50f;



    public void RotateToFront(){
        StartCoroutine("_RotateToFront");
        cardInfo.isFront = true;
    }


    public void RotateToBack()
    {
        StartCoroutine("_RotateToBack");
        cardInfo.isFront = false;
    }





    IEnumerator _RotateToFront(){
        float y = 0;
        bool isFront = false;

        for (int i = 0; i < 100; i++)
        {

            if (y < 90f && !isFront)
            {
                y += speed;
                rb2D.transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
                yield return new WaitForSeconds(nameraka);
            }

            if (y >= 90f && !isFront)
            {
                sr.sprite = cardInfo.frontSprite;
                isFront = true;
            }

            if (y >= 0f && isFront)
            {
                y -= speed;
                rb2D.transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
                yield return new WaitForSeconds(nameraka);
            }

            if (y <= 0f && isFront)
            {
                rb2D.transform.rotation = Quaternion.Euler(new Vector3(0, 0f, 0));
                break;
            }
        }
        yield return null;
    }



    IEnumerator _RotateToBack()
    {
        float y = 0;
        bool isback = false;

        for (int i = 0; i < 100; i++)
        {

            if (y < 90f && !isback)
            {
                y += speed;
                rb2D.transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
                yield return new WaitForSeconds(nameraka);
            }

            if (y >= 90f && !isback)
            {
                sr.sprite = cardInfo.backSprite;
                isback = true;
            }

            if (y >= 0f && isback)
            {
                y -= speed;
                rb2D.transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
                yield return new WaitForSeconds(nameraka);
            }

            if (y <= 0f && isback)
            {
                rb2D.transform.rotation = Quaternion.Euler(new Vector3(0, 0f, 0));
                break;
            }
        }
        yield return null;
    }

 
}
