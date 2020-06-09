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
        public GameObject mySector;
        bool playerIsIn;


        private void OnTriggerStay(Collider other)
        {
            if(!playerIsIn && other.gameObject.name == "Player")
            {
                playerIsIn = true;
            }
        }

        private void SendPlayer()
        {
            //turn off my sector
            //player starts flying
            otherGate.GetComponent<BasicGateScript>().ReceivePlayer();
        }

        public void ReceivePlayer()
        {
            //turn on my sector
            //spawn player
        }
    }
}
