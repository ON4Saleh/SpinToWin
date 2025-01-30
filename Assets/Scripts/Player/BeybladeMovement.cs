using UnityEngine;

public class BeybladeMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float pushForce = 10f;  // Force applied when clicking
    public float circularForce = 5f; // Constant force for circular motion
    public float radius = 5f; // Radius of the circular motion

    private Vector3 centerPosition; // The center of the circular motion

    private void Start()
    {
        InitializeRigidbody();
        centerPosition = transform.position + (Vector3.right * radius);
    }

    private void Update()
    {
        HandleMouseInput();
        DebugRayForCursor();
    }

    private void FixedUpdate()
    {
        ApplyCircularMotion();
    }

    private void InitializeRigidbody()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is missing on this GameObject!");
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PushToCursor();
        }
    }

    private void PushToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Ignore clicks on the blade itself
            if (hit.collider.gameObject == gameObject)
            {
                return;
            }

            Vector3 direction = (hit.point - transform.position).normalized;
            rb.AddForce(direction * pushForce, ForceMode.Impulse);
        }
    }

    private void DebugRayForCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
    }

    private void ApplyCircularMotion()
    {
        Vector3 toCenter = centerPosition - transform.position;
        Vector3 tangentDirection = Vector3.Cross(toCenter.normalized, Vector3.up).normalized;
        rb.AddForce(tangentDirection * circularForce, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            return;
        }

        // Calculate collision force (impulse magnitude)
        float impactForce = collision.impulse.magnitude;

        // Calculate the push-back force (double the impact force)
        float pushBackForce = impactForce;

        // Get the collision normal (direction perpendicular to the surface)
        Vector3 collisionNormal = collision.contacts[0].normal;

        // Apply force in the opposite direction of the impact
        rb.AddForce(collisionNormal * pushBackForce, ForceMode.Impulse);
    }
}
