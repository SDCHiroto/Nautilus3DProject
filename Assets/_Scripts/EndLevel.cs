using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{

    [SerializeField] GameObject loadingPanel;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            loadingPanel.SetActive(true);
        }
    }
}
