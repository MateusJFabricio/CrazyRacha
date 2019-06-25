using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover_Inimico : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform target1, target2;
    private bool chegada1Done;
    // Start is called before the first frame update
    void Start()
    {
        chegada1Done = false;
        target1 = GameObject.FindGameObjectWithTag("Chegada1").transform;
        target2 = GameObject.FindGameObjectWithTag("Chegada2").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.SetDestination(target1.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!chegada1Done)
            if ((navMeshAgent.remainingDistance < 10f) && (navMeshAgent.remainingDistance > 4f))
            {
                print("Passei");    
                navMeshAgent.SetDestination(target2.position);
                chegada1Done = true;
            }
                
    }
}
