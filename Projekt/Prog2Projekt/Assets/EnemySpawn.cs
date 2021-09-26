using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform movePoint;



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.4f);
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(movePoint.position, 0.3f);
    }


    public void SpawnEnemy(GameObject _enemy)
    {
        EnemyMovement _newEnemy = Instantiate(_enemy, transform.position, Quaternion.identity).GetComponent<EnemyMovement>();
        _newEnemy.Initialize(movePoint.position);
    }
}
