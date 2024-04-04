using System.Collections;
using UnityEngine;

public class Laserbeam : MonoBehaviour
{
    public float maxLength = 50f; // Lunghezza massima del raggio laser
    public LayerMask whatIsGround; // Maschera per definire cosa è considerato terreno
    public bool isStartingLaserbeam = false; // Flag che indica se questo è un raggio laser iniziale

    private LineRenderer lineRenderer; // Riferimento al componente LineRenderer
    private bool isActive = false; // Flag che indica se il raggio laser è attivo
    private bool isRotating = false; // Flag che indica se il raggio laser è in rotazione

    Laserbeam laserHitted; // Raggio laser colpito
    Gate gateHitted; // Cancello colpito

    Animator anim; // Riferimento all'Animator

    private void Awake() {
        anim = GetComponent<Animator>(); // Ottiene il riferimento all'Animator del GameObject
    }

    // Funzione chiamata all'avvio dello script
    private void Start()
    {
        CreateLineRenderer(); // Crea il LineRenderer

        // Se questo è un raggio laser iniziale, attivalo immediatamente
        if (isStartingLaserbeam)
        {
            Activate(); // Attiva il raggio laser
        }
        else
        {
            lineRenderer.enabled = false; // Disabilita inizialmente il LineRenderer
        }
    }

    // Crea il LineRenderer e lo configura
    private void CreateLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>(); // Aggiunge un LineRenderer al GameObject
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Imposta il materiale del LineRenderer
        lineRenderer.startWidth = 0.1f; // Imposta la larghezza iniziale del raggio
        lineRenderer.endWidth = 0.1f; // Imposta la larghezza finale del raggio
        lineRenderer.positionCount = 2; // Imposta il numero di punti del raggio (inizio e fine)
    }

    // Funzione chiamata ad ogni frame
    private void Update()
    {
        if(isActive){
            ShootLaser(); // Spara il raggio laser se è attivo
        } else {
            if(laserHitted != null && !laserHitted.isStartingLaserbeam){
                laserHitted.Deactivate(); // Disattiva il raggio laser colpito se non è un raggio laser iniziale
                laserHitted = null;
            }
        }

        if(Input.GetKeyDown(KeyCode.R)){
            RotateLaserbeam(); // Ruota il raggio laser
        }
    }

    // Spara il raggio laser
    private void ShootLaser()
    {
        RaycastHit hit;

        if(!isRotating) {
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxLength, whatIsGround))
            {
                lineRenderer.SetPosition(0, transform.position); // Imposta la posizione iniziale del raggio laser
                lineRenderer.SetPosition(1, hit.point); // Imposta la posizione finale del raggio laser dove ha colpito

                if(hit.collider.GetComponent<Laserbeam>() != null)  {
                    laserHitted = hit.collider.GetComponent<Laserbeam>(); // Memorizza il raggio laser colpito
                    laserHitted?.Activate(); // Attiva il raggio laser colpito
                } else {
                    if(laserHitted != null && !laserHitted.isStartingLaserbeam){
                        laserHitted.Deactivate(); // Disattiva il raggio laser colpito se non è un raggio laser iniziale
                        laserHitted = null;
                    }
                }

                if(hit.collider.GetComponent<Gate>() != null){
                    gateHitted = hit.collider.GetComponent<Gate>(); // Memorizza il cancello colpito
                    gateHitted?.Use(); // Usa il cancello colpito
                }
            }
            else
            {
                lineRenderer.SetPosition(0, transform.position); // Imposta la posizione iniziale del raggio laser
                lineRenderer.SetPosition(1, transform.position + transform.forward * maxLength); // Imposta la posizione finale del raggio laser (massima lunghezza)

                if(laserHitted != null && !laserHitted.isStartingLaserbeam) {
                    laserHitted.Deactivate(); // Disattiva il raggio laser colpito se non è un raggio laser iniziale
                    laserHitted = null;
                }
            }
        } else {
            lineRenderer.SetPosition(0, transform.position); // Imposta la posizione iniziale del raggio laser
            lineRenderer.SetPosition(1, transform.position); // Imposta la posizione finale del raggio laser dove ha colpito
        }
       
    }

    // Ruota il raggio laser
    void RotateLaserbeam(){
        anim.SetTrigger("Rotate"); // Imposta il trigger dell'animazione per la rotazione del raggio laser
    }

    // Evento dell'animazione
    void _DisableWhenRotating(){
        isRotating = !isRotating; // Inverte lo stato della rotazione del raggio laser
        if(laserHitted != null  && !laserHitted.isStartingLaserbeam) {
            laserHitted.Deactivate(); // Disattiva il raggio laser colpito se non è un raggio laser iniziale
            laserHitted = null;
        }
    }

    // Attiva il raggio laser
    public void Activate()
    {
        isActive = true; // Imposta il flag di attivazione del raggio laser su true
        lineRenderer.enabled = true; // Abilita il LineRenderer
    }

    // Disattiva il raggio laser
    public void Deactivate()
    {
        isActive = false; // Imposta il flag di attivazione del raggio laser su false
        lineRenderer.enabled = false; // Disabilita il LineRenderer
    }

}
