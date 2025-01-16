using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
    }
}
