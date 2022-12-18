using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot_MIRV : CannonShotController
{

    [SerializeField] private GameObject spreadShot;

    private float shotRadius = 3.0f;
    private float shotAmount = 6;

    // Start is called before the first frame update
    void Awake()
    {
        timeTillDeath = .25f;
        StartCoroutine(RemoveSelfRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void DoSpecialAction()
    {
        for(int i = 0;i < shotAmount; i++)
        {
            GameObject newShot = Instantiate(spreadShot, GenerateSpreadPosition() , transform.rotation);
            newShot.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
        }
    }

    private Vector3 GenerateSpreadPosition()
    {
        float newZ = Random.Range(-shotRadius, shotRadius);
        float newY = Random.Range(-shotRadius, shotRadius);

        return new Vector3(transform.position.x, transform.position.y + newY, transform.position.z + newZ);
    }

    protected override IEnumerator RemoveSelfRoutine()
    {
        yield return new WaitForSeconds(timeTillDeath);

        DoSpecialAction();

        ParticleSystem explosion = Instantiate(explosionParticles, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
