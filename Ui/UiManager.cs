using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{


    static UiMover start;
    static UiMover hint;
    static UiMover undo;
    static UiMover setting;
    static UiMover replay;
    static UiMover newGame;
    static UiMover shuffle;
    static UiMover random;
    static UiMover gameMe;
    static UiMover design;
    static UiMover rate;
    static UiMover home;
    static UiMover presentCoin;
    static UiMover movieCoin;
    static UiMover completeCoin;



    public static bool isMovedCard = false;


    // Start is called before the first frame update
    public static void Init()
    {
        start = GameObject.Find("Start").GetComponent<UiMover>();
        hint = GameObject.Find("Hint").GetComponent<UiMover>();
        undo = GameObject.Find("Undo").GetComponent<UiMover>();
        setting = GameObject.Find("Setting").GetComponent<UiMover>();
        replay = GameObject.Find("Replay").GetComponent<UiMover>();
        newGame = GameObject.Find("NewGame").GetComponent<UiMover>();
        shuffle = GameObject.Find("Shuffle").GetComponent<UiMover>();
        random = GameObject.Find("Random").GetComponent<UiMover>();
        gameMe = GameObject.Find("GameMe").GetComponent<UiMover>();
        design = GameObject.Find("Design").GetComponent<UiMover>();
        rate = GameObject.Find("Rate").GetComponent<UiMover>();
        home = GameObject.Find("Home").GetComponent<UiMover>();
        presentCoin = GameObject.Find("PresentCoin").GetComponent<UiMover>();
        movieCoin = GameObject.Find("MovieCoin").GetComponent<UiMover>();
        completeCoin = GameObject.Find("CompleteCoin").GetComponent<UiMover>();
    }




    public static void HomeUi(bool isIn)
    {
        if (isIn)
        {
            gameMe.MoveToInPosition();
            start.MoveToInPosition();
            design.MoveToInPosition();
            rate.MoveToInPosition();
        }

        if (!isIn)
        {
            gameMe.MoveToOutPosition();
            start.MoveToOutPosition();
            design.MoveToOutPosition();
            rate.MoveToOutPosition();
        }
    }



    public static void GetPlayUi(bool isIn)
    {

        if (isIn)
            random.MoveToInPosition();
        if (!isIn)
            random.MoveToOutPosition();

    }





    public static void PlayUi(bool isIn)
    {

        if (isIn)
        {
            replay.MoveToInPosition();
            newGame.MoveToInPosition();
            shuffle.MoveToInPosition();
            undo.MoveToInPosition();
        }

        if (!isIn)
        {
            replay.MoveToOutPosition();
            newGame.MoveToOutPosition();
            shuffle.MoveToOutPosition();
            undo.MoveToOutPosition();
        }

    }



    public static void ClearUi(bool isIn)
    {
        if (isIn)
        {
            newGame.MoveToInPosition();
            home.MoveToInPosition();
            presentCoin.SlideToInPosition();
            movieCoin.SlideToInPosition();
            completeCoin.SlideToInPosition();

        }

        if (!isIn)
        {
            newGame.MoveToOutPosition();
            home.MoveToOutPosition();
            presentCoin.SlideToOutPosition();
            movieCoin.SlideToOutPosition();
            completeCoin.SlideToOutPosition();

        }

    }



}
