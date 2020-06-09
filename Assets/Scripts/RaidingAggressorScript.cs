using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class RaidingAggressorScript :BasicAggressorScript
    {
        override
        public void BecomeDestroyed()
        {
            thisSectorScript.LoseARaider(gameObject.name);
            Destroy(gameObject);
        }
    }
}
