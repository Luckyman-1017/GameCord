using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFinish : MonoBehaviour
{
    [SerializeField] private Button _finishButton;
    // Start is called before the first frame update
    void Start()
    {
        _finishButton.onClick.AddListener(AppFinish);
    }
    public void AppFinish()
    {
        Application.Quit();
    }
}
