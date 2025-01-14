using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    private NavMeshAgent navMeshAgent;
    Ray lastRay;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        Debug.DrawRay(lastRay.origin, lastRay.direction);
        if (target != null) 
        {
            navMeshAgent.destination = target.position;
        }
    }
}
