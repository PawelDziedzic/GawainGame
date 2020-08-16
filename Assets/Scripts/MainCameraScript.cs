using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MainCameraScript : MonoBehaviour
    {
        void Update()
        {
            if (ProtoPlayerScript.PlayerInstance != null)
            {
                transform.SetPositionAndRotation(
                  new Vector3(
                      ProtoPlayerScript.PlayerInstance.transform.position.x,
                      transform.position.y,
                      ProtoPlayerScript.PlayerInstance.transform.position.z - 7f
                      ),
                  transform.rotation
                  );
            }
        }
    }
}