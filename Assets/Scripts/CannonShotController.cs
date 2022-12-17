using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShotController : MonoBehaviour
{
    public ParticleSystem explosionParticles;
    public GameManager manager;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(RemoveSelfRoutine());
        }
    }

    IEnumerator RemoveSelfRoutine()
    {
        yield return new WaitForSeconds(2);
        ParticleSystem explosion = Instantiate(explosionParticles,transform.position,transform.rotation);
        GameObject[] shotsOnField = GameObject.FindGameObjectsWithTag("Projectile");

        if (manager.checkEndStatus() && shotsOnField.Length <= 1)
        {
            manager.GameOver();
        }

        Destroy(gameObject);
    }

}
