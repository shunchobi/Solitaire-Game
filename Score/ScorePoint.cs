using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    
    //Move
    public static float move = 0;

    //Score
    public static float score = 0f;
    public static float toYama = 10f;
    public static float backFromYama = -15f;
    public static float fromDeck = 5f;
    public static float flipedRetu = 5f;
    //public static float usedAllDeck_One = -100f; //1巡後、1巡ごとに
    //public static float usedAllDeck_Three = -20f; //3巡後は、1巡ごとに
    //public static float runTime = -2f; //10秒経過ごとに
    //public static float usedUndo = -7f; //一回元に戻すと
    public static float bounusScore = 700000f; //ゲーム終了までにかかった総時間数 (秒) で割って計算

    //Time
    public static float time_Second = 0;
    public static float time_Minute = 0;

}
