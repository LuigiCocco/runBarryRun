using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float velocity;
    public Rigidbody rb;
    public float tempo = 0.4f;
    public float xvelocity = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();
        xvelocity = playerController.currentMoveSpeed;
        Debug.Log(xvelocity);
        rb.linearVelocity = new Vector3(xvelocity, 0, 50);
        Destroy(gameObject, tempo);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
    //Debug.Log("Sono onCollision");
    //Debug.Log(other.tag);

        if (other.CompareTag("Obstacle1") || other.CompareTag("Obstacle2"))
        {
           
         
           
            Destroy(gameObject);

        }
        
    }

 








}
