using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{

    private float zRange;
    [SerializeField] private float speed = 10.0f;

    [SerializeField] private GameObject com;
    [SerializeField] private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        zRange = manager.horizontalBound;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.z) > zRange)
        {
            speed *= -1;
            //revertForce *= -1;
            //myRb.velocity = Vector3.zero;
            //myRb.angularVelocity = Vector3.zero;
            //myRb.AddForce(new Vector3(0.0f, 0.0f, speed * Time.deltaTime),ForceMode.VelocityChange);
        }
        transform.Translate(0.0f, 0f, speed * Time.deltaTime);

        //transform.Translate(0.0f, 0f, speed * Time.deltaTime);
        //myRb.AddForce(new Vector3(0.0f,0.0f,speed * Time.deltaTime));
    }
}
