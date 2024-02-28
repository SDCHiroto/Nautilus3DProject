using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRecognization : MonoBehaviour
{
    public static DrawRecognization instance;

    [Header("Triggers")]
    [SerializeField] DrawTrigger[] trigger;
    [SerializeField] public bool[] triggerActivated = new bool[9];
    [SerializeField] public string orderCombination = "";

    void Awake(){
        if(instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Update() {
    
    }

    public void RecognizeSymbol(){
        if(orderCombination == "1236"){
            Debug.Log("BRAVASSANTO");
        }
    }

    public void Reset(){
        for(int i = 0; i < triggerActivated.Length; ++i){
            triggerActivated[i] = false;
            orderCombination = "";
        }



    }




}
