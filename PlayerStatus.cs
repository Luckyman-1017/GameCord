using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerStatus : MobStatus
{
    public int _combo;
    private MobAttack _attack;
    [SerializeField] private int _score = 0;
    [SerializeField] private PlayerData _data;
    private string ScoreKey = "ScoreData";
    private string StatusKey = "StatusData";

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public int getScore
    {
        get
        {
            return _score;
        }
    }

    public void Combo()
    {
        base.GoToAttackStateIfPossible();
        _animator.SetInteger("Combo", _combo);
        _combo++;
        if (_combo == 4)
        {
            _combo = 1;
        }
    }

    protected override void OnDie()
    {
        base.OnDie();
        _data.SaveScore();
        StartCoroutine(GoToGameOverCoroutine());
    }

    public void ScoreAdd(int _score)
    {
        this._score += _score;
    }

    private IEnumerator GoToGameOverCoroutine()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOverScene");
    }

    public void Initialize()
    {
        base.Start();
        _combo = 1;
        if (PlayerPrefs.HasKey(ScoreKey))
        {
            this._score = _data.LoadScore();
        }
        if (PlayerPrefs.HasKey(StatusKey))
        {
            this._life = _data.LoadStatus();
        }
    }
}