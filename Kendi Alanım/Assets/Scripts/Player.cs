using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Text text2;
    public float CurrentHealth = 100f;
    public float MaxHealth = 100f;
    public float score = 0f;
    //public float money = 500f;
    public float movementSpeed = 15f;
    public float jumpSpeed = 100f;
    private Rigidbody rb;
    [SerializeField] private GameObject Camera;
    CharacterController mycharacter;
    private Vector3 moveDirection = Vector3.zero;
    private float turner;
     private float looker;
     public float sensitivity = 5;
     public float gravity = 20;
    public Vector3 cameraDistance;
    public bool anahtarAlindiMi;

    private IEnumerator _attackCoroutine;
    //public Vector3 screenPosition;
    //public Vector3 worldPosition;

    public float mouseSpeed = 3;
    
    bool grounded = false;

    void Awake()
    {
        /*CurrentHealth = PlayerPrefs.GetFloat("Health");
        PlayerPrefs.GetFloat("Score", score);*/

    }
    void Start()
    {
        GetComponent<Text>();
        GameObject.Find("Main Camera");
        mycharacter = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Move()
    {

        //Feed moveDirection with input.
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        //Multiply it by speed.
        moveDirection *= movementSpeed;
        //Jumping
        
        if (Input.GetButtonDown("Jump") && grounded == false)
        {
            moveDirection.y = jumpSpeed * Time.deltaTime ;
        }
             
        //Applying gravity to the controller
        moveDirection.y -= 2;
        //Making the character move
        mycharacter.Move(moveDirection * Time.deltaTime);
    }


    void FixedUdate()
    {
       
        Camera.transform.position = new Vector3(transform.position.x + cameraDistance.x , transform.position.y + cameraDistance.y, transform.position.z + cameraDistance.z);
    }

    void Update()
    {
      
        
        //Debug.Log(grounded);
    
        float X = Input.GetAxis("Mouse X") * mouseSpeed;
        float Y = Input.GetAxis("Mouse Y") * mouseSpeed;

        transform.Rotate(0, X, 0);

        if (Camera.transform.eulerAngles.x + (-Y) > 80 && Camera.transform.eulerAngles.x + (-Y) < 280)
        { 
        
        }
        else
        {

            Camera.transform.RotateAround(transform.position, Camera.transform.right, -Y);
        }
        Move();
       
/*         float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

       
        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed) * Time.deltaTime; */

        
/*         if(Input.GetButtonDown("Jump"))
        {
            
            if(grounded == false)
            {

                rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
            }
        } */

        
        if(Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 20f;
        } else
        {
            movementSpeed = 15f;
        }



          if (anahtarAlindiMi == true)
        {    
            text2.gameObject.SetActive(true);
            text.gameObject.SetActive(false); 
        }
        else
        {
            text2.gameObject.SetActive(false);
        }
        if (CurrentHealth <= 0)
        {
            SceneManager.LoadScene("DeathScene");
        }
        /*PlayerPrefs.SetFloat("Score",score);
        PlayerPrefs.Save();*/
     
    }
    void OnCollisionStay(Collision other)
    {
        foreach (ContactPoint contact in other.contacts)
        {
            if (Vector2.Angle(contact.normal, Vector3.up) < 80)
            {
                grounded = false;
            }
        }
    }

    void OnCollisionExit()
    {
        grounded = true;
    }
   

    
    void HiziArttir()
    {
        movementSpeed = 30f;
    }
    private void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Anahtar")
        {
            if(Input.GetMouseButtonDown(0))
            {
                Destroy(col.gameObject);
                anahtarAlindiMi = true;

            }
           
            
        }
        if (col.gameObject.tag == "Kapı" && anahtarAlindiMi)
        {

            if(Input.GetMouseButtonDown(0))
            {
                //PlayerPrefs.SetFloat("Health",CurrentHealth);
                SceneManager.LoadScene("Level2");
                anahtarAlindiMi = false;
                
            }
        }
        
        if (col.gameObject.tag=="Back" && anahtarAlindiMi)
        {
           if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("TheEnd");
                anahtarAlindiMi = false;
                
            }
        }
         if (col.gameObject.tag=="Bot")
        {
            CurrentHealth -=5f;
        }
      
        
    }
   
     private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag =="Player")
        {
            Destroy(col.gameObject);
            score +=500;
            ScoreManager.instance.Score();
            if (score == 3000)
            {
                CurrentHealth += 5f;
            }
            else if (score == 6000)
            {
                CurrentHealth += 5f;
            }
            else if (score == 9000)
            {
                CurrentHealth += 5f;
            }
        }
        

        /*if (col.gameObject.tag == "Kapı" && anahtarAlindiMi)
        {
            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("Level1");
                anahtarAlindiMi = false;
            }
        }
        if (col.gameObject.tag=="Back")
        {
           if(Input.GetMouseButtonDown(0))
            {
                Invoke("ChangeScene", 1f);
                
            }
        
            
        }*/
       
    }

    


    public void TakeDamage(float damage)
    {
        CurrentHealth -=damage;
    }
    

   
    void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
