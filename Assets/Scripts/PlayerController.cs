using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isAlive = true;

    public GameObject gameOverText;
    public bool isGrounded = true;
    public int maxJumps = 2;
    public int currentJumps = 0;
    public int lives = 3;
    public float RunSpeed;           // forward speed
    public float HorizontalSpeed;    // left and right speed
    public Rigidbody rb;             // Rigidbody component
    float horizontalInput;
    public int position = 0; // 0 al centro, -1 a sx, 1 a dx
    public int proiettili = 3;
    public bool ricarica = false;
    public float tempoRicarica = 2f;
    public float timerRicarica = 0f;
    public GameObject sphere;

    [SerializeField] private float JumpForce = 10;
    [SerializeField] private LayerMask GroundMask;

    private Renderer[] renderers;//lo utilizzo per salvare tutti i figli del Player


    private void Start() //eseguito prima di Update e dopo Awake
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            /*
                        Vector3 newVelocity = new Vector3(horizontalInput * HorizontalSpeed, rb.linearVelocity.y, RunSpeed);
                        rb.linearVelocity = newVelocity;
            */
            Vector3 forwardMovement = transform.forward * RunSpeed * Time.fixedDeltaTime;
            Vector3 newposition = new Vector3(position*3, transform.position.y, transform.position.z);
            rb.MovePosition(newposition + forwardMovement );

        }


    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        float playerHeight = GetComponent<Collider>().bounds.size.y;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.3f, GroundMask);

        if (Input.GetKeyDown(KeyCode.Space) && isAlive && isGrounded)
        {
            currentJumps = 0;
            Jump();
            isGrounded = false;
            currentJumps++;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isAlive && !isGrounded && currentJumps < maxJumps)
        {
            Jump();
            currentJumps++;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            switch (position)
            {
                case -1: break;
                case 0: position = -1; break;
                case 1: position = 0; break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            switch (position)
            {
                case 1: break;
                case 0: position = 1; break;
                case -1: position = 0; break;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && isAlive)
        {
            if (!ricarica)
            {
                spara();
                proiettili--;
                if (proiettili == 0)
            {
                ricarica = true;
            }
            }
            
        }
        if (ricarica)
        {
            timerRicarica += Time.deltaTime;
            //Debug.Log(timerRicarica);
            if (timerRicarica >= tempoRicarica)
            {
                Debug.Log("Sono qui!");
                timerRicarica = 0;
                ricarica = false;
                proiettili = 3;
            }
        }



    }

    public void spara()
    {
        Debug.Log("Sparo!");
        Vector3 pos = new Vector3(rb.position.x, rb.position.y + 1, rb.position.z + 1);
        GameObject sp = Instantiate(sphere, pos, Quaternion.identity);
   

        

        
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle1") || other.CompareTag("Obstacle2"))
        {

            IgnoreCollisionBetweenPlayerAndObstacle(this.gameObject, other.gameObject);

            if (lives - 1 != 0)
            {
                lives--;
                StartCoroutine(FlashRoutine(1.0f, 0.2f));
                Debug.Log("Oops.. Ti restano " + lives + " vite");
            }
            else
            {
                Debug.Log("GAME OVER!!!");
                Dead();
            }
        }
    }
    

    void IgnoreCollisionBetweenPlayerAndObstacle(GameObject player, GameObject obstacle)
    {
        Collider[] playerColliders = player.GetComponentsInChildren<Collider>();
        Collider[] obstacleColliders = obstacle.GetComponentsInChildren<Collider>();

        foreach (Collider pCol in playerColliders)
        {
            foreach (Collider oCol in obstacleColliders)
            {
                Physics.IgnoreCollision(pCol, oCol, true);
            }
        }
    }





    public void Dead()
    {
        isAlive = false;
        gameOverText.SetActive(true);
    }


    private IEnumerator FlashRoutine(float duration, float flashSpeed)  //è una coroutine perché è capace di sospendersi e riprendersi nel tempo
    {
        Debug.Log("FlashRoutine");
        float elapsed = 0f;     //tempo trascorso
        bool visible = true;

        while (elapsed < duration)  //il tempo trascorso dev'essere minore della durata del lampeggio
        {
            visible = !visible;

            foreach (Renderer r in renderers) //per tutti i renderers raccolti
            {
                r.enabled = visible;    //abilita/disabilita la visibilità
            }

            elapsed += flashSpeed; //attende flashSpeed prima di ripetere
            yield return new WaitForSeconds(flashSpeed);
        }


        foreach (Renderer r in renderers)
        {
            r.enabled = true; //alla fine vengono mostrati nuovamente tutti i renderers
        }
    }
 










}
