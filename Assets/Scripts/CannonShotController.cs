using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShotController : MonoBehaviour
{
    [SerializeField] protected ParticleSystem explosionParticles;
    [SerializeField] private GameManager manager;
    protected float timeTillDeath = 1.0f;


    public AudioSource shotSource;
    public AudioClip shotSound;
    public AudioClip groundSound;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        shotSource = GetComponent<AudioSource>();

        Debug.Log(shotSound.length);
        shotSource.PlayOneShot(shotSound,50.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            shotSource.PlayOneShot(groundSound,25.0f);
            StartCoroutine(RemoveSelfRoutine());
        }
    }

    // Any child object will have a special action that must take place
    protected virtual void DoSpecialAction()
    {
    }

    protected virtual IEnumerator RemoveSelfRoutine()
    {
        yield return new WaitForSeconds(timeTillDeath);
        ParticleSystem explosion = Instantiate(explosionParticles,transform.position,transform.rotation);
        GameObject[] shotsOnField = GameObject.FindGameObjectsWithTag("Projectile");

        if (manager.checkEndStatus() && shotsOnField.Length <= 1)
        {
            manager.GameOver();
        }

        Destroy(gameObject);
    }

}
