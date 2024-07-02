using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MobStatus))]
public class MobAttack : MonoBehaviour//攻撃について管理(MobStatusクラスからの派生クラス)
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Collider attackCollider;
    [SerializeField] private AudioSource swingSound;
    private MobStatus _status;

    // Start is called before the first frame update
    void Start()
    {
        _status = GetComponent<MobStatus>();
    }

    public void AttackIfPossible()//攻撃モーションさせる
    {
        if (_status.IsAttackable) return;
        _status.GoToAttackStateIfPossible();
    }

    public void OnAttackRangeEnter(Collider collider)//攻撃開始判定にモブが入ってきたときの処理
    {
        AttackIfPossible();
    }

    public void OnAttackStart()
    {
        attackCollider.enabled = true;//攻撃判定用のコライダーを展開(任意のタイミングに設定可)
        if (swingSound != null)
        {
            swingSound.pitch = Random.Range(0.7f, 1.3f);
            swingSound.Play();
        }
    }

    public void OnHitAttack(Collider collider)
    {
        var targetMob = collider.GetComponent<MobStatus>();//相手方のMOBステータスを読み取り
        if (targetMob == null) return;

        targetMob.Damage(_status.HitDamage);
    }

    public void OnAttackFinished()
    {
        attackCollider.enabled = false;//攻撃判定用のコライダーを収束(任意のタイミングに設定可)
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        _status.GoToNormalStateIfPossible();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}