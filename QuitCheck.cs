using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class QuitCheck : MonoBehaviour
{
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _canselButton;
    [SerializeField] private Button _yesButton;
    [SerializeField] private GameObject quitCheck;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PlayerData _data;
    [SerializeField] private PlayerController _controller;
    private string _isQuited = "QuitKey";

    // Start is called before the first frame update
    void Start()
    {
        quitCheck.SetActive(false);
        _quitButton.onClick.AddListener(QuitChecking);
        _canselButton.onClick.AddListener(QuitCansel);
        _yesButton.onClick.AddListener(Quit);
    }

    void QuitChecking()
    {
        pauseMenu.SetActive(false);
        quitCheck.SetActive(true);
    }

    void QuitCansel()
    {
        quitCheck.SetActive(false);
        pauseMenu.SetActive(true);
    }

    void Quit()
    {
        quitCheck.SetActive(false);
        PlayerPrefs.SetInt(_isQuited, 1);
        _data.SavePosition();
        _data.SaveStatus();
        _data.SaveScore();
        SceneManager.LoadScene("TitleScene");
    }
}
