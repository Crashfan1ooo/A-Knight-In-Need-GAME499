using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private Rigidbody rb;

    [Tooltip("This will change how fast the player moves.")] public float moveSpeed = 6;
   
    [Tooltip("This will change how high the player jumps.")] public float jumpForce = 2;

    [Tooltip("Make sure to have this set to 'Ground'.")] public LayerMask layerMask;

    [HideInInspector] public bool grounded;

    public int Health = 5;
    public Text textbox;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //textbox = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //All of this is the code necessary for movement.
        float x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float y = Input.GetAxisRaw("Vertical") * moveSpeed;

        grounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), 0.4f, layerMask);

        

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        { 
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            
        }

        Vector3 movePos = transform.right * x + transform.forward * y;
        Vector3 newMovePos = new Vector3(movePos.x, rb.velocity.y, movePos.z);

        rb.velocity = newMovePos;

        if (Health <= 0)
        {
            Debug.Log("You are dead!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        textbox.text = "Health:" + Health;
       
    }
}
