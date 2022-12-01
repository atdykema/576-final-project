using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mungo : MonoBehaviour
{
    bool has_won;
    private CharacterController cc;
    int velocity;
    private float horizVelocity;
    // Start is called before the first frame update
    void Start()
    {
        has_won = false;
        cc = GetComponent<CharacterController>();
        velocity = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!has_won){

            horizVelocity += (float)(Time.deltaTime * -9.81);

            if(Input.GetKey(KeyCode.LeftArrow))
            {  
                transform.Rotate(new Vector3(0.0f, -0.5f, 0.0f));
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(new Vector3(0.0f, 0.5f, 0.0f));
            }

            float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
            float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
            Vector3 movement_direction = new Vector3(xdirection, 0.0f, zdirection);

            if(Input.GetKey(KeyCode.UpArrow))
            {  
                velocity = 100;
            }
            else if(Input.GetKey(KeyCode.DownArrow))
            {
                velocity = -100;
            }
            else{
                velocity = 0;
            }

            if(cc.isGrounded){
                if(Input.GetKey(KeyCode.Space))
                {  
                    horizVelocity = 5;
                }
            }

            movement_direction.y = (float)horizVelocity;

            cc.Move(movement_direction * Time.deltaTime * velocity);

        }
    }
}
