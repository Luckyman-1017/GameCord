using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayerMask;
    private RaycastHit[] _raycastHits = new RaycastHit[10];
    private NavMeshAgent _agent;
    private EnemyStatus _status;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _status = GetComponent<EnemyStatus>();
    }

    public void OnDetectObject(Collider collider)//他コライダーとぶつかったときの処理
    {
        if (!_status.IsMovable)
        {
            _agent.isStopped = true;
            return;
        }

        if (collider.CompareTag("Player"))
        {
            var positionDiff = collider.transform.position - transform.position;//自分と相手(プレイヤー)の位置ベクトルの差
            var distance = positionDiff.magnitude;//大きさ
            var direction = positionDiff.normalized;//方向
            var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance, raycastLayerMask);
            Debug.Log("hitCount:" + hitCount);
            if (hitCount == 0)
            {
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;
            }
            else
            {
                _agent.isStopped = true;
            }
        }
    }
}
