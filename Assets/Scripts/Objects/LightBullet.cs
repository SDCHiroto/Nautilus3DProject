using UnityEngine;

public class LightBullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float lifeTime = 3f;

    private float timer;

    private void OnEnable()
    {
        timer = lifeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime; 

        if (timer <= 0f)
        {
            this.gameObject.SetActive(false);
            return;
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.TryGetComponent(out IPowerable objHitted))
        {
            objHitted.Power();
        }
        this.gameObject.SetActive(false);
    }
}
