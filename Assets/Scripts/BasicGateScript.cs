using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class BasicGateScript : MappableObjectScript
    {
        public GameObject otherGate;
        public GameObject theSector;

        private BasicSectorScript mySectorScript;
        private Vector3 SpawnPoint;

        public void OnEnable()
        {
            mySectorScript = theSector.GetComponent<BasicSectorScript>();
            SpawnPoint = new Vector3(transform.position.x, 1, transform.position.z);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.name == "ProtoPlayer") 
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Debug.Log("Fire in the trigger");
                    SendPlayer();
                }
            }
        }

        private void SendPlayer()
        {
            mySectorScript.Automate();
            otherGate.GetComponent<BasicGateScript>().ReceivePlayer();
            Debug.Log("Sent to " + theSector.name);
        }

        public void ReceivePlayer()
        {
            Debug.Log("received in " + theSector.name);
            mySectorScript.Activate();
            ProtoPlayerScript.PlayerInstance.transform.position = SpawnPoint;
            ProtoPlayerScript.PlayerInstance.SetSector(theSector);
        }
    }
}
