using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObstacleCtrl : MonoBehaviour, ITileObject {
    public eTileType GetTileType()
    {
        return eTileType.TileObstacle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCtrl>().HitPlayer();
        }
    }
}
