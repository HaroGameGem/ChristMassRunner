using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCtrl : MonoBehaviour {
    public void OnButtonClickedPlayGame()
    {
        SceneManager.LoadScene("main");
    }
}
