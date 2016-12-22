using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCoinCtrl : MonoBehaviour, ITileObject {
    public eTileType GetTileType()
    {
        return eTileType.TileCoin;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCtrl>().PickupCoin();
        }
    }
}
