using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovCharacter : MonoBehaviour
{
    public Transform objetivo;
    public float speed = 10.0f;
    public Rigidbody2D rb;
    private Animator anim;

    private Vector2 direction = Vector2.zero;
    private BoxCollider2D boxCollider;
    public GameObject finalCueva;
    public GameObject panelFinal;

    public float fadeInDuration = 2f;  // Duración del fade-in en segundos
    public float fadeOutDuration = 2f; // Duración del fade-out en segundos
    public float delayTime = 1f;       // Tiempo de espera antes de iniciar el fade-out

    public GameObject panel;
    private int llamado = 0;

    public Transform teleportLocation;

    public void TeleportTo()
    {
        transform.position = teleportLocation.position;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        panel.SetActive(false); // Asegurarse de que el panel esté desactivado al inicio

    }
    public void StartFadeInAndOut()
    {
        llamado = 1;
        StartCoroutine(FadeInAndOutCoroutine());
    }

    private IEnumerator FadeInAndOutCoroutine()
    {
        // Fade-in
        panel.SetActive(true);
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = panel.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;

        float timer = 0f;
        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeInDuration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        // Esperar antes de iniciar el fade-out
        yield return new WaitForSeconds(delayTime);

        // Fade-out
        timer = 0f;
        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeOutDuration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        panel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            direction.x = Input.GetAxis("Horizontal");
            direction.y = 0;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            direction.y = Input.GetAxis("Vertical");
            direction.x = 0;
        }
        else 
        {
            direction.x = 0;
            direction.y = 0;
        };
        if (PlayerPrefs.GetInt("npcs") == 2)
        {
            MoverFinal();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    public void MoverFinal()
    {

        speed = 15.0f;

        if (llamado == 0)
        {
            StartFadeInAndOut();
        }
        boxCollider.enabled = false;
        //TeleportTo();

        anim.SetBool("arriba", true);
        // Calcular la dirección hacia el objetivo
        Vector3 direccion = (objetivo.position - transform.position).normalized;

        // Calcular la distancia entre el jugador y el objetivo
        float distancia = Vector3.Distance(transform.position, objetivo.position);

        // Si aún no ha alcanzado el objetivo, mover al jugador hacia él
        if (distancia > 0.1f)
        {
            rb.MovePosition(transform.position + direccion * speed * Time.deltaTime);
        }
        if (distancia < 8f)
        {
            finalCueva.SetActive(false);
        }
        if (distancia < 4f)
        {
            panelFinal.SetActive(true);
        }

    }
}
