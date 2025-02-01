using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
   
    private StateMachine stateMachine;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private WaypointPath path;
    private Animator animator;
    private Weapon Weapon;
    public NavMeshAgent NavMeshAgent => navMeshAgent ;
    [SerializeField] float navmeshspeedinitial = 6;
    [SerializeField] float navmeshspeedattack = 10;
    public WaypointPath WayPath => path;
    public GameObject player;
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    [SerializeField] string currentState;

    private GameObject EnemyWeaponHolder;
    [SerializeField] private float eyeHeight;
    private Vector3 lastKnownPosition;
    public Vector3 LastKnownPos { get => lastKnownPosition; set => lastKnownPosition = value; }
    private int currentWaypointIndex = 0;
    [SerializeField] private GameObject bulletPrefab; // Reference to the bullet prefab
    [SerializeField] private Transform bulletSpawnPoint; // Point from where bullets will be spawned
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private int bulletsPerBurst = 50; // Number of bullets to fire in a burst
    [SerializeField] private float timeBetweenBullets = 0.1f; // Time between each bullet in the burst

    private bool isFiring = false; // To prevent overlapping coroutines

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Weapon = GetComponent<Weapon>();
        stateMachine.Initialize();
        player = GameObject.FindGameObjectWithTag("Player");

        if (path.waypoints.Count > 0)
        {
            navMeshAgent.SetDestination(path.waypoints[currentWaypointIndex].position);
        }
        EnemyWeaponHolder = transform.GetComponentInChildren<Transform>().Find("EnemyWeaponHolder")?.gameObject;
        Weapon = EnemyWeaponHolder.GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        currentState = stateMachine.activeState.ToString();
        if (currentState != "AttackState")
        {
            canSeePlayer();
        }

        HandleMovement();
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % path.waypoints.Count;
            navMeshAgent.SetDestination(path.waypoints[currentWaypointIndex].position);
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }

    public bool canSeePlayer()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - Vector3.up * eyeHeight;
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);

                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo;

                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            navMeshAgent.angularSpeed = 60f;

                            return true;
                        }
                    }
                }
            }
        }
        navMeshAgent.angularSpeed = 120f; 

        return false;
    }


private void HandleMovement()
{
    if (currentState == "AttackState" && !animator.GetBool("isShooting"))
    {
        if (canSeePlayer())
        {
            animator.SetBool("isShooting", true);
            navMeshAgent.speed = navmeshspeedinitial;
            EnemyWeaponHolder.gameObject.SetActive(true);

            SoundManager.Instance.PlaySFX("Trump");

            // Start firing a burst of bullets if not already firing
            if (!isFiring)
            {
                StartCoroutine(FireBurst());
            }
        }
    }
    else if (currentState != "AttackState" && animator.GetBool("isShooting"))
    {
        animator.SetBool("isShooting", false);
        EnemyWeaponHolder.gameObject.SetActive(false);
        navMeshAgent.speed = navmeshspeedattack;
    }

    if (currentState != "AttackState")
    {
        navMeshAgent.SetDestination(path.waypoints[currentWaypointIndex].position);
        EnemyWeaponHolder.gameObject.SetActive(false);
    }
}

private IEnumerator FireBurst()
{
    isFiring = true;

    // 🔸 Shoot a burst of bullets
    for (int i = 0; i < bulletsPerBurst; i++)
    {
        SpawnBullet();
        yield return new WaitForSeconds(timeBetweenBullets); // Wait before firing the next bullet
    }

    // 🔸 Wait for 10 seconds after completing the burst before allowing next burst
    yield return new WaitForSeconds(10f);

    isFiring = false;
}


    private void SpawnBullet()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // Instantiate the bullet at the spawn point
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Add a small random spread to the bullet direction for realism
            Vector3 randomSpread = Random.insideUnitSphere * 1f; // Adjust the spread amount
            bullet.transform.forward = bulletSpawnPoint.forward + randomSpread;

            // Add velocity to the bullet
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.linearVelocity = bullet.transform.forward * bulletSpeed;
            }
        }
    }
}