using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class BasicAggressorScript : MappableObjectScript
    {
        public int healthBar;
        public static int maxHealth = 30;
        public GameObject thisSector;
        protected BasicSectorScript thisSectorScript;
        public bool isAllied;
        Animator myAnimator;
        public static byte basicDamage;

        void OnEnable()
        {
            thisSectorScript = thisSector.GetComponent<BasicSectorScript>();
            myAnimator = gameObject.GetComponent<Animator>();
        }

        public void TakeDamage(byte dmg)
        {
            healthBar -= dmg;
            if (healthBar >= 0)
                BecomeDestroyed();
        }

        public void SetSector(GameObject s)
        {
            thisSector = s;
            thisSectorScript = thisSector.GetComponent<BasicSectorScript>();
        }

        public abstract void BecomeDestroyed();

        public void Deactivate()
        {
            myAnimator.SetTrigger("deactivate");
        }

        public void Activate()
        {
            myAnimator.SetTrigger("activate");
        }
    }
}
