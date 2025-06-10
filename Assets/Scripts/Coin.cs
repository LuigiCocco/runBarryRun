using UnityEngine;

public class Coin : MonoBehaviour
{

    public float turnSpeed = 110f;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(turnSpeed * Time.deltaTime, 0, 0);
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             Destroy(gameObject);
             Debug.Log("Hai collezionato una nuova moneta");
        
        }
       
    }

}
