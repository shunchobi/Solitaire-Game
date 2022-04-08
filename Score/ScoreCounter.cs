using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{


    public static bool countingTime = false;

    private float second;

    public float span = 10f;
    private float countToSpan = 0f;

    // Update is called once per frame
    void Update()
    {
        if (countingTime == true)
        {
            //プレイ時間の計算
            second += Time.deltaTime;
            ScorePoint.time_Second = Mathf.FloorToInt(second);
            if (ScorePoint.time_Second >= 60f){
                second = 0f;
                ScorePoint.time_Second = 0f;
                ScorePoint.time_Minute += 1f;
            }
            ScoreDisplayer.DisplayPlayTime(ScorePoint.time_Minute, ScorePoint.time_Second);


            //時間経過によるスコア減少の計算
            //countToSpan += Time.deltaTime;
            //if (countToSpan > span){
            //    AddScore(ScorePoint.runTime);
            //    countToSpan = 0f;
            //}
        }
    }



    public static void AddMove(){
        ScorePoint.move += 1;
    }


    public static void AddScore(float _score)
    {
        ScorePoint.score += _score;
    }






}
