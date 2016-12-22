using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCtrl : MonoBehaviour {
    public static bool isScrolling1P = true;
    public static bool isScrolling2P = true;
    public static float scrollSpeed1P = 3f;
    public static float scrollSpeed2P = 3f;

    public ePlayerSide playerSide = ePlayerSide.None;

    private void Start()
    {
#if UNITY_EDITOR
        if (playerSide == ePlayerSide.None)
            Debug.Log("[ERROR]ScrollCtrl : PlayerSide is None");
#endif
    }

    void Update () {
        if(playerSide == ePlayerSide.Player1)
        {
            if (isScrolling1P)
                transform.Translate(Vector3.down * scrollSpeed1P * Time.deltaTime);
        }
        else if(playerSide == ePlayerSide.Player2)
        {
            if(isScrolling2P)
                transform.Translate(Vector3.down * scrollSpeed2P * Time.deltaTime);
        }
    }
}
