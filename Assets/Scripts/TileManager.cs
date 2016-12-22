using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public ePlayerSide playerSide = ePlayerSide.None;

    DataManager dataManager;
    GameObject rootTileSets;

    public Transform lastTileTransform = null;

    public GameObject[] tileSetsObjects;
    public GameObject[] prefabTileSets;

    int tileSetCnt = -1;

    //-2.6 ~ 2.7간격 7개 

    //-2  / -.65 / 0.7 / 2.05

    //y = 2.7

        //if y == -8 reset
	// Use this for initialization
	void Start () {
        //캐쉬 및 초기화
        dataManager = DataManager.Instance;

        tileSetCnt = dataManager.tileSetCnt;

#if UNITY_EDITOR
        if (playerSide == ePlayerSide.None)
        {
            Debug.Log("[ERROR]TileManager : playerSide가 None입니다");
        }
        if(tileSetCnt == -1)
        {
            Debug.Log("[ERROR]TileManager : tileSetCnt가 초기화 되지 않았습니다.");
        }
#endif

        tileSetsObjects = new GameObject[tileSetCnt];
        prefabTileSets = new GameObject[tileSetCnt];

        rootTileSets = playerSide == ePlayerSide.Player1 ? dataManager.player1RootTileSets : dataManager.player2RootTileSets;
        /////////////////////



        //타일셋 설정
        {
            for (int i = 0; i < tileSetCnt; i++)
            {
                tileSetsObjects[i] = rootTileSets.transform.FindChild(string.Format("TileSets{0}", i)).gameObject;
                prefabTileSets[i] = Resources.Load(string.Format("Prefabs/TileSet/TileSet{0}", i)) as GameObject;

#if UNITY_EDITOR
                if (tileSetsObjects[i] == null)
                {
                    Debug.Log(string.Format("TileSets{0} 를 찾을 수 없습니다.", i));
                }
                if (prefabTileSets[i] == null)
                {
                    Debug.Log(string.Format("Prefabs/TileSet/TileSet{0} 을 찾을 수 없습니다.", i));
                }
#endif
            }
        }
        /////////////////////////

        /*
        //셈플 타일셋 삭제
        {
            for (int i = 0; i < tileSetCnt; i++)
            {
                Debug.Log(tileSetsObjects[i].transform.GetChild(0));
                Destroy(tileSetsObjects[i].transform.GetChild(0));
            }
        }
        //////////////////////////
        */

        //타일셋 프리로드
        {
            GameObject tileSet = null;
            TileSetCtrl tileSetCtrl = null;
            ScrollCtrl scrollCtrl = null;
            for (int i = 0; i < tileSetCnt; i++)
            {
                for (int j = 0; j < dataManager.preloadTileCnt; j++)
                {
                    tileSet = Instantiate(prefabTileSets[i], transform.position, Quaternion.identity);
                    tileSet.transform.parent = tileSetsObjects[i].transform;
                    tileSetCtrl = tileSet.GetComponent<TileSetCtrl>();
                    tileSetCtrl.EventDisableTile += OnDisableTile;
                    scrollCtrl = tileSet.GetComponent<ScrollCtrl>();
                    scrollCtrl.playerSide = playerSide;
                    tileSet.SetActive(false);
                }
            }
        }
        ////////////////////////

        //초기 바닥 설정
        {
            Transform tileSet = null;
            //0.1 ~ 2.7씩증가 기본배치
            for (int i = 0; i < dataManager.preloadTileCnt; i++)
            {
                tileSet = tileSetsObjects[0].transform.GetChild(i);
                tileSet.localPosition = new Vector3(0, -2.6f + (2.7f * i), 0f);
                tileSet.gameObject.SetActive(true);
            }
            lastTileTransform = tileSetsObjects[0].transform.GetChild(dataManager.preloadTileCnt-1);
        }


        //게임시작을 가정

    }
	
    void OnDisableTile(TileSetCtrl sender)
    {
        if(GameManager.Instance.isPlaying)
            EnableRandomTileSet(sender);
    }

	// Update is called once per frame
	void Update () {
		
	}

    void EnableRandomTileSet(TileSetCtrl sender)
    {
        int tileSetNum = Random.Range(1, tileSetCnt);
        Transform tileSetsOjbectTr = tileSetsObjects[tileSetNum].transform;
        GameObject tileSet = null;
        TileSetCtrl tileSetCtrl = null;
        Transform lastTileSet = null;
        lastTileSet = lastTileTransform;
        for (int i = 0; i < tileSetsOjbectTr.childCount; i++)
        {
            tileSet = tileSetsOjbectTr.GetChild(i).gameObject;
            tileSetCtrl = tileSet.GetComponent<TileSetCtrl>();
            if (!tileSet.activeSelf && tileSetCtrl.TileID != sender.TileID)
            {
                tileSet.SetActive(true);
                tileSet.transform.localPosition = lastTileSet.localPosition + new Vector3(0f, 2.65f, 0f);
                lastTileSet = tileSet.transform;
                break;
            }
        }
        lastTileTransform = lastTileSet;
    }
}
