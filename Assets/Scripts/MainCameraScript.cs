using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MainCameraScript : MonoBehaviour
    {
        Vector3 orientationVectorShift;
        float transfromPositionY;

        private void OnEnable()
        {
            transfromPositionY = transform.position.y;
        }

        void FixedUpdate()
        {
            if (ProtoPlayerScript.PlayerInstance != null)
            {
                SetOrientationVectorShift();

                transform.SetPositionAndRotation(
                  new Vector3(
                      ProtoPlayerScript.PlayerInstance.transform.position.x,
                      transfromPositionY,
                      ProtoPlayerScript.PlayerInstance.transform.position.z - 9f
                      ) + orientationVectorShift,
                  transform.rotation
                  );
            }
        }//*/

        void SetOrientationVectorShift()
        {
            orientationVectorShift += 0.1f * ProtoPlayerScript.PlayerInstance.transform.forward;
            orientationVectorShift = Vector3.ClampMagnitude(orientationVectorShift, 2f);
        }
    }
}