using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Spawner : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private List<GameObject> enemyPrefab = new List<GameObject>();
    [SerializeField] private int _membersNumber;
    [SerializeField] private float _waitTime;
    [SerializeField] private int _index;

    // Start is called before the first frame update
    private void Start()
    {
        _membersNumber = enemyPrefab.Count;
        _waitTime = 2f;
        StartCoroutine(SpawnLoop());
        _index = Random.Range(0, _membersNumber - 1);
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            var distanceVector = new Vector3(10, 0);
            var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;
            var spawnPosition = playerStatus.transform.position + spawnPositionFromPlayer;
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(spawnPosition, out navMeshHit, 10, NavMesh.AllAreas))
            {
                Instantiate(enemyPrefab[_index], navMeshHit.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(_waitTime);

            if (playerStatus.Life <= 0)
            {
                break;
            }
        }
    }
}
