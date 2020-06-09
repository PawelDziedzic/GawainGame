using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class DefendingAggressorScript :BasicAggressorScript
    {
        override
        public void BecomeDestroyed()
        {
            thisSectorScript.LoseADefender(gameObject.name);
            Destroy(gameObject);
        }
    }
}
