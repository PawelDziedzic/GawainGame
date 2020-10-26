using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class ProtoPlayerScript : MappableObjectScript
    {
        public GameObject mySector;
        public static ProtoPlayerScript PlayerInstance;
        public float MovementSpeed;
        public GameObject attackPrefab;

        private BasicSectorScript mySectorScript;
        private Rigidbody myRB;
        private Animator myAnim;
        private float movementRate;
        private float strafeRate;
        private Vector3 movementVector;
        private Vector3 directionVector;
        
        void OnEnable()
        {
            mySectorScript = mySector.GetComponent<BasicSectorScript>();
            myAnim = GetComponent<Animator>();

            if (PlayerInstance == null)
            {
                PlayerInstance = this;
            }

            myRB = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            myRB.AddForce(-myRB.velocity, ForceMode.VelocityChange);

            ReadMovementInput();

            ReadActionInput();

            ApplyMovement();
        }

        void ReadMovementInput()
        {
            movementRate = Input.GetAxis("Vertical");
            movementRate *= Time.deltaTime;

            strafeRate = Input.GetAxis("Horizontal");
            strafeRate *= Time.deltaTime;

            movementVector = (Vector3.right * strafeRate + Vector3.forward * movementRate).normalized * MovementSpeed;
        }

        void ReadActionInput()
        {
            if (Input.GetButtonDown("Jump"))
            {
                GameObject attack = Instantiate(attackPrefab, transform.position + transform.forward, Quaternion.identity);
                attack.transform.Rotate(Vector3.up, 45);
            }
        }

        void ApplyMovement()
        {
            if (movementVector.magnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(movementVector);
            }
            myRB.AddForce(movementVector, ForceMode.Acceleration);
        }

        public void SetSector(GameObject newSector)
        {
            mySector = newSector;
        }
    }
}
