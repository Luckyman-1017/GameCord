using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReOpeningButton : MonoBehaviour
{
    [SerializeField] private Button _reopenButton;
    [SerializeField] private GameObject _restart;
    [SerializeField] int _quitkey;
    private string _isQuited = "QuitKey";

    void Start()
    {
        _quitkey = PlayerPrefs.GetInt(_isQuited);
        if (_quitkey == 1)
        {
            _restart.SetActive(true);
            _reopenButton.onClick.AddListener(ReOpen);
        }
        else
        {

            _restart.SetActive(false);
        }

    }

    void Update()
    {
    }

    void ReOpen()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }
}
