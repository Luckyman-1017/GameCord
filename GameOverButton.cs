using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    [SerializeField] Button _toTitle;
    [SerializeField] Button _reStart;
    private string ScoreKey = "ScoreData";
    private string StatusKey = "StatusData";
    private string PositionX = "PositionX";
    private string PositionY = "PositionY";
    private string PositionZ = "PositionZ";

    // Start is called before the first frame update
    void Start()
    {
        _toTitle.onClick.AddListener(ToTitle);
        _reStart.onClick.AddListener(ReStart);
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ReStart()
    {
        ResetPosition();
        ResetScore();
        ResetStatus();
        SceneManager.LoadScene("MainScene");
    }

    public void ResetPosition()
    {
        PlayerPrefs.DeleteKey(PositionX);
        PlayerPrefs.DeleteKey(PositionY);
        PlayerPrefs.DeleteKey(PositionZ);
        PlayerPrefs.Save();
    }

    public void ResetStatus()
    {
        PlayerPrefs.DeleteKey(StatusKey);
        PlayerPrefs.Save();
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey(ScoreKey);
        PlayerPrefs.Save();
    }
}
