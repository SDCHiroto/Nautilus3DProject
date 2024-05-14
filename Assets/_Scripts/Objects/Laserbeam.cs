using UnityEngine;

public class Laserbeam : MonoBehaviour, IPowerable, IActivable
{
    [SerializeField] float maxLength = 50f; // Lunghezza massima del raggio laser
    [SerializeField] LayerMask whatIsGround; // Maschera per definire cosa è considerato terreno
    [SerializeField] bool isStartingLaserbeam = false; // Flag che indica se questo è un raggio laser iniziale

    [Header("References")]
    [SerializeField] GameObject centerParticle; // Il prefab dei particle presenti al centro del laser

    private LineRenderer lineRenderer; // Riferimento al componente LineRenderer
    private bool isActive = false; // Flag che indica se il raggio laser è attivo
    private bool isRotating = false; // Flag che indica se il raggio laser è in rotazione

    Laserbeam laserHitted; // Raggio laser colpito
    Button buttonHitted = null; // Pulsante colpito
    Animator anim; // Riferimento all'Animator
    AudioSource audioSource;

    private void Awake() {
        anim = GetComponent<Animator>(); // Ottiene il riferimento all'Animator del GameObject
        audioSource = GetComponent<AudioSource>(); // Ottiene il riferimento all'AudioSource del GameObject
    }

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

        Color color = new Color(0, 1, 1, 1);
        lineRenderer.startColor = color; lineRenderer.endColor = color; // Imposta il colore del raggio laser
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
            else {
                centerParticle.SetActive(false);
                if(laserHitted != null && !laserHitted.isStartingLaserbeam){
                    centerParticle.SetActive(false);
                    laserHitted.Off(); // Disattiva il raggio laser colpito se non è un raggio laser iniziale
                    laserHitted = null;
                }
            }
        }
    }

    // Spara il raggio laser
    private void ShootLaser()
    {
        RaycastHit hit;

        if(!isRotating) {
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxLength))
            {
                lineRenderer.SetPosition(0, transform.position); // Imposta la posizione iniziale del raggio laser
                lineRenderer.SetPosition(1, hit.point); // Imposta la posizione finale del raggio laser dove ha colpito

                if(hit.collider.GetComponent<Laserbeam>() != null)  {
                    laserHitted = hit.collider.GetComponent<Laserbeam>(); // Memorizza il raggio laser colpito
                    laserHitted?.On(); // Attiva il raggio laser colpito
                } 
                else {
                    if(laserHitted != null && !laserHitted.isStartingLaserbeam){
                        laserHitted.Off(); // Disattiva il raggio laser colpito se non è un raggio laser iniziale
                        laserHitted = null;
                    }
                }

                // Se il laser colpisce un pulsante, lo attiva
                if (hit.collider.gameObject.TryGetComponent(out Button objHitted)){
                    objHitted.Activate();
                    buttonHitted = objHitted;
                } else if(buttonHitted != null) {
                    buttonHitted.Deactivate();
                    buttonHitted = null;
                }

                // Se il laser colpisce un nemico, lo elimina
                if(hit.collider.GetComponent<Enemy_GolemShoot>() != null){
                    hit.collider.GetComponent<Enemy_GolemShoot>().Die();
                }
            }
            else
            {
                lineRenderer.SetPosition(0, transform.position); // Imposta la posizione iniziale del raggio laser
                lineRenderer.SetPosition(1, transform.position + transform.forward * maxLength); // Imposta la posizione finale del raggio laser (massima lunghezza)

                if(laserHitted != null && !laserHitted.isStartingLaserbeam) {
                    laserHitted.Off(); // Disattiva il raggio laser colpito se non è un raggio laser iniziale
                    laserHitted = null;
                }

                if(buttonHitted != null){
                    buttonHitted.Deactivate();
                    buttonHitted = null;
                }
            }     
            if(!audioSource.isPlaying) {_PlayLaserClip();}
        } else {
            _StopPlayerClip();
            lineRenderer.SetPosition(0, transform.position); // Imposta la posizione iniziale del raggio laser
            lineRenderer.SetPosition(1, transform.position); // Imposta la posizione finale del raggio laser dove ha colpito
        }
    }

    // Ruota il raggio laser
    public void RotateLaserbeam(){
        anim.SetTrigger("Rotate"); // Imposta il trigger dell'animazione per la rotazione del raggio laser
    }

    // Evento dell'animazione
    void _DisableWhenRotating(){
        isRotating = !isRotating; // Inverte lo stato della rotazione del raggio laser
        if(laserHitted != null  && !laserHitted.isStartingLaserbeam) {
            laserHitted.Off(); // Disattiva il raggio laser colpito se non è un raggio laser iniziale
            laserHitted = null;
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
        Invoke("Off", 2f); // Chiama la funzione Off dopo 2 secondi
    }

    // Disattiva il raggio laser
    public void Off()
    {
        isActive = false; // Imposta il flag di attivazione del raggio laser su false
        _StopPlayerClip(); // Interrompe la riproduzione del suono del raggio laser
        lineRenderer.enabled = false; // Disabilita il LineRenderer
    }

    public void Activate()
    {
        RotateLaserbeam(); // Attiva la rotazione del raggio laser
    }

    public void Power()
    {
        OnWithBullet(); // Attiva il raggio laser con proiettile
    }

    // Riproduce il suono del raggio laser
    public void _PlayLaserClip(){
        audioSource.Play();
    }

    // Interrompe la riproduzione del suono del raggio laser
    public void _StopPlayerClip(){
        if(audioSource.isPlaying){
            audioSource.Stop();
        }
    }
}
