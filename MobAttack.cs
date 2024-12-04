using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MobStatus))]
public class MobAttack : MonoBehaviour//�U���ɂ��ĊǗ�(MobStatus�N���X����̔h���N���X)
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

    public void AttackIfPossible()//�U�����[�V����������
    {
        if (_status.IsAttackable) return;
        _status.GoToAttackStateIfPossible();
    }

    public void OnAttackRangeEnter(Collider collider)//�U���J�n����Ƀ��u�������Ă����Ƃ��̏���
    {
        AttackIfPossible();
    }

    public void OnAttackStart()
    {
        attackCollider.enabled = true;//�U������p�̃R���C�_�[��W�J(�C�ӂ̃^�C�~���O�ɐݒ��)
        if (swingSound != null)
        {
            swingSound.pitch = Random.Range(0.7f, 1.3f);
            swingSound.Play();
        }
    }

    public void OnHitAttack(Collider collider)
    {
        var targetMob = collider.GetComponent<MobStatus>();//�������MOB�X�e�[�^�X��ǂݎ��
        if (targetMob == null) return;

        targetMob.Damage(_status.HitDamage);
    }

    public void OnAttackFinished()
    {
        attackCollider.enabled = false;//�U������p�̃R���C�_�[������(�C�ӂ̃^�C�~���O�ɐݒ��)
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