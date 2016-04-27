using UnityEngine;
using System.Collections;

public class LockTo : MonoBehaviour
{
    [SerializeField] GameObject Target = null;
    [SerializeField] float FollowSpeedMulitplier = 1f;
    [SerializeField] float RotateSpeedMulitplier = 1f;

    private float HorizontalAngle = Mathf.PI;
    private float VerticalAngle = Mathf.PI / 4;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPosition = Target.transform.position;
        transform.position = Vector3.Lerp(transform.position, newPosition, FollowSpeedMulitplier * Time.fixedDeltaTime);
    }
}
