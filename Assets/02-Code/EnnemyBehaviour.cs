using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class TestFixedMovement : MonoBehaviour
{
    // Glissez ici un objet (ex: un Cube) qui servira de destination visuelle
    public int destinationTarget; 
    public GameObject cible;
    private NavMeshAgent agent;
    private bool aAttaque = false;

    public UnityEvent onHitTower;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        cible = GameObject.Find("Tower");

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            Debug.Log("J'attaque la tour !");
            
            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            GetComponent<Animator>().SetBool("Attack", true);
            aAttaque = true;
            onHitTower?.Invoke();
        }
    }
}
