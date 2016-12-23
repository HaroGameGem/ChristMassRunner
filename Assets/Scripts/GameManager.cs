using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {
    public event Action EventStartGamePlay;
    public event Action EventWinGame;
    public event Action EventLoseGame;
    public event Action<int> EventTickTimer;

    static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    public DataManager dataManager;
    public UIManager uiManager1P;
    public UIManager uiManager2P;

    public bool isPlaying;
    public int timer = 60;

    private void Awake()
    {
#if UNITY_EDITOR
        if (instance != null)
            Debug.Log("[ERROR]GameManager : 둘 이상 입니다.");
#endif
        instance = this;
    }

    // Use this for initialization
    void Start () {
        dataManager = DataManager.Instance;

        EventStartGamePlay += OnStartGamePlay;

        PlayerCtrl player1 = dataManager.player1;
        player1.EventHitPlayer += OnHitPlayer;
        player1.EventDropPlayer += OnDropPlayer;
        player1.EventPickupCoin += OnPickupCoin;
        player1.EventDiePlayer += OnDiePlayer;

        PlayerCtrl player2 = dataManager.player2;
        player2.EventHitPlayer += OnHitPlayer;
        player2.EventDropPlayer += OnDropPlayer;
        player2.EventPickupCoin += OnPickupCoin;
        player2.EventDiePlayer += OnDiePlayer;

        Init();

        StartCoroutine(DelayStartGamePlay());
	}

    public void Init()
    {
        isPlaying = false;
        ScrollCtrl.isScrolling1P = false;
        ScrollCtrl.isScrolling2P = false;
    }

    IEnumerator DelayStartGamePlay()
    {
        yield return new WaitForSeconds(2f);
        EventStartGamePlay();
        StartCoroutine(CalculateTimer());
    }

    IEnumerator CalculateTimer()
    {
        yield return new WaitForSeconds(1f);
        if(EventTickTimer != null)
            EventTickTimer(--timer);
        StartCoroutine(CalculateTimer());
    }

    void OnStartGamePlay()
    {
        isPlaying = true;
        ScrollCtrl.isScrolling1P = true;
        ScrollCtrl.isScrolling2P = true;
    }

    void OnTickTimer()
    {
        Debug.Log("Timer : " + timer);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnHitPlayer(PlayerCtrl player)
    {
        Debug.Log(player.name + " 가 맞았다.");
        StartCoroutine(CoHitPlayer(player));
    }

    //기술부채

    IEnumerator CoHitPlayer(PlayerCtrl player)
    {
        switch (player.playerSide)
        {
            case ePlayerSide.Player1:   ScrollCtrl.scrollSpeed1P = -1f; break;
            case ePlayerSide.Player2:   ScrollCtrl.scrollSpeed2P = -1f; break;
        }

        yield return new WaitForSeconds(0.5f);

        switch (player.playerSide)
        {
            case ePlayerSide.Player1: ScrollCtrl.isScrolling1P = false; break;
            case ePlayerSide.Player2: ScrollCtrl.isScrolling2P = false; break;
        }
        yield return new WaitForSeconds(1f);
        switch (player.playerSide)
        {
            case ePlayerSide.Player1: ScrollCtrl.isScrolling1P = true; break;
            case ePlayerSide.Player2: ScrollCtrl.isScrolling2P = true; break;
        }

        switch (player.playerSide)
        {
            case ePlayerSide.Player1: ScrollCtrl.scrollSpeed1P = 2.5f; break;
            case ePlayerSide.Player2: ScrollCtrl.scrollSpeed2P = 2.5f; break;
        }
    }

    void OnDropPlayer(PlayerCtrl player)
        
    {
        Debug.Log(player.name + " 가 떨어졌다.");
        StartCoroutine(CoDropPlayer(player));
    }

    //기술부채
    IEnumerator CoDropPlayer(PlayerCtrl player)
    {
        switch (player.playerSide)
        {
            case ePlayerSide.Player1:   ScrollCtrl.scrollSpeed1P = -1f; break;
            case ePlayerSide.Player2:   ScrollCtrl.scrollSpeed2P = -1f; break;
        }

        yield return new WaitForSeconds(0.5f);

        switch (player.playerSide)
        {
            case ePlayerSide.Player1: ScrollCtrl.isScrolling1P = false; break;
            case ePlayerSide.Player2: ScrollCtrl.isScrolling2P = false; break;
        }
        yield return new WaitForSeconds(1f);
        switch (player.playerSide)
        {
            case ePlayerSide.Player1: ScrollCtrl.isScrolling1P = true; break;
            case ePlayerSide.Player2: ScrollCtrl.isScrolling2P = true; break;
        }

        switch (player.playerSide)
        {
            case ePlayerSide.Player1: ScrollCtrl.scrollSpeed1P = 2.5f; break;
            case ePlayerSide.Player2: ScrollCtrl.scrollSpeed2P = 2.5f; break;
        }
    }

    void OnPickupCoin(PlayerCtrl player)
    {
        Debug.Log(player.name + " 가 동전을 주웠다.");
    }

    void OnDiePlayer(PlayerCtrl player)
    {
        player.isAlive = false;
    }
}
