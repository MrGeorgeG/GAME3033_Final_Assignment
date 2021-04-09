using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    public static bool GameIsPaused = false;

    public GameManager GM;

    private bool isGround = false;

    private Vector3 movement;
    public Animator anim;
    public Rigidbody playerRigidbody;

    private int floorMask;
    private float camRayLength = 100f;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();

        playerRigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused == false)
            {
                GM.PauseButton();
                GameIsPaused = true;
            }
            else
            {
                GM.QuitPauseButton();
                GameIsPaused = false;
            }
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        anim.SetFloat("Forward", v);
        anim.SetFloat("Turn", h);

        Move(h, v);
        Turning();
        Animating();

        
    }

    private void Move(float h, float v)
    {
        movement.Set(h, 0f, v);

        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    private void Turning()
    {
        Vector3 point = new Vector3(Input.mousePosition.x, Input.mousePosition.y , Input.mousePosition.z);

        Ray camRay = Camera.main.ScreenPointToRay(point);

        RaycastHit floorHit;

        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - new Vector3 (transform.position.x, transform.position.y - 50, transform.position.z);
            playerToMouse.y = 0.0f;

            //Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            //playerRigidbody.MoveRotation(newRotation);

            transform.LookAt(transform.position + playerToMouse, Vector3.up);

            //newRotation.y += 100;
            

        }

        
    }

    private void Animating()
    {
        
        foreach (var chilAnimator in GetComponentsInChildren<Animator>())
        {
            if(chilAnimator != anim)
            {
                anim.avatar = chilAnimator.avatar;
                Destroy(chilAnimator);
                break;
            }
        }
    } 

}
