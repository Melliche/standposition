using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform target;
    private int damage;
    public float speed = 10f;

    public void Seek(Transform newTarget, int degatsDeLaTour)
    {
        target = newTarget;
        damage = degatsDeLaTour;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.LookAt(target.position);
        
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Debug.Log("Boum ! Le projectile a touché " + target.name);
        
        target.GetComponent<SkeletonBehaviour>().TakeDamage(damage);
        
        Destroy(gameObject);
    }
}