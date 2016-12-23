using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public ePlayerSide playerSide = ePlayerSide.None;

    PlayerCtrl playerCtrl;

    public Image[] arrImgHealth;
    public Sprite sprFillHealth;
    public Sprite sprNoneHealth;
    public Image[] arrImgMana;
    public Sprite sprFillMana;
    public Sprite sprNoneMana;

    public Text textTimer;

	// Use this for initialization
	void Start () {
#if UNITY_EDITOR
        if (playerSide == ePlayerSide.None)
            Debug.Log("[ERROR]UIManager : PlayerSide is None");
#endif
        GameManager.Instance.EventTickTimer += OnTickTimer;
        playerCtrl = playerSide == ePlayerSide.Player1 ? DataManager.Instance.player1 : DataManager.Instance.player2;
        playerCtrl.EventHitPlayer += OnHitPlayer;
        playerCtrl.EventDropPlayer += OnDropPlayer;
        playerCtrl.EventPickupCoin += OnPickupCoin;
	}
	
    void OnTickTimer(int timer)
    {
        textTimer.text = timer.ToString();
    }

    void OnHitPlayer(PlayerCtrl player)
    {
        Debug.Log("UI HP Hit 갱신");
        SetHealth(player.HP);
    }

    void OnDropPlayer(PlayerCtrl player)
    {
        Debug.Log("UI HP Drop 갱신");
        SetHealth(player.HP);
    }

    void OnPickupCoin(PlayerCtrl player)
    {
        SetCoin(player.Coin);
    }

    void SetHealth(int hp)
    {
        Debug.Log("남은체력 : " + hp);
        for (int i = 0; i < arrImgHealth.Length; i++)
        {
            arrImgHealth[i].sprite = sprNoneHealth;
        }

        for (int i = 0; i < hp; i++)
        {
            arrImgHealth[i].sprite = sprFillHealth;
        }
    }

    void SetMana(int mp)
    {
        for (int i = 0; i < arrImgMana.Length; i++)
        {
            arrImgMana[i].sprite = sprNoneMana;
        }

        for (int i = 0; i < mp; i++)
        {
            arrImgMana[i].sprite = sprFillMana;
        }
    }

    void SetCoin(int coin)
    {
        Debug.Log("UIManager 코인먹기 UI갱신 구현 안됨");
    }
}
