using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestisce il pooling degli oggetti per evitare l'allocazione e la distruzione frequente di oggetti durante il gioco
public class PoolingManager : MonoBehaviour
{
    // Singleton per accedere a questa classe da altre parti del gioco
    public static PoolingManager instance;

    // Lista degli oggetti in pool
    public List<GameObject> pooledObjects;

    // Oggetto da mettere in pool
    public GameObject objectToPool;

    // Numero iniziale di oggetti da mettere in pool
    public int amountToPool;


    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }


    void Start()
    {
        // Inizializzazione della lista di oggetti in pool
        pooledObjects = new List<GameObject>();

        // Creazione degli oggetti in pool
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false); // Disattiva gli oggetti appena creati
            pooledObjects.Add(tmp); // Aggiunge gli oggetti alla lista di oggetti in pool
        }
    }

    // Ottiene un oggetto dalla pool
    public GameObject GetPooledObject(){
        for(int i = 0; i < amountToPool; i++)
        {
            // Se l'oggetto non è attivo (non è in uso)
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i]; // Restituisce l'oggetto dalla pool
            }
        }
        return null; // Restituisce null se non ci sono oggetti disponibili nella pool
    }
}
