using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BC2DSizeChanger : MonoBehaviour
{

    BoxCollider2D bxc2d;

    float yVisibleSize;
    float yFullSize;
    float yOffSet = 0.45f;


    private void Start()
    {
        bxc2d = this.gameObject.GetComponent<BoxCollider2D>();

        yFullSize = bxc2d.size.y;
        yVisibleSize = bxc2d.size.y * Cash.cardPos.spaceBtwRetu_Front * 0.008f;
    }




    public void ChangeBxcSizeToFull(){
        bxc2d.size = new Vector2(bxc2d.size.x, yFullSize);

        bxc2d.offset = Vector2.zero;
    }


    public void ChangeBxcSizeToVisible()
    {
        bxc2d.size = new Vector2(bxc2d.size.x, yVisibleSize); 
        bxc2d.offset = new Vector2(0, yOffSet);

    }



}
