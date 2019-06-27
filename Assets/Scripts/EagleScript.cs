using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleScript : Player {

   
    public float Enemyspeed;
    Rigidbody2D rb;
    public Vector2 target;
    
    //public GameObject enemy;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        
        
        if (Player.instace.id == 1)
        {
            
            target.Set(-5, 0);
        }
        if (Player.instace.id == 2)
        {
            
            target.Set(-12,-2);
        }
        if(Player.instace.id == 3) {
            
            target.Set(-15, -2);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        float step = Enemyspeed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, target, step);

       // switch(gameObject.name){
           // case "eagle":
                /*break;
            case "opossum":
                if (transform.position.y < -3)
                {
                    Debug.Log("Good bye everybody, i've got to go!");
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }*/
        
    }
}
