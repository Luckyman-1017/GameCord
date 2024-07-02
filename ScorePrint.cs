using UnityEngine;
using UnityEngine.UI;

public class ScorePrint : MonoBehaviour
{
    [SerializeField] private PlayerStatus _status;
    [SerializeField] private Text _scoreText;

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = "Score : " + _status.getScore;
    }
}
