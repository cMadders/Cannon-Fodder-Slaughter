using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText;

    private int Count = 0;

    public ParticleSystem goalParticles;
    public GameManager manager;

    private void Start()
    {
        Count = 0;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Count += 1;
        CounterText.text = "Count : " + Count;
        manager.currentScore++;

        Instantiate(goalParticles, transform.position,transform.rotation);
    }
}
