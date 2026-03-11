using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float moveSpeed = 1.9f;

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(
            player.position.x,
            player.position.y,
            transform.position.z
            );
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime);
    }
}
