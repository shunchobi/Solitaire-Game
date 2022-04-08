using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMover : MonoBehaviour
{



    float inYPos = -580f;
    float outYPos = -1000f;

    RectTransform rectTransform;


    bool isMoving = false;
    Vector3 endPos = Vector3.zero;
    float timeToArrive = 0.5f;
    float elapsedTime = 0f;




    public void MoveToInPosition()
    {
        rectTransform = this.gameObject.GetComponent<RectTransform>();

        endPos = new Vector3(rectTransform.localPosition.x, inYPos, 0f);
        isMoving = true;
    }



    public void MoveToOutPosition()
    {
        rectTransform = this.gameObject.GetComponent<RectTransform>();

        endPos = new Vector3(rectTransform.localPosition.x, outYPos, 0f);
        isMoving = true;
    }



    public void SlideToInPosition()
    {
        rectTransform = this.gameObject.GetComponent<RectTransform>();

        endPos = new Vector3(0f, rectTransform.localPosition.y, 0f);
        isMoving = true;
    }



    public void SlideToOutPosition()
    {
        rectTransform = this.gameObject.GetComponent<RectTransform>();

        endPos = new Vector3(1000f, rectTransform.localPosition.y, 0f);
        isMoving = true;
    }





    private void Update()
    {
        if (isMoving == true)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / timeToArrive;
            if (t > 1.0f)
                t = 1.0f;
            float rate = t * t * (3.0f - 2.0f * t);
            rectTransform.localPosition = rectTransform.localPosition * (1.0f - rate) + endPos * rate;

            if (elapsedTime >= timeToArrive)
            {
                rectTransform.localPosition = endPos;
                elapsedTime = 0f;
                isMoving = false;
            }
        }
    }


}
