using System.Collections;
using UnityEngine;

public class Obstacles : MonoBehaviour
{




    public void OnTriggerEnter(Collider other)
    {

        StartCoroutine(BlinkRed(10, 0.1f));

    
    }

private IEnumerator BlinkRed(int times, float interval)
{
    Debug.Log("Sono blink");
    Renderer rend = GetComponentInChildren<Renderer>();
    if (rend == null)
    {
        Debug.Log("NULLO");
        yield break;
    }

    // Crea una copia del materiale per evitare bug
    rend.material = new Material(rend.material);

    Color originalColor = rend.material.color;
    Color redColor = Color.red;

    for (int i = 0; i < times; i++)
    {
        Debug.Log("Blinking " + i);
        rend.material.color = redColor;
        yield return new WaitForSeconds(interval);
        rend.material.color = originalColor;
        yield return new WaitForSeconds(interval);
    }
    rend.material.color = originalColor;
    yield return new WaitForSeconds(0.1f);  // piccolo delay di sicurezza
    Destroy(this.gameObject);
}
}
