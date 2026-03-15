using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SkeletonBehaviour : MonoBehaviour
{
    // Glissez ici un objet (ex: un Cube) qui servira de destination visuelle
    private GameObject cible;
    private NavMeshAgent agent;
    private TowerStats towerStats;
    public int CurrentHp;
    public int damage;
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

    void Update()
    {
            
    
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
        Debug.Log("Before :" + CurrentHp);
        CurrentHp -= damage;
        Debug.Log("After :" + CurrentHp);
        if(CurrentHp <= 0)
        {
            Debug.Log("Mort !!");
            Destroy(this.gameObject);
        }
    }
}
