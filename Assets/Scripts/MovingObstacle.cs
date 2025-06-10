using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
        public Vector3 spostamento = new Vector3(180, 0, 0); // Quanto si sposta a destra/sinistra
    public float velocita = 3f;
    
    private Vector3 posizioneA;
    private Vector3 posizioneB;
    private Vector3 target;
    
    void Start()
    {
        posizioneA = transform.position; 
        posizioneB = transform.position + spostamento; 
        
        target = posizioneB; 
    }
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, velocita * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) == 0)
        {
            if (target == posizioneB)
            {
                target = posizioneA;
            }
            else
            {
                target = posizioneB;
            }
        }
    }
}
