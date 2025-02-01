using UnityEngine;

public class WreckingBallHolder : MonoBehaviour
{

    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float maxDistance = 87f;

    [SerializeField]
    private float followSpeed = 1f;

    private void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, playerTransform.position)>=maxDistance)
        {
            Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }


    }
}
