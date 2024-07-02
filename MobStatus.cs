using System.Collections;
using UnityEngine;

public abstract class MobStatus : MonoBehaviour//HP、状態について管理
{
   protected enum StateEnum//キャラクターの状態
    {
        Normal,
        Attack,
        Die
    }

    public bool IsMovable => _state == StateEnum.Normal;
    public bool IsAttackable => _state == StateEnum.Attack;
    public bool IsDie => _state == StateEnum.Die;

    public float LifeMax => lifeMax;
    public float Life => _life;
    public int HitDamage => _hitdamage;

    [SerializeField] private int _hitdamage = 1;
    [SerializeField] private float lifeMax = 10;
    protected StateEnum _state;
    protected Animator _animator;
    protected float _life;

    protected virtual void Start()
    {
        _state = StateEnum.Normal;
        _life = lifeMax;
        LifeGaugeContainer.Instance.Add(this);
        _animator = GetComponentInChildren<Animator>();
    }

    protected virtual void OnDie()
    {
        LifeGaugeContainer.Instance.Remove(this);
    }

    public void Damage(int damage)
    {
        if (IsDie) return;
        _life -= damage;
        if (_life > 0) return;
        _state = StateEnum.Die;
        _animator.SetTrigger("Die");
        OnDie();
    }

    public void GoToAttackStateIfPossible()
    {
        if (IsDie) return;
            _state = StateEnum.Attack;
            _animator.SetTrigger("Attack");
    }

    public void GoToNormalStateIfPossible()
    {
        if (IsDie) return;
        _state = StateEnum.Normal;
    }
}