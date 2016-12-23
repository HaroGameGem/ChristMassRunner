using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerCtrl : MonoBehaviour {
    public event Action<PlayerCtrl> EventHitPlayer;     //장애물에 걸렸을 때
    public event Action<PlayerCtrl> EventDropPlayer;    //바닥에 떨어졌을 때
    public event Action<PlayerCtrl> EventPickupCoin;    //코인을 먹었을 때
    public event Action<PlayerCtrl> EventDiePlayer;     //플레이어가 죽었을 때

    Animator animator;

    public ePlayerSide playerSide;

    [SerializeField]
    int playerPosIdx = 0;

    public enum eMoveSide
    {
        None,
        MoveRight,
        MoveLeft
    }

    public bool isAlive = true;

    int hp = 3;
    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }
    int mp = 3;
    public int MP
    {
        get { return mp; }
        set { mp = value; }
    }
    int coin = 0;
    public int Coin
    {
        get { return coin; }
        set { coin = value; }
    }

    float[] arrMoveXPos = null;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        arrMoveXPos = new float[4];
        for (int i = 0; i < arrMoveXPos.Length; i++)
        {
            arrMoveXPos[i] = -2f + (i * 1.35f);
        }
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonClickedMoveLeft()
    {
        Move(eMoveSide.MoveLeft);
    }

    public void OnButtonClickedMoveRight()
    {
        Move(eMoveSide.MoveRight);
    }

    void Move(eMoveSide moveSide)
    {
        int idx = moveSide == eMoveSide.MoveRight ? playerPosIdx + 1 : playerPosIdx - 1;
        Debug.Log(idx);
        if(idx < 0 || idx > 3)
        {
            Debug.Log("Idx초과 / " + moveSide);
            return;
        }

        string strTrigger = moveSide == eMoveSide.MoveRight ? "JumpRight" : "JumpLeft";
        Debug.Log(strTrigger);
        animator.SetTrigger(strTrigger);

        transform.DOLocalMoveX(arrMoveXPos[idx], 0.2f);
        playerPosIdx = idx;
    }

    public void HitPlayer()
    {
        hp--;
        if (hp < 0)
        {
            hp = 0;
            if (!isAlive)
                return;
            if (EventDiePlayer != null)
                EventDiePlayer(this);
        }
        if (EventHitPlayer != null)
            EventHitPlayer(this);
        Debug.Log("Hit");
    }

    public void DropPlayer()
    {
        hp--;
        if (hp < 0)
        {
            hp = 0;
            if (!isAlive)
                return;
            if (EventDiePlayer != null)
                EventDiePlayer(this);
            return;
        }
        if(EventDropPlayer != null)
            EventDropPlayer(this);
        Debug.Log("Drop");
    }

    public void PickupCoin()
    {
        if (EventPickupCoin != null)
            EventPickupCoin(this);
        Debug.Log("PickupCoin");
    }

}
