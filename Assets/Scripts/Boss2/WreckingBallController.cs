using UnityEngine;

public class WreckingBallController : MonoBehaviour
{
    [Header("Phase 1")]
    public Transform player; // Reference to the player's transform
    public float swingForce = 10f; // Force applied to swing the ball
    public float swingInterval = 3f; // Time between swings (in seconds)

    private Rigidbody rb;
    private RotateWreckingBall rotateWreckingBall;

    [Header("Phase 2")]
    [SerializeField]
    private bool isPhase2 = false;
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
    }

    private System.Collections.IEnumerator SwingLoop()
    {
        while (!isPhase2)
        {
            // Wait until the wrecking ball is rotated toward the player
            yield return new WaitUntil(() => rotateWreckingBall.IsRotatedTowardPlayer);

            // Apply force toward the player (only on X and Z axes)
            SwingTowardPlayer();

            // Wait for the swing interval before the next swing
            yield return new WaitForSeconds(swingInterval);
        }
    }
    private void FixedUpdate()
    {
        if (isPhase2)
            FollowPlayer();
       
        
    }

    public void FollowPlayer()
    {

        HingeJoint hingeJoint = gameObject.GetComponent<HingeJoint>();
        Destroy(hingeJoint);
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, player.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
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

        // Remove the Y component to restrict movement to the X and Z axes
        directionToPlayer.y = 0;

        // Apply force in the direction of the player (only on X and Z axes)
        rb.AddForce(directionToPlayer * swingForce, ForceMode.Impulse);
    }
}