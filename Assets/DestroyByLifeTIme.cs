using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByLifeTIme : MonoBehaviour
{

    public float lifeTIme;
    //int i=0;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,lifeTIme);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
