using UnityEngine;

public class WreckingBallController : MonoBehaviour
{
    [Header("Phase 1")]
    public Transform player; 
    public float swingForce = 10f; 
    public float swingInterval = 3f; 
 
    [SerializeField]
    private GameObject holder;

    Rigidbody[] ChainsRb;


    private Rigidbody rb;




    void Start()
    {
        rb = GetComponent<Rigidbody>();
     

        StartCoroutine(SwingLoop());
        ChainsRb = holder.GetComponentsInChildren<Rigidbody>();
    }

    private System.Collections.IEnumerator SwingLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(swingInterval);

            SwingTowardPlayer();
        }
    }



    public void OnCollisionEnter(Collision collision)
    {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            directionToPlayer.y = 0; 
            rb.AddForce(-directionToPlayer * swingForce, ForceMode.Impulse);
    }

 

    private void SwingTowardPlayer()
    {
        if (player == null)
        {
            Debug.LogWarning("Player Transform is not assigned!");
            return;
        }

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0; 

        foreach(Rigidbody rb in ChainsRb)
        {
            if(!rb.isKinematic)
            rb.linearVelocity = Vector3.zero;
        }
        rb.AddForce(directionToPlayer * swingForce, ForceMode.Impulse);
        
    }
}