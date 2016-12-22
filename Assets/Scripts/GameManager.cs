using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {
    public event Action EventStartGamePlay;

    static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    public DataManager dataManager;

    public bool isPlaying;

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

        PlayerCtrl player2 = dataManager.player2;
        player2.EventHitPlayer += OnHitPlayer;
        player2.EventDropPlayer += OnDropPlayer;
        player2.EventPickupCoin += OnPickupCoin;

        Init();

        StartCoroutine(DelayStartGamePlay());
	}

    public void Init()
    {
        ScrollCtrl.isScrolling1P = false;
        ScrollCtrl.isScrolling2P = false;
    }

    IEnumerator DelayStartGamePlay()
    {
        yield return new WaitForSeconds(2f);
        EventStartGamePlay();
    }

    void OnStartGamePlay()
    {
        isPlaying = true;
        ScrollCtrl.isScrolling1P = true;
        ScrollCtrl.isScrolling2P = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnHitPlayer(PlayerCtrl player)
    {

    }

    void OnDropPlayer(PlayerCtrl player)
    {

    }

    void OnPickupCoin(PlayerCtrl player)
    {

    }

}
