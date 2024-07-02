using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStatus : MobStatus
{
    private NavMeshAgent _agent;
    [SerializeField] private PlayerStatus _pStatus;
    [SerializeField] private int addScore;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
        _pStatus = FindObjectOfType<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
    }

    protected override void OnDie()
    {
        base.OnDie();//Ž€‚ñ‚¾‚Æ‚«‚É‰½‚©‚³‚¹‚é
        _pStatus.ScoreAdd(addScore);
        StartCoroutine(DestroyCoroutine());

    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
