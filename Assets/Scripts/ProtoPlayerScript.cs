using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class ProtoPlayerScript : MappableObjectScript
    {
        public GameObject mySector;
        public float MovementSpeed = 500;
        public float maxMagnitude = 1f;
        public int standardDamage = 5;

        public static ProtoPlayerScript PlayerInstance;
        public static int sendThroughGate;

        private BasicSectorScript mySectorScript;
        private Rigidbody myRB;
        private Animator myAnim;
        private float movementRate;
        private float strafeRate;
        private Vector3 movementVector;
        private Vector3 attackDimensions;
        
        void OnEnable()
        {
            mySectorScript = mySector.GetComponent<BasicSectorScript>();
            myAnim = GetComponent<Animator>();
            attackDimensions = new Vector3(1f, 0.25f, 1f);
            movementVector = new Vector3();

            if (PlayerInstance == null)
            {
                PlayerInstance = this;
            }

            myRB = GetComponent<Rigidbody>();
        }
        

        void Update()
        {
            ReadActionInput();

            if (transform.position.magnitude > 100)
            {
                Time.timeScale = 0;
            }
        }

        private void FixedUpdate()
        {

            ResetVelocity();

            ReadMovementInput();

            ApplyMovement();
        }

        void ResetVelocity()
        {
            myRB.AddForce(-myRB.velocity, ForceMode.VelocityChange);
        }

        void ReadMovementInput()
        {
            movementRate = Input.GetAxis("Vertical");
            strafeRate = Input.GetAxis("Horizontal");

            float frameTime = Time.deltaTime;

            float prevX = movementVector.x;

            movementVector = (Vector3.right * strafeRate + Vector3.forward * movementRate) * MovementSpeed * frameTime;
        }

        void ReadActionInput()
        {
            if (Input.GetButtonDown("Jump"))
            {
                Collider[] attacked = Physics.OverlapBox(transform.position + transform.forward, attackDimensions, Quaternion.Euler(0f, 45f, 0f));

                for (int i = 0; i < attacked.Length; i++)
                {
                    if (attacked[i].tag == "Enemy")
                    {
                        BasicAggressorScript enemyScript = attacked[i].GetComponent<BasicAggressorScript>();

                        if (enemyScript != null)
                        {
                            enemyScript.TakeDamage(standardDamage);
                        }

                        Debug.Log(attacked[i].name);
                    }
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(SendTimer());
            }
        }

        System.Collections.IEnumerator SendTimer()
        {
            for (sendThroughGate = 20; sendThroughGate > 0; --sendThroughGate)
            {
                yield return null;
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

        void ApplyActions()
        {

        }

        public void SetSector(GameObject newSector)
        {
            mySector = newSector;
        }
    }
}
