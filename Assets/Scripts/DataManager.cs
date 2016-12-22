using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

    static DataManager instance;
    public static DataManager Instance
    {
        get { return instance; }
    }

    TileManager tileManagerP1;
    public TileManager TIleManagerP1
    {
        get { return tileManagerP1; }
    }

    TileManager tileManagerP2;
    public TileManager TIleManagerP2
    {
        get { return tileManagerP2; }
    }

    public GameObject player1Origin;
    public GameObject player1Canvas;
    public PlayerCtrl player1;

    public GameObject player2Origin;
    public GameObject player2Canvas;
    public PlayerCtrl player2;

    public GameObject player1RootTileSets;
    public GameObject player2RootTileSets;

    public int tileSetCnt = 2;
    public int preloadTileCnt = 8;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("DataManager가 하나 이상 입니다");

        instance = this;
    }

    // Use this for initialization
    void Start () {
        tileManagerP1 = player1Origin.transform.FindChild("TileManager").GetComponent<TileManager>();
        tileManagerP2 = player2Origin.transform.FindChild("TileManager").GetComponent<TileManager>();
        player1 = player1Origin.transform.FindChild("Player").GetComponent<PlayerCtrl>();
        player2 = player2Origin.transform.FindChild("Player").GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
