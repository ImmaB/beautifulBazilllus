using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField][Range(0, 1)] private float followSpeed;

    private void FixedUpdate()
    {
        float xDiff = followTarget.position.x - transform.position.x;
        transform.position = new Vector3(transform.position.x + xDiff * followSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
