using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    bool isPause = false;
    [SerializeField]Canvas canvas;
    public void ChangeScene(int index) {
        SceneManager.LoadScene(index);
    }

    public void SetPauseMenu() {
        if (!isPause) {
            isPause = true;
            canvas.gameObject.SetActive(true);
        } else {
            isPause = false;
            canvas.gameObject.SetActive(false);
        }
    }
}
