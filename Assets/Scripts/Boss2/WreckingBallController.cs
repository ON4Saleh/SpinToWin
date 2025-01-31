using UnityEngine;

public class WreckingBallController : MonoBehaviour
{
    [Header("Phase 1")]
    public Transform player; // Reference to the player's transform
    public float swingForce = 10f; // Force applied to swing the ball
    public float swingInterval = 3f; // Time between swings (in seconds)
    [SerializeField]
    private float enemyHealth = 10f;
    [SerializeField]
    private GameObject holder;

    Rigidbody[] ChainsRb;


    private Rigidbody rb;
    private RotateWreckingBall rotateWreckingBall;


    

    [Header("Phase 2")]
    [SerializeField]
    private bool isPhase2 = false;
    [SerializeField]
    private float followSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotateWreckingBall = GetComponent<RotateWreckingBall>();

        if (rotateWreckingBall == null)
        {
            Debug.LogError("RotateWreckingBall component not found!");
            return;
        }

        StartCoroutine(SwingLoop());
        ChainsRb = holder.GetComponentsInChildren<Rigidbody>();
    }

    private System.Collections.IEnumerator SwingLoop()
    {
        while (!isPhase2)
        {
            // Wait for the swing interval before applying force
            yield return new WaitForSeconds(swingInterval);

            // Apply force toward the player (only on X and Z axes)
            SwingTowardPlayer();
        }
    }

    void FixedUpdate()
    {
        if (isPhase2)
            FollowPlayer();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyHealth -= 1;
            if (enemyHealth < 5f)
            {
                isPhase2 = true;
            }

            // Push the ball away from the player on collision
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            directionToPlayer.y = 0; // Restrict movement to the X and Z axes
            rb.AddForce(-directionToPlayer * swingForce, ForceMode.Impulse);
        }
    }

    public void FollowPlayer()
    {
        // Remove the HingeJoint component in Phase 2
        HingeJoint hingeJoint = gameObject.GetComponent<HingeJoint>();
        if (hingeJoint != null)
        {
            Destroy(hingeJoint);
        }

        // Move the ball toward the player in Phase 2
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0; // Restrict movement to the X and Z axes
        rb.linearVelocity = directionToPlayer * followSpeed;
    }

    private void SwingTowardPlayer()
    {
        if (player == null)
        {
            Debug.LogWarning("Player Transform is not assigned!");
            return;
        }

        // Calculate the direction to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0; // Restrict movement to the X and Z axes

        // Apply force in the direction of the player (only on X and Z axes)
        foreach(Rigidbody rb in ChainsRb)
        {
            if(!rb.isKinematic)
            rb.linearVelocity = Vector3.zero;
        }
        rb.AddForce(directionToPlayer * swingForce, ForceMode.Impulse);
        
    }
}