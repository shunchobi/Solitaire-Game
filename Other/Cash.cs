using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 static class Cash {

    public static string retu = "retu";
    public static string yama = "yama";
    public static string deck = "deck";
    public static string opendDeck = "openedDeck";
    public static string retu_empty = "retu_empty";
    public static string yama_empty = "yama_empty";


    public static string black = "black";
    public static string red = "red";


    public static float speedDeckToOpenDeck = 0.15f;
    public static float speedToRetuYama = 0.3f;
    public static float speedToRandom = 1f;


    public static bool isStaredGame = false;


    public static CardPos cardPos;
    public static Preference preference;



    public static void Init(){
        cardPos = new CardPos();
        preference = SaveManager.LoadFile_Preference();
    }
}
