UI
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartButton : MonoBehaviour
{
    private string ScoreKey = "ScoreData";
    private string StatusKey = "StatusData";
    private string PositionX = "PositionX";
    private string PositionY = "PositionY";
    private string PositionZ = "PositionZ";
    private string _isQuited = "QuitKey";

    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            ResetPosition();
            ResetScore();
            ResetStatus();
            ResetQuit();
            Debug.Log("データがリセットされました");
            Time.timeScale = 1;
            SceneManager.LoadScene("MainScene");
        });
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

    public void ResetQuit()
    {
        PlayerPrefs.SetInt(_isQuited, 0);
        PlayerPrefs.Save();
    }
}
