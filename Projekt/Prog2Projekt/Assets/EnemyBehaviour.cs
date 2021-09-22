using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject projectile = null;
    [SerializeField] private Transform projectileSpawn = null;
    [SerializeField] private float attackInterval = 1f;
    [SerializeField] private float attackIntervalRandomizing = 0.15f;
    [SerializeField] private int damage = 20;
    [SerializeField] private float projectileSpeed = 40f;

    private bool canAttack = true;
    private Vector3 predictedPosition_ = Vector3.zero;




    public void Attack(Vector3 _targetPos,Vector3 _targetVelocity)
    {
        if (canAttack)
        {

            print(Mathf.Clamp(1 - ((projectileSpeed / 100) * 2),0,2f));
            print((transform.position - _targetPos).magnitude);
            predictedPosition_ = _targetPos + (_targetVelocity * ((Mathf.Clamp(1 - ((projectileSpeed / 100) * 2), 0, 2f)) +
               (Mathf.Clamp((transform.position - _targetPos).magnitude / 100, 0, 1f))));

            Rigidbody _newProjectile = Instantiate(projectile, projectileSpawn.position, Quaternion.LookRotation(_targetPos - projectileSpawn.position)).GetComponent<Rigidbody>();
            _newProjectile.AddForce((predictedPosition_ - projectileSpawn.position).normalized * projectileSpeed, ForceMode.VelocityChange);
            _newProjectile.GetComponent<Projectile>().SetDamage(damage);

            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackInterval + Random.Range(-attackIntervalRandomizing,attackIntervalRandomizing));
        canAttack = true;
    }


}
