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

            if (PlayerInstance == null)
            {
                PlayerInstance = this;
            }

            myRB = GetComponent<Rigidbody>();
        }
        

        void Update()
        {
            myRB.AddForce(-myRB.velocity, ForceMode.VelocityChange);

            ReadMovementInput();

            ReadActionInput();

            ApplyMovement();

            if(transform.position.magnitude > 100)
            {
                Time.timeScale = 0;
            }
        }

        void ReadMovementInput()
        {
            movementRate = Input.GetAxis("Vertical");
            movementRate *= Time.deltaTime;

            //strafeRate = Input.GetAxis("Horizontal");
            strafeRate = Mathf.Sin(2 * Time.time);
            //strafeRate *= Time.deltaTime;

            movementVector = Vector3.ClampMagnitude
                (
                (Vector3.right * strafeRate + Vector3.forward * movementRate),
                maxMagnitude
                ) * MovementSpeed;

            Debug.Log(string.Format("name: {0}, strafe rate: {1}, movement vector x: {2}", gameObject.name, strafeRate, movementVector.x));
        }

        void ReadActionInput()
        {
            if (Input.GetButtonDown("Jump"))
            {
                Collider[] attacked = Physics.OverlapBox(transform.position + transform.forward, attackDimensions, Quaternion.Euler(0f, 45f, 0f));

                for (int i = 0; i< attacked.Length; i++)
                {
                    Debug.Log(attacked[i].name);
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(SendTimer());
            }
        }

        System.Collections.IEnumerator SendTimer()
        {
            for (sendThroughGate = 30; sendThroughGate > 0; --sendThroughGate)
            {
                Debug.Log("Send! " + sendThroughGate);
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

        public void SetSector(GameObject newSector)
        {
            mySector = newSector;
        }
    }
}
