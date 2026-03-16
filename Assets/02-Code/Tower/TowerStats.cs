using UnityEngine;

public class TowerStats : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float hp;
    [SerializeField] private int armor;
    [SerializeField] private int regen;
    [SerializeField] private float attackSpeed;
    [SerializeField] private TowerMainStatsUI towerMainStatsUI;

    [Header("Combat Stats")]
    [SerializeField] private float attackRange = 15f;
    [SerializeField] private int attackDamage = 25;

    [Header("Projectile System")]
    [SerializeField] private GameObject projectilePrefab; 
    [SerializeField] private Transform firePoint;

    private void Awake()
    {
        hp = maxHp;
    }

    void Start()
    {
        InvokeRepeating(nameof(RegenHp), 0, 1);
        InvokeRepeating(nameof(Attack), 0, attackSpeed);
    }

    /// <summary>
    /// Régnère les points de vie de la tour avec les points de régénération définis, jusqu'à atteindre le maximum de points de vie.
    /// </summary>
    private void RegenHp()
    {
        if (regen > 0 && hp < maxHp)
        {
            hp += regen;
            if (hp > maxHp)
            {
                hp = maxHp;
            }
        }

        UpdateMainStatsUI();
    }

    public void AddMaxHp(int amount)
    {
        maxHp += amount;
        UpdateMainStatsUI();
    }

    public void AddRegen(int amount)
    {
        regen += amount;
        UpdateMainStatsUI();
    }

    public void AddArmor(int amount)
    {
        armor += amount;
        UpdateMainStatsUI();
    }

    public void MultiplyAttackSpeed(float multiplier)
    {
        attackSpeed *= multiplier;
        UpdateMainStatsUI();
    }

    private void UpdateMainStatsUI()
    {
        if (towerMainStatsUI)
        {
            towerMainStatsUI.SetData(hp, maxHp, armor, regen, attackSpeed);
        }
    }

    public void TakeDamage(float amount)
    {
        //Debug.Log("La tour a subi " + amount + " dégâts ! PV restants : " + hp);
        hp -= amount;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }


    private GameObject GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float shortestDistance = Mathf.Infinity; 
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(currentPosition, enemy.transform.position);

            if (distanceToEnemy < shortestDistance && distanceToEnemy <= attackRange)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private void Attack()
    {
        GameObject target = GetClosestEnemy();
        GameObject projGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Arrow projectile = projGO.GetComponent<Arrow>();
        if (projectile != null)
        {
            projectile.Seek(target.transform, attackDamage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}