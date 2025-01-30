using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private float movementSpeed = 5f;

    private void Start()
    {
        InitializeNavMeshAgent();
    }

    private void Update()
    {
        HandleMouseInput();
        DebugRayForCursor();
    }

    private void InitializeNavMeshAgent()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent is missing on this GameObject!");
        }
        else
        {
            // Set movement properties
            navMeshAgent.speed = movementSpeed;
            navMeshAgent.acceleration = 20f;
            navMeshAgent.stoppingDistance = 0.1f;

            // Disable automatic rotation
            navMeshAgent.updateRotation = false;
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            navMeshAgent.destination = hit.point;
        }
    }

    private void DebugRayForCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
    }
}