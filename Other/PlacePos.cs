using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlacePos : MonoBehaviour
{
    
    public float yGenerateAndDealCardsPos;

    //  float baseScreenHeight = 960f;
    //  float baseScreenWidth = 1136f;

    float bc2d_SizeX = 0.8f;
    float bc2d_SizeY = 1.2f;


    //カードのサイズ情報
    float divisionNumOfCardWidth = 7.2f; //カメラの横サイズに対して何分割分の横サイズのカードにするかを決める値
    float zureRow8s = 30f;
    public static Vector3 scaleCard = new Vector3(); //カメラサイズとpercentageOfCardWidthから割り出された実際のscaleの値が入る
    public float cardWidth; //カードのワールド座標系スケール
    public float cardHeight; //カードのワールド座標系スケール
                             //scanカードのサイズ情報

    public float aRemaningSpaceBtwCards;  //x座標のカード間の余白
    public float upperMenuSize; //上部にあるメニューのHeightの大きさ
    public float yYamaPos;

    // カメラの外枠のスケールをワールド座標系で取得
    public float worldScreenHeight;
    public float worldScreenWidth;

    float intervalBackCardsController = 0.12f;//0.09f;
    float intervalFrontCardsController = 0.28f;//0.25f;
    public float intervalBackCards;
    public float intervalFrontCards;



    void CreatPosObj1to12(){
        GameObject cardPosObj;
        for (int i = 1; i <= 12; i++)
            cardPosObj = new GameObject("row"+i);
    }

    void CreatPosObj8_1to8_3()
    {
        GameObject cardPosObj;
        for (int i = 1; i <= 3; i++)
            cardPosObj = new GameObject("row8_" + i);
    }

    void CreatPosLeftObj8to12()
    {
        GameObject cardPosObj;
        for (int i = 8; i <= 12; i++)
            cardPosObj = new GameObject("row" + i + "_Left");
    }

    void CreatPosLeftObj8_1to8_3()
    {
        GameObject cardPosObj;
        for (int i = 1; i <= 3; i++)
            cardPosObj = new GameObject("row8_" + i + "_Left");
    }



    GameObject row1;
    GameObject row2;
    GameObject row3;
    GameObject row4;
    GameObject row5;
    GameObject row6;
    GameObject row7;
    GameObject row8;
    GameObject row9;
    GameObject row10;
    GameObject row11;
    GameObject row12;
    GameObject row8_1;
    GameObject row8_2;
    GameObject row8_3;

    GameObject row8_Left;
    GameObject row8_1_Left;
    GameObject row8_2_Left;
    GameObject row8_3_Left;
    GameObject row9_Left;
    GameObject row10_Left;
    GameObject row11_Left;
    GameObject row12_Left; //23amount


    public Vector3 row8Pos = new Vector3();
    public Vector3 row8_1Pos = new Vector3();
    public Vector3 row8_2Pos = new Vector3();
    public Vector3 row8_3Pos = new Vector3();
    public Vector3 row8_LeftPos = new Vector3();
    public Vector3 row8_1_LeftPos = new Vector3();
    public Vector3 row8_2_LeftPos = new Vector3();
    public Vector3 row8_3_LeftPos = new Vector3();
    public Vector3 row9Pos = new Vector3();
    public Vector3 row10Pos = new Vector3();
    public Vector3 row11Pos = new Vector3();
    public Vector3 row12Pos = new Vector3();
    public Vector3 row9_LeftPos = new Vector3();
    public Vector3 row10_LeftPos = new Vector3();
    public Vector3 row11_LeftPos = new Vector3();
    public Vector3 row12_LeftPos = new Vector3();



    /// <summary>
    /// 実機のサイズとカメラのサイズを同じにする
    /// </summary>
    public void MakePosAndScale()
    {

        CreatPosObj1to12();
        CreatPosObj8_1to8_3();
        CreatPosLeftObj8to12();
        CreatPosLeftObj8_1to8_3();

        // 開発している画面を元に縦横比取得 (縦画面) iPhone6, 6sサイズ
        float developAspect = 750.0f / 1334.0f;

        // 実機のサイズを取得して、縦横比取得
        float deviceAspect = (float)Screen.width / (float)Screen.height;

        // 実機と開発画面との対比
        float scale = deviceAspect / developAspect;

        Camera mainCamera = Camera.main;

        // カメラに設定していたorthographicSizeを実機との対比でスケール
        float deviceSize = mainCamera.orthographicSize;
        // scaleの逆数
        float deviceScale = 1.0f / scale;
        // orthographicSizeを計算し直す
        mainCamera.orthographicSize = deviceSize * deviceScale;

        // カメラの外枠のスケールをワールド座標系で取得
        worldScreenHeight = Camera.main.orthographicSize * 2f;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;



        row1 = GameObject.Find("row1");
        row2 = GameObject.Find("row2");
        row3 = GameObject.Find("row3");
        row4 = GameObject.Find("row4");
        row5 = GameObject.Find("row5");
        row6 = GameObject.Find("row6");
        row7 = GameObject.Find("row7");
        row8 = GameObject.Find("row8");
        row8_1 = GameObject.Find("row8_1");
        row8_2 = GameObject.Find("row8_2");
        row8_3 = GameObject.Find("row8_3");
        row8_Left = GameObject.Find("row8_Left");
        row8_1_Left = GameObject.Find("row8_1_Left");
        row8_2_Left = GameObject.Find("row8_2_Left");
        row8_3_Left = GameObject.Find("row8_3_Left");
        row9 = GameObject.Find("row9");
        row10 = GameObject.Find("row10");
        row11 = GameObject.Find("row11");
        row12 = GameObject.Find("row12");
        row9_Left = GameObject.Find("row9_Left");
        row10_Left = GameObject.Find("row10_Left");
        row11_Left = GameObject.Find("row11_Left");
        row12_Left = GameObject.Find("row12_Left");


        MakeScaleValue();
        MakeRetuYamaPos();
    }






    /// <summary>
    /// カードのscaleの値をカメラサイズから割り出し、scaleCardに代入
    /// playmatのscaleも割り出す
    /// </summary>
    void MakeScaleValue()
    {
        //GameObject obj = Instantiate (prefab) as GameObject;
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefab/CardPrefab")) as GameObject;

        var color = obj.GetComponent<SpriteRenderer>().color;
        color.a = 0f;
        obj.GetComponent<SpriteRenderer>().color = color;
        obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image/DotCards/1_1");//cash.assetResourceManager.assetBundle.LoadAsset<Sprite>("1_1");

        // スプライトのスケールもワールド座標系で取得 → scale.x.yは同じ値でいいからbounds.size.yは求めない
        float width = obj.GetComponent<SpriteRenderer>().bounds.size.x;

        //カードのscaleとワールド系座標の値を割り出す
        float scaleNum_Card = worldScreenWidth / divisionNumOfCardWidth / width;

        scaleCard = new Vector3(scaleNum_Card, scaleNum_Card, 0);
        obj.transform.localScale = scaleCard;
        cardWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        cardHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y;


        intervalBackCards = cardHeight * intervalBackCardsController;
        intervalFrontCards = cardHeight * intervalFrontCardsController;
        Cash.cardPos.spaceBtwRetu_Back = intervalBackCards;
        Cash.cardPos.spaceBtwRetu_Front = intervalFrontCards;

        Destroy(obj);


        //GameObject playmat_Obj = GameObject.Find("playmat");
        //playmat_Obj.transform.position = new Vector3(0, 0, 0);
        //float playmatWidth = 20.48f; //playmat_Obj.GetComponent<SpriteRenderer>().bounds.size.x;
        //float playmatHeight = 27.32f; //playmat_Obj.GetComponent<SpriteRenderer>().bounds.size.y;
        //float playmatWidthScale = worldScreenWidth / playmatWidth;
        //float playmatHeightScale = worldScreenHeight / playmatHeight;
        //playmat_Obj.transform.localScale = new Vector3(playmatWidthScale, playmatHeightScale, 0);
    }




    public void MakeRetuYamaPos()
    {
        GameObject uiBack = GameObject.Find("Setting");
        Vector3 uiBackPos = Camera.main.ScreenToWorldPoint(uiBack.transform.position);
        float uiBackHeight = uiBack.GetComponent<RectTransform>().rect.height;


        upperMenuSize = worldScreenHeight / 2 - uiBackPos.y + uiBackHeight / 2; //上部にあるメニューのHeightの大きさ
        float everyRemaningSpaceBtwCards = worldScreenWidth - cardWidth * 7; //x座標のカード間の余白の合計
        aRemaningSpaceBtwCards = everyRemaningSpaceBtwCards / 7;
        float yRetuPos = worldScreenHeight / 2 - upperMenuSize - cardHeight / 2 - cardHeight * 1.3f - aRemaningSpaceBtwCards * 5f; //retuのY座標
        yYamaPos = worldScreenHeight / 2 - upperMenuSize - aRemaningSpaceBtwCards * 8f - cardHeight / 2;

        row1.transform.position = new Vector3(0 - aRemaningSpaceBtwCards * 3 - cardWidth * 3, yRetuPos, 10f);
        row1.transform.localScale = scaleCard;
        SetBoxColliderToObj(row1);
        SetPlaceName(row1, Cash.retu_empty, 0);
        Cash.cardPos.retu.Add(new Vector3(0 - aRemaningSpaceBtwCards * 3 - cardWidth * 3, yRetuPos, 0));

        row2.transform.position = new Vector3(0 - aRemaningSpaceBtwCards * 2 - cardWidth * 2, yRetuPos, 10f);
        row2.transform.localScale = scaleCard;
        SetBoxColliderToObj(row2);
        SetPlaceName(row2, Cash.retu_empty, 1);
        Cash.cardPos.retu.Add(new Vector3(0 - aRemaningSpaceBtwCards * 2 - cardWidth * 2, yRetuPos, 0));

        row3.transform.position = new Vector3(0 - aRemaningSpaceBtwCards - cardWidth, yRetuPos, 10f);
        row3.transform.localScale = scaleCard;
        SetBoxColliderToObj(row3);
        SetPlaceName(row3, Cash.retu_empty, 2);
        Cash.cardPos.retu.Add(new Vector3(0 - aRemaningSpaceBtwCards - cardWidth, yRetuPos, 0));

        row4.transform.position = new Vector3(0, yRetuPos, 10f);
        row4.transform.localScale = scaleCard;
        SetBoxColliderToObj(row4);
        SetPlaceName(row4, Cash.retu_empty, 3);
        Cash.cardPos.retu.Add(new Vector3(0, yRetuPos, 0));

        row5.transform.position = new Vector3(0 + aRemaningSpaceBtwCards + cardWidth, yRetuPos, 10f);
        row5.transform.localScale = scaleCard;
        SetBoxColliderToObj(row5);
        SetPlaceName(row5, Cash.retu_empty, 4);
        Cash.cardPos.retu.Add(new Vector3(0 + aRemaningSpaceBtwCards + cardWidth, yRetuPos, 0));

        row6.transform.position = new Vector3(0 + aRemaningSpaceBtwCards * 2 + cardWidth * 2, yRetuPos, 10f);
        row6.transform.localScale = scaleCard;
        SetBoxColliderToObj(row6);
        SetPlaceName(row6, Cash.retu_empty, 5);
        Cash.cardPos.retu.Add(new Vector3(0 + aRemaningSpaceBtwCards * 2 + cardWidth * 2, yRetuPos, 0));

        row7.transform.position = new Vector3(0 + aRemaningSpaceBtwCards * 3 + cardWidth * 3, yRetuPos, 10f);
        row7.transform.localScale = scaleCard;
        SetBoxColliderToObj(row7);
        SetPlaceName(row7, Cash.retu_empty, 6);
        Cash.cardPos.retu.Add(new Vector3(0 + aRemaningSpaceBtwCards * 3 + cardWidth * 3, yRetuPos, 0));

        row8.transform.position = new Vector3(0 + aRemaningSpaceBtwCards * 3 + cardWidth * 3, yYamaPos, 10f);
        row8Pos = row8.transform.position;
        row8.transform.localScale = scaleCard;
        SetBoxColliderToObj(row8);
        row8.AddComponent<RuleDeck>();
        SetPlaceName(row8, Cash.retu_empty, 7);
        Cash.cardPos.deck.Add(new Vector3(0 + aRemaningSpaceBtwCards * 3 + cardWidth * 3, yYamaPos, 10f));

        row8_1.transform.position = new Vector3(0 + aRemaningSpaceBtwCards + cardWidth * 2 - zureRow8s * 2, yYamaPos, 0);
        row8_1Pos = row8_1.transform.position;
        row8_1.transform.localScale = scaleCard;
        Cash.cardPos.deck.Add(new Vector3(0 + aRemaningSpaceBtwCards + cardWidth * 2 - zureRow8s * 2, yYamaPos, 0));

        row8_2.transform.position = new Vector3(0 + aRemaningSpaceBtwCards + cardWidth * 2 - zureRow8s, yYamaPos, 0);
        row8_2Pos = row8_2.transform.position;
        row8_2.transform.localScale = scaleCard;
        Cash.cardPos.deck.Add(new Vector3(0 + aRemaningSpaceBtwCards + cardWidth * 2 - zureRow8s, yYamaPos, 0));

        row8_3.transform.position = new Vector3(0 + aRemaningSpaceBtwCards + cardWidth * 2, yYamaPos, 0);
        row8_3Pos = row8_3.transform.position;
        row8_3.transform.localScale = scaleCard;
        Cash.cardPos.deck.Add(new Vector3(0 + aRemaningSpaceBtwCards + cardWidth * 2, yYamaPos, 0));

        row8_Left.transform.position = new Vector3(0 - aRemaningSpaceBtwCards * 3 - cardWidth * 3, yYamaPos, 0);
        row8_LeftPos = row8_Left.transform.position;
        row8_Left.transform.localScale = scaleCard;
        Cash.cardPos.deck_Left.Add(new Vector3(0 - aRemaningSpaceBtwCards * 3 - cardWidth * 3, yYamaPos, 0));

        row8_1_Left.transform.position = new Vector3(0 - aRemaningSpaceBtwCards - cardWidth * 2, yYamaPos, 0);
        row8_1_LeftPos = row8_1_Left.transform.position;
        row8_1_Left.transform.localScale = scaleCard;
        Cash.cardPos.deck_Left.Add(new Vector3(0 - aRemaningSpaceBtwCards - cardWidth * 2, yYamaPos, 0));

        row8_2_Left.transform.position = new Vector3(0 - aRemaningSpaceBtwCards - cardWidth * 2 + zureRow8s, yYamaPos, 0);
        row8_2_LeftPos = row8_2_Left.transform.position;
        row8_2_Left.transform.localScale = scaleCard;
        Cash.cardPos.deck_Left.Add(new Vector3(0 - aRemaningSpaceBtwCards - cardWidth * 2 + zureRow8s, yYamaPos, 0));

        row8_3_Left.transform.position = new Vector3(0 - aRemaningSpaceBtwCards - cardWidth * 2 + zureRow8s * 2, yYamaPos, 0);
        row8_3_LeftPos = row8_3_Left.transform.position;
        row8_3_Left.transform.localScale = scaleCard;
        Cash.cardPos.deck_Left.Add(new Vector3(0 - aRemaningSpaceBtwCards - cardWidth * 2 + zureRow8s * 2, yYamaPos, 0));

        row9.transform.position = new Vector3(0, yYamaPos, 10f);
        row9Pos = row9.transform.position;
        row9.transform.localScale = scaleCard;
        SetBoxColliderToObj(row9);
        SetPlaceName(row9, Cash.yama_empty, 9);
        Cash.cardPos.yama.Add(new Vector3(0, yYamaPos, 10f));

        row10.transform.position = new Vector3(0 - aRemaningSpaceBtwCards - cardWidth, yYamaPos, 10f);
        row10Pos = row10.transform.position;
        row10.transform.localScale = scaleCard;
        SetBoxColliderToObj(row10);
        SetPlaceName(row10, Cash.yama_empty, 10);
        Cash.cardPos.yama.Add(new Vector3(0 - aRemaningSpaceBtwCards - cardWidth, yYamaPos, 10f));

        row11.transform.position = new Vector3(0 - aRemaningSpaceBtwCards * 2 - cardWidth * 2, yYamaPos, 10f);
        row11Pos = row11.transform.position;
        row11.transform.localScale = scaleCard;
        SetBoxColliderToObj(row11);
        SetPlaceName(row11, Cash.yama_empty, 11);
        Cash.cardPos.yama.Add(new Vector3(0 - aRemaningSpaceBtwCards * 2 - cardWidth * 2, yYamaPos, 10f));

        row12.transform.position = new Vector3(0 - aRemaningSpaceBtwCards * 3 - cardWidth * 3, yYamaPos, 10f);
        row12Pos = row12.transform.position;
        row12.transform.localScale = scaleCard;
        SetBoxColliderToObj(row12);
        SetPlaceName(row12, Cash.yama_empty, 12);
        Cash.cardPos.yama.Add(new Vector3(0 - aRemaningSpaceBtwCards * 3 - cardWidth * 3, yYamaPos, 10f));

        row9_Left.transform.position = new Vector3(0 + aRemaningSpaceBtwCards * 3 + cardWidth * 3, yYamaPos, 10f);
        row9_LeftPos = row9_Left.transform.position;
        Cash.cardPos.yama_Left.Add(new Vector3(0 + aRemaningSpaceBtwCards * 3 + cardWidth * 3, yYamaPos, 10f));

        row10_Left.transform.position = new Vector3(0 + aRemaningSpaceBtwCards * 2 + cardWidth * 2, yYamaPos, 10f);
        row10_LeftPos = row10_Left.transform.position;
        Cash.cardPos.yama_Left.Add(new Vector3(0 + aRemaningSpaceBtwCards * 2 + cardWidth * 2, yYamaPos, 10f));

        row11_Left.transform.position = new Vector3(0 + aRemaningSpaceBtwCards + cardWidth, yYamaPos, 10f);
        row11_LeftPos = row11_Left.transform.position;
        Cash.cardPos.yama_Left.Add(new Vector3(0 + aRemaningSpaceBtwCards + cardWidth, yYamaPos, 10f));

        row12_Left.transform.position = new Vector3(0, yYamaPos, 10f);
        row12_LeftPos = row12_Left.transform.position;
        Cash.cardPos.yama_Left.Add(new Vector3(0, yYamaPos, 10f));

    }


    void SetBoxColliderToObj(GameObject obj){
        obj.AddComponent<BoxCollider2D>();
        BoxCollider2D boxCollider2D = obj.GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
        boxCollider2D.enabled = false;
        boxCollider2D.size = new Vector2(bc2d_SizeX, bc2d_SizeY);
    }



    void SetPlaceName(GameObject obj, string place, int _placeListInt){
        obj.AddComponent<CardInfo>();
        obj.GetComponent<CardInfo>().place = place;
        obj.GetComponent<CardInfo>().placeListInt = _placeListInt;
    }


}
