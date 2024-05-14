using System.Collections;
using UnityEngine;

public class LaserbeamEnemy : MonoBehaviour
{
    [SerializeField] float maxLength = 50f; // Lunghezza massima del raggio laser
    [SerializeField] LayerMask whatIsGround; // Maschera per definire cosa è considerato terreno
    [SerializeField] bool isStartingLaserbeam = false; // Flag che indica se questo è un raggio laser iniziale

    [Header("References")]
    [SerializeField] GameObject centerParticle;

    private LineRenderer lineRenderer; // Riferimento al componente LineRenderer
    private bool isActive = false; // Flag che indica se il raggio laser è attivo

    Animator anim; // Riferimento all'Animator
    [SerializeField] LayerMask whatIsHittable;

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
            On(); // Attiva il raggio laser
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

        Color color = new Color(1, 0, 0, 1);
        lineRenderer.startColor = color; lineRenderer.endColor = color;
        lineRenderer.startWidth = 0.1f; // Imposta la larghezza iniziale del raggio
        lineRenderer.endWidth = 0.1f; // Imposta la larghezza finale del raggio
        lineRenderer.positionCount = 2; // Imposta il numero di punti del raggio (inizio e fine)
    }

    private void Update()
    {
        if(!GeneralManager.instance.isPaused){
            if(isActive){
                centerParticle.SetActive(true);
                ShootLaser(); // Spara il raggio laser se è attivo
            }
        }
        

    }

    // Spara il raggio laser
    private void ShootLaser()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxLength, whatIsHittable))
        {
            lineRenderer.SetPosition(0, transform.position); // Imposta la posizione iniziale del raggio laser
            lineRenderer.SetPosition(1, hit.point); // Imposta la posizione finale del raggio laser dove ha colpito

            if (hit.collider.gameObject.TryGetComponent(out MainCharacter damageable))
            {
                if(!damageable.isDead) {damageable.GetDamage();}
            }

        }
        else
        {
            lineRenderer.SetPosition(0, transform.position); // Imposta la posizione iniziale del raggio laser
            lineRenderer.SetPosition(1, transform.position + transform.forward * maxLength); // Imposta la posizione finale del raggio laser (massima lunghezza)
        }
       
    }


        // Attiva il raggio laser
    public void On()
    {
        isActive = true; // Imposta il flag di attivazione del raggio laser su true
        lineRenderer.enabled = true; // Abilita il LineRenderer
    }

    public void OnWithBullet()
    {
        isActive = true; // Imposta il flag di attivazione del raggio laser su true
        lineRenderer.enabled = true; // Abilita il LineRenderer
        Invoke("Off", 2f);
    }

    // Disattiva il raggio laser
    public void Off()
    {
        isActive = false; // Imposta il flag di attivazione del raggio laser su false
        lineRenderer.enabled = false; // Disabilita il LineRenderer
    }


}
