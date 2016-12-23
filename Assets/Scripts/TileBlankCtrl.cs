using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBlankCtrl : MonoBehaviour, ITileObject {
    public bool isDeactive = false;
    public eTileType GetTileType()
    {
        return eTileType.TileBlank;
    }

    public void Init()
    {
        isDeactive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDeactive)
            return;
        isDeactive = true;

        Debug.Log(collision.name);
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCtrl>().DropPlayer();
        }
    }
}
