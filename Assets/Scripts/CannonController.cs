using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private float movementSpeed = 15.0f;
    private float xRange = 15.0f;
    private float rotationYRange = 60.0f;
    public float rotationSpeed = 20.0f;
    public float power = 0.0f;
    public float maxPower = 50.0f;
    public float powerIncrementer = 2.0f;


    public GameObject activeCannon;
    public GameObject activeShot;
    public GameObject firePoint;
    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        xRange = manager.horizontalBound;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        float rangeCheck = transform.position.x;

        transform.Translate(new Vector3(horizontalInput * movementSpeed * Time.deltaTime, 0.0f, 0.0f));

        if(Mathf.Abs(transform.position.z) > rangeCheck)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y,rangeCheck);
        }

        //if(!transform.rotation.y > rotationYRange)
        //{

        //}

        activeCannon.transform.Rotate(Vector3.left,verticalInput * rotationSpeed * Time.deltaTime);


        if (manager.checkEndStatus())
        {
            return;
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            if(power <= maxPower)
            {
                power += Time.deltaTime * powerIncrementer;
            }
        }else if (Input.GetKeyUp(KeyCode.Space) && power > 2)
        {
            manager.shotFired();
            GameObject newShot = Instantiate(activeShot, firePoint.transform.position, Quaternion.identity);
            newShot.GetComponent<Rigidbody>().AddForce(new Vector3(-power,power,0.0f),ForceMode.Impulse);
            //newShot.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * power,ForceMode.Impulse);
            //newShot.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * power, ForceMode.Impulse);
            power = 0;
        }

    }

}
