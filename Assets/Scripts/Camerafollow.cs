using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    [SerializeField] Transform player;//charator position
    [SerializeField] float timeoffset;//camera move speed
    [SerializeField] Vector3 offsetPos;//camera and charator distance

    [SerializeField] Vector3 boundsMin;//camera coordinate right and up
    [SerializeField] Vector3 boundsMax;//camera coordinate left and down


    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 startPos = transform.position;//get camera position 
            Vector3 targetPos = player.position;//get charator position

            targetPos.x += offsetPos.x;//set camera offset
            targetPos.y += offsetPos.y;
            targetPos.z = transform.position.z;

            targetPos.x = Mathf.Clamp(targetPos.x, boundsMin.x, boundsMax.x);//prevent camera exceed map
            targetPos.y = Mathf.Clamp(targetPos.y, boundsMin.y, boundsMax.y);//prevent camera exceed map

            float t = 1f - Mathf.Pow(1f - timeoffset, Time.deltaTime * 30);//calculate camera move speed
            transform.position = Vector3.Lerp(startPos, targetPos, t);//set camera from starpos to targetpos by t 
        }
        
    }
}
