using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class BasicSectorScript : MappableObjectScript
    {
        public bool? isAllied;
        Dictionary<string,BasicAggressorScript> activeDefenders;
        Dictionary<string, BasicAggressorScript> activeRaiders;
        float xMin, xMax, zMin, zMax;
        public int index;
        protected float angle = 0f;
        protected float halfLength;
        Animator myAnimator;

        // Start is called before the first frame update
        void OnEnable()
        {
            isAllied = isAllied ?? false;
            activeDefenders = new Dictionary<string, BasicAggressorScript>();
            activeRaiders = new Dictionary<string, BasicAggressorScript>();
            myAnimator = gameObject.GetComponent<Animator>();

            SectorManagerScript.AddSector(gameObject.name, this);
            SetSpawnPoints();
            ConstructWalls();
        }

        void ConstructWalls()
        {
            xMin = transform.position.x - transform.localScale.x;
            xMax = transform.position.x + transform.localScale.x;
            zMin = transform.position.z - transform.localScale.z;
            zMax = transform.position.z + transform.localScale.z;
        }

        void SetSpawnPoints()
        {
            int xDist = (int)(xMax - transform.position.x) / 2;
            int zDist = (int)(zMax - transform.position.z) / 2;

            halfLength = xDist >= zDist ? zDist : xDist;
        }

        protected Vector3 GetSpawnPoint()
        {
            angle += 0.618034f;
            float xOffset = halfLength * Mathf.Cos(angle);
            float zOffset = halfLength *  Mathf.Sin(angle);
            Vector3 point = new Vector3(transform.position.x + xOffset,
                0f, transform.position.z + zOffset);
            return point;
        }

        public void LoseADefender(string name)
        {
            activeDefenders.Remove(name);
            if (activeDefenders.Count <= 0)
                ChangeAlliance();
        }

        public void LoseARaider(string name)
        {
            activeRaiders.Remove(name);
            if (activeRaiders.Count <= 0)
                SuccesfulDefence();
        }
        
        void ChangeAlliance()
        {
            isAllied = !isAllied;
            //EVENT - communicate to player
            if(isAllied.Value)
            {
                SpawnDefenders(GameManagerScript.GetStagePhase() / 2);
                //notify SectorManager
            }
            else{
                SpawnDefenders(GameManagerScript.GetStagePhase());
                //notify SectorManager
            }
        }

        void SpawnDefenders(int n)
        {
            for (int i = 0; i < n; i++)
            {
                GameObject newDefender = GameObject.Instantiate(GameManagerScript.DefenderPrefab(), GetSpawnPoint(), Quaternion.identity);
                DefendingAggressorScript dScript = newDefender.GetComponent<DefendingAggressorScript>();
                activeDefenders.Add(newDefender.name, dScript);
                dScript.isAllied = isAllied.Value;
                dScript.healthBar = BasicAggressorScript.maxHealth;
                dScript.SetSector(gameObject);
            }
        }

        void SuccesfulDefence()
        {
            //EVENT - communicate to player

        }

        public void Deactivate()
        {
            myAnimator.SetTrigger("deactivate");

            if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Active"))
            {
                DeactivateAggressors();
            }
        }

        public void Activate()
        {
            myAnimator.SetTrigger("activate");

            ActivateAggressors();
        }

        public void Automate()
        {
            myAnimator.SetTrigger("automate");

            if(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Active"))
            {
                DeactivateAggressors();
            }
        }

        void ActivateAggressors()
        {
            foreach (string name in activeDefenders.Keys)
            {
                activeDefenders[name].GetComponent<BasicAggressorScript>().Activate();
            }

            foreach (string name in activeRaiders.Keys)
            {
                activeRaiders[name].GetComponent<BasicAggressorScript>().Activate();
            }
        }

        void DeactivateAggressors()
        {
            foreach (string name in activeDefenders.Keys)
            {
                activeDefenders[name].GetComponent<BasicAggressorScript>().Deactivate();
            }

            foreach (string name in activeRaiders.Keys)
            {
                activeRaiders[name].GetComponent<BasicAggressorScript>().Deactivate();
            }
        }

        public Dictionary<string,BasicAggressorScript> GetRaiders()
        {
            return activeRaiders;
        }

        public Dictionary<string, BasicAggressorScript> GetDefenders()
        {
            return activeDefenders;
        }
    }
}