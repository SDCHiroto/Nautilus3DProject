using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    public static AnimationsManager instance;

    [Header("Ref")]
    [SerializeField] Animator anim;

    [Header("Animator Variables")]
    public float anim_velocity;

     void Awake(){
        if(instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    void Start(){
        anim = GetComponent<Animator>();
    }

    private void Update() {
        
    }


}
