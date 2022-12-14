using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mungo : MonoBehaviour
{
    bool has_won;
    private CharacterController cc;
    int velocity;
    int walk_velocity;
    private float horizVelocity;
    private bool dubJump;

    // Fields used for inventory
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;
    
    // can be level 1,2,3
    public int level = 1;

    private void Awake()
    {
        inventory = new Inventory();
        inventory.level = level;
        uiInventory.SetInventory(inventory);
    }

    // Start is called before the first frame update
    void Start()
    {
        has_won = false;
        cc = GetComponent<CharacterController>();
        walk_velocity = 100;
        dubJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        // display the inventory
        if (Input.GetKeyDown("tab")) {
            ShowMungoInventory();
        }

        if(!has_won){

            horizVelocity += (float)(Time.deltaTime * -9.81);

            if(Input.GetKey(KeyCode.LeftArrow))
            {  
                transform.Rotate(new Vector3(0.0f, -1f, 0.0f));
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(new Vector3(0.0f, 1f, 0.0f));
            }

            float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
            float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
            Vector3 movement_direction = new Vector3(xdirection, 0.0f, zdirection);

            if(Input.GetKey(KeyCode.UpArrow))
            {  
                /*
                if(Input.GetKey(KeyCode.Tab)){
                    velocity = walk_velocity;
                }else{
                    velocity = walk_velocity*3;
                }*/

                velocity = walk_velocity*3;
            }
            /*
            else if(Input.GetKey(KeyCode.DownArrow))
            {
                velocity = -100;
            }
            */
            else{
                velocity = 0;
            }

            if(cc.isGrounded){
                horizVelocity = 0f;
                dubJump = false;
                if(Input.GetKey(KeyCode.Space))
                {  
                    horizVelocity = 4;  
                }
            }

            if(!cc.isGrounded && !dubJump){
                if(Input.GetKey(KeyCode.Tab))
                {  
                    cc.Move(movement_direction * velocity);
                    dubJump = true;
                }
            }

            movement_direction.y = (float)horizVelocity;

            cc.Move(movement_direction * Time.deltaTime * velocity);

        }
    }

    public void AddItemToInventory(string itemName) {
        switch (itemName) {
            default:
            case "Apple":         
                inventory.AddItem(new Item { itemType = Item.ItemType.Apple, amount = 1, level = level });
                break;
            case "Eggs":         
                inventory.AddItem(new Item { itemType = Item.ItemType.Eggs, amount = 1, level = level });
                break;
            case "Cake":         
                inventory.AddItem(new Item { itemType = Item.ItemType.Cake, amount = 1, level = level });
                break;
            case "Avacado":         
                inventory.AddItem(new Item { itemType = Item.ItemType.Avacado, amount = 1, level = level });
                break;
            case "Strawberry":         
                inventory.AddItem(new Item { itemType = Item.ItemType.Strawberry, amount = 1, level = level });
                break;
            case "PurpleFood":    
                inventory.AddItem(new Item { itemType = Item.ItemType.PurpleFood, amount = 1, level = level });
                break;
        }
        uiInventory.RefreshInventoryItems();
    }

    public void RemoveItemFromInventory(Item item) {
        inventory.RemoveItem(item);
        uiInventory.RefreshInventoryItems();
    }

    public void ShowMungoInventory()  {
        Animator animator = uiInventory.GetComponent<Animator>();
        if(animator != null) {
            bool isShowing = animator.GetBool("show");
            animator.SetBool("show", !isShowing);
        }
    }
}
