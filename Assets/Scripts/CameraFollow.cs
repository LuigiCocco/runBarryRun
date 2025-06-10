using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Il riferimento al personaggio
    Vector3 offset;  // Distanza tra la telecamera e il personaggio
    public bool firstPerson = false;

    private void Awake()
    {
        // Trova automaticamente il personaggio nella scena
        player = FindObjectOfType<PlayerController>().transform;
    }

    void Start()
    {
        // Calcola l'offset tra la posizione iniziale della telecamera e quella del player
        offset = transform.position - player.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            firstPerson = !firstPerson;

        Vector3 targetpos;
        // Calcola la posizione desiderata della telecamera
        if (!firstPerson)
            targetpos = player.position + offset;
        else
            targetpos = player.position + new Vector3(0,1.8f,0);
        
        // Fissa la telecamera solo sull'asse Y (così la telecamera non si sposterà orizzontalmente)
            //targetpos.x = 0;

            // Muovi la telecamera verso la posizione calcolata
            transform.position = targetpos;
    }
}
