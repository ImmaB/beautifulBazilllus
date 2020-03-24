using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private Transform startPoint;
    [SerializeField] private float startSize;
    [SerializeField] private float startDuration;
    private float realSize;
    [SerializeField][Range(0, 1)] private float followSpeed;
    private Camera cam;

    private bool follow = false;

    private void Start()
    {
        cam = GetComponent<Camera>();
        realSize = cam.orthographicSize;
        transform.position = transform.position.WithX(startPoint.position.x);
        cam.orthographicSize = startSize;
    }

    private void FixedUpdate()
    {
        if (follow)
        {
            float xDiff = followTarget.position.x - transform.position.x;
            transform.position = transform.position.WithX(transform.position.x + xDiff * followSpeed * Time.deltaTime);
        }
        else if (Time.time > startDuration)
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        Vector3 diff = followTarget.position - transform.position;
        transform.position += new Vector3(diff.normalized.x * followSpeed * Time.deltaTime * 15, 0, 0);
        cam.orthographicSize += (realSize - cam.orthographicSize) * Time.deltaTime;
        if (diff.magnitude < 1 && Mathf.Abs(realSize - cam.orthographicSize) < 0.1)
        {
            cam.orthographicSize = realSize;
            follow = true;
        }
    }
}
