using System;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
   [SerializeField] private Transform player;
   [SerializeField] private Vector3 offset;
   [SerializeField] private Vector2 smoothing;
   [SerializeField] private Vector2 deadZone;

   private Vector3 camPostion;

   private void FixedUpdate()
   {
      CalculateCameraPosition();
      transform.position = camPostion;
   }

   private void CalculateCameraPosition()
   {
      camPostion.x = Mathf.Lerp(
         transform.position.x,
         player.position.x + offset.x,
         Time.deltaTime * smoothing.x
      );
      camPostion.y = Mathf.Lerp(
         transform.position.y,
         player.position.y + offset.y,
         Time.deltaTime * smoothing.y
      );

     
      if (camPostion.x - player.position.x > deadZone.x)
         camPostion.x = player.position.x + deadZone.x;
      else if (camPostion.x - player.position.x < -deadZone.x)
         camPostion.x = player.position.x - deadZone.x;

      if (camPostion.y - player.position.y > deadZone.y)
         camPostion.y = player.position.y + deadZone.y;
      else if (camPostion.y - player.position.y < -deadZone.y)
         camPostion.y = player.position.y - deadZone.y;
      camPostion.z = offset.z;
      
   }
}
