using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class SkeletonBehaviour : MonoBehaviour
{
    private GameObject cible;
    private NavMeshAgent agent;
    private TowerStats towerStats;
    private Economy economy;
    private GameObject gameRoot;

    public EnemyScriptableObject enemyScriptableObject;
    public float hp;
    public float damage;
    public static List<GameObject> AllEnemies = new List<GameObject>();
    public bool isAimed;

    void Start()
    {
        hp = enemyScriptableObject.health;
        damage = enemyScriptableObject.damage;
        agent = GetComponent<NavMeshAgent>();

        cible = GameObject.Find("Tower");
        towerStats = cible.GetComponent<TowerStats>();
        gameRoot = GameObject.Find("GameRoot");
        economy = gameRoot.GetComponent<Economy>();

        agent.isStopped = false;

        MoveToPoint();
    }

    void MoveToPoint()
    {
        GameObject hb = towerStats.hitBox;
        bool pathFound = agent.SetDestination(hb.transform.position);
    }

    void AttackTower()
    {
        if (cible != null)
        {
            if (towerStats != null)
            {
                towerStats.TakeDamage(damage);
            }
        }
    }

    public void SetStats(WaveStatScritableObject stats)
    {
        hp *= stats.health;
        damage *= stats.damage;
        Debug.Log("HP : " + hp + " Damage : " + damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            GetComponent<Animator>().SetBool("Attack", true);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        AllEnemies.Add(gameObject);
    }

    void OnDisable()
    {
        AllEnemies.Remove(gameObject);
    }

    private void OnDestroy()
    {
        if (economy)
        {
            // Ajoute l'or de récompense de kill à l'économie du joueur
            economy.AddGold(economy.KillIncome);
            ShopUI shop = economy.GetComponent<ShopUI>();
            if (shop)
            {
                shop.UpdateHeader(economy.Gold);
            }
        }
    }
}