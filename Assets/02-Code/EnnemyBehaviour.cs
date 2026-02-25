using UnityEngine;
using UnityEngine.AI;

public class TestFixedMovement : MonoBehaviour
{
    // Glissez ici un objet (ex: un Cube) qui servira de destination visuelle
    public int destinationTarget; 
    public GameObject cible;
    private NavMeshAgent agent;
    private bool aAttaque = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        cible = GameObject.Find("Tower");

        // 1. DÉBLOQUAGE FORCE : On s'assure que l'agent n'est pas en pause
        agent.isStopped = false; 
        
        MoveToPoint();
    }

    void MoveToPoint()
    {
        // On lance le déplacement
        bool pathFound = agent.SetDestination(cible.transform.position);

    }

    void Update()
    {
            
    
    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. On vérifie si l'objet touché a le Tag "Tower"
        if (other.CompareTag("Tower"))
        {
            Debug.Log("J'attaque la tour !");
            
            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            GetComponent<Animator>().SetBool("Attack", true);

        }
    }
}
