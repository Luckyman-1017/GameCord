using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverTextAnimator : MonoBehaviour
{
    [SerializeField] private Text _scoretxt;
    private string ScoreKey = "ScoreData";
    private string _isQuited = "QuitKey";
    [SerializeField] int _score;
    [SerializeField] GameObject _buttons;
    [SerializeField] int _waitTime;

    // Start is called before the first frame update
    void Start()
    {
        _buttons.SetActive(false);
        var transformCache = transform;
        var defaultPosition = transformCache.localPosition;
        transformCache.localPosition = new Vector3(0, 360f);
        transformCache.DOLocalMove(defaultPosition, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Debug.Log("GAME OVER!!");
                transformCache.DOShakePosition(1.5f, 100);
            });
        DOVirtual.DelayedCall(_waitTime, () =>
         {
             _scoretxt.text = "Score : " + PlayerPrefs.GetInt(ScoreKey);
             PlayerPrefs.SetInt(_isQuited, 0);
             PlayerPrefs.Save();
             _buttons.SetActive(true);
         });
    }
}
