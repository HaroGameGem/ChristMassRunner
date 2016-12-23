using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileSetCtrl : MonoBehaviour {
    static int globalTileID = 0;
    int tileID = 0;
    public int TileID
    {
        get { return tileID; }
    }

    public eTileType[] arrTileType;

    public event Action<TileSetCtrl> EventDisableTile;

    private void Awake()
    {
        tileID = globalTileID++;
        arrTileType = new eTileType[8];

        ITileObject tileObject = null;
        for (int i = 0; i < arrTileType.Length / 4; i++)
        {
            for (int j = 0; j < arrTileType.Length / 2; j++)
            {
                tileObject = transform.GetChild(j + (i * 4)).GetChild(0).GetComponent<ITileObject>();
                if (tileObject == null)
                    arrTileType[j + (i*4)] = eTileType.TileNormal;
                else
                    arrTileType[j + (i*4)] = tileObject.GetTileType();
            }
        }
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y <= -8f)
        {
            this.gameObject.SetActive(false);
        }
	}

    private void OnDisable()
    {
        if(EventDisableTile != null)
        {
            EventDisableTile(this);

            ITileObject tileObject = null;
            for (int i = 0; i < arrTileType.Length / 4; i++)
            {
                for (int j = 0; j < arrTileType.Length / 2; j++)
                {
                    tileObject = transform.GetChild(j + (i * 4)).GetChild(0).GetComponent<ITileObject>();
                    if (tileObject != null)
                    {
                        tileObject.Init();
                    }
                }
            }
        }
    }
}
