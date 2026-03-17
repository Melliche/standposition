using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private int damage;
    private float speed;
    private Vector3 lastDirection;
    public TowerWeaponInstance weapon;

    [SerializeField] private float lifeTime = 3f;

    public void Initialize(Transform newTarget, int newDamage, float newSpeed, TowerWeaponInstance newWeapon)
    {
        target = newTarget;
        damage = newDamage;
        speed = newSpeed;
        weapon = newWeapon;

        if (target)
        {
            lastDirection = (target.position - transform.position).normalized;
        }
        else
        {
            lastDirection = transform.forward;
        }

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        float distanceThisFrame = speed * Time.deltaTime;

        if (target)
        {
            Vector3 direction = target.position - transform.position;

            if (direction.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            lastDirection = direction.normalized;

            transform.rotation = Quaternion.LookRotation(lastDirection);
            transform.position += lastDirection * distanceThisFrame;
        }
        else
        {
            transform.position += lastDirection * distanceThisFrame;
        }
    }


    /// <summary>
    /// Gère l'impact du projectile avec la cible. Inflige des dégâts à l'ennemi et détruit le projectile.
    /// </summary>
    private void HitTarget()
    {
        if (target)
        {
            SkeletonBehaviour enemy = target.GetComponent<SkeletonBehaviour>();
            if (enemy)
            {
                enemy.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}