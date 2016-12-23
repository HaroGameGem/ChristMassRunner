using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObstacleCtrl : MonoBehaviour, ITileObject {
    public bool isDeactive = false;
    public GameObject obj;

    public eTileType GetTileType()
    {
        return eTileType.TileObstacle;
    }

    private void Awake()
    {
        obj = transform.FindChild("Object").gameObject;
    }

    public void Init()
    {
        if (!obj.activeSelf)
            obj.SetActive(true);
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
            collision.GetComponent<PlayerCtrl>().HitPlayer();
            obj.SetActive(false);
        }
    }
}
