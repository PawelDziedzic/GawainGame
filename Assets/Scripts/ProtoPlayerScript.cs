using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class ProtoPlayerScript : MappableObjectScript
    {
        public GameObject mySector;
        BasicSectorScript mySectorScript;
        public GameObject myInstance;

        // Start is called before the first frame update
        void Start()
        {
            mySectorScript = mySector.GetComponent<BasicSectorScript>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
