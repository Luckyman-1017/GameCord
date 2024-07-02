using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private PlayerStatus _status;
    [SerializeField] private PlayerController _controller;
    private string ScoreKey = "ScoreData";
    private string StatusKey = "StatusData";
    private string PositionX = "PositionX";
    private string PositionY = "PositionY";
    private string PositionZ = "PositionZ";


    public void SaveScore()
    {
        PlayerPrefs.SetInt(ScoreKey, _status.getScore);
        PlayerPrefs.Save();
    }

    public int LoadScore()
    {
        return PlayerPrefs.GetInt(ScoreKey, _status.getScore);
    }

    public void ResetScore() 
    {
        PlayerPrefs.DeleteKey(ScoreKey);
        PlayerPrefs.Save();
    }

    public void SaveStatus()
    {
        PlayerPrefs.SetFloat(StatusKey, _status.Life);
        PlayerPrefs.Save();
    }

    public float LoadStatus()
    {
        return PlayerPrefs.GetFloat(StatusKey, _status.Life);
    }

    public void ResetStatus()
    {
        PlayerPrefs.DeleteKey(StatusKey);
        PlayerPrefs.Save();
    }

    public void SavePosition()
    {
        PlayerPrefs.SetFloat(PositionX, _controller.GetPositionX);
        PlayerPrefs.SetFloat(PositionY, _controller.GetPositionY);
        PlayerPrefs.SetFloat(PositionZ, _controller.GetPositionZ);
        PlayerPrefs.Save();
    }

    public Vector3 LoadPosition()
    {
        Vector3 position;
        position.x = PlayerPrefs.GetFloat(PositionX, _controller.GetPositionX);
        position.y = PlayerPrefs.GetFloat(PositionY, _controller.GetPositionY);
        position.z = PlayerPrefs.GetFloat(PositionZ, _controller.GetPositionZ);

        return position;
    }

    public void ResetPosition()
    {
        PlayerPrefs.DeleteKey(PositionX);
        PlayerPrefs.DeleteKey(PositionY);
        PlayerPrefs.DeleteKey(PositionZ);
        PlayerPrefs.Save();
    }
}
