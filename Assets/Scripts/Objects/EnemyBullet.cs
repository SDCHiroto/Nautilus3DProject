using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float lifeTime = 3f;

    private float timer;

    private void Start()
    {
        timer = lifeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime; 

        if (timer <= 0f)
        {
            Destroy(this.gameObject);;
            return;
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("Controlled"))
        {
            other.GetComponent<Controllable>().GetDamage();
        }
        
        Destroy(this.gameObject);
    }
}