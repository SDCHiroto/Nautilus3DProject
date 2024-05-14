using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float speed = 10f; // Velocità del proiettile
    [SerializeField] float lifeTime = 3f; // Tempo di vita del proiettile

    private float timer; // Timer per il conteggio del tempo di vita del proiettile

    private void Start()
    {
        timer = lifeTime; // Imposta il timer al tempo di vita iniziale
    }

    void Update()
    {
        timer -= Time.deltaTime; // Aggiorna il timer

        // Se il tempo di vita è scaduto, distruggi il proiettile e interrompi l'esecuzione della funzione
        if (timer <= 0f)
        {
            Destroy(this.gameObject);;
            return;
        }

        // Sposta il proiettile in avanti lungo il suo asse Z con una velocità costante
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Se il proiettile collide con un oggetto dotato del componente IDamageable, infliggi danni
        if (other.TryGetComponent(out IDamageable damageable)){
            damageable.GetDamage();
        }
        
        Destroy(this.gameObject); // Distruggi il proiettile dopo la collisione
    }
}
