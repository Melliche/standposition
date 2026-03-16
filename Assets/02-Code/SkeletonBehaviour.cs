using UnityEngine;
using UnityEngine.AI;

public class SkeletonBehaviour : MonoBehaviour
{
    // Glissez ici un objet (ex: un Cube) qui servira de destination visuelle
    private GameObject cible;
    private NavMeshAgent agent;
    private TowerStats towerStats;

    public EnemyScriptableObject enemyScriptableObject;
    public float hp;
    public float damage;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        cible = GameObject.Find("Tower");
        towerStats = cible.GetComponent<TowerStats>();

        agent.isStopped = false; 
        
        MoveToPoint();
    }

    void MoveToPoint()
    {
        bool pathFound = agent.SetDestination(cible.transform.position);
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
        //Debug.Log("Before :" + hp);
        hp -= damage;
        //Debug.Log("After :" + hp);
        if(hp <= 0)
        {
            //Debug.Log("Mort !!");
            Destroy(this.gameObject);
        }
    }
}
