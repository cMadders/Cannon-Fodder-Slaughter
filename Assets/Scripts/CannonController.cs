using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;

    private float movementSpeed = 15.0f;
    private float xRange;
    private float rotationSpeed = 20.0f;
    private float power = 0.0f;
    private float powerIncrementer = 100.0f;
    [SerializeField] private float maxPower = 1000.0f;

    // private float rotationYRange = 60.0f; Eventually prevent full cannon rotation


    public GameObject activeCannon;
    public GameObject basicShot;
    public GameObject mirvShot;
    public GameObject firePoint;
    private GameObject activeShot;
    [SerializeField] private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        xRange = manager.horizontalBound;
        activeShot = basicShot;
        power = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // ABSTRACTION - Moved movement and firing code to their own methods

        HandleMovement();

        // Check if game is over
        if (manager.checkEndStatus())
        {
            return;
        }

        HandleFiring();
    }

    void HandleMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        float rangeCheck = transform.position.x;

        transform.Translate(new Vector3(horizontalInput * movementSpeed * Time.deltaTime, 0.0f, 0.0f));

        if (Mathf.Abs(transform.position.z) > rangeCheck)
        {
            // add 1.02f to the range check to account for occasional bug of getting stuck at range value
            transform.position = new Vector3(transform.position.x, transform.position.y, rangeCheck * 1.02f);
        }

        // TO DO - Implement rotation movement cap
        //if(!transform.rotation.y > rotationYRange)
        //{

        //}

        activeCannon.transform.Rotate(Vector3.left, verticalInput * rotationSpeed * Time.deltaTime);

    }

    void HandleFiring()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (power <= maxPower)
            {
                power += Time.deltaTime * powerIncrementer;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) && power > 2)
        {
            if (manager.currentAmmo % 5 == 0 && manager.currentAmmo != manager.startingAmmo)
            {
                activeShot = mirvShot;
            }

            manager.shotFired();

            GameObject newShot = Instantiate(activeShot, firePoint.transform.position, Quaternion.identity);
            
            newShot.GetComponent<Rigidbody>().AddForce(new Vector3(-power, power, 0.0f), ForceMode.Impulse);

            //newShot.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * power,ForceMode.Impulse);
            //newShot.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * power, ForceMode.Impulse);

            ResetValues();

        }
    }

    // Ensure any special values are returned to base values after performing action
    void ResetValues()
    {
        power = 0;
        activeShot = basicShot;
    }
}
