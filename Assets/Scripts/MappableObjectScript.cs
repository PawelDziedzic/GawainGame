using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MappableObjectScript : MonoBehaviour
    {
        GameObject GetMinimapIcon()
        {
            for(int i=0; i< transform.childCount; i++)
            {
                GameObject icon = transform.GetChild(i).gameObject;
                if(icon.layer == 9)
                {
                    return icon;
                }
            }

            return null;
        }
    }
}
