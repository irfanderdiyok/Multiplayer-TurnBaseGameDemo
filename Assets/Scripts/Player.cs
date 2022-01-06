using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;



    public float walkSpeed = 3.5f;
    public float kosuHizi = 6.5f;
    public float ziplamaGucu = 8.0f;
    public float yercekimi = 30.0f;



    Vector3 moveDirection = Vector3.zero;

    private CharacterController characterController;


    public bool canMove = true;


    bool Walk = false;







    public GameObject SkillUI;

    Vector3 enemyPosition;
    Vector3 currentPostion;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
     

    }

    
    void Update()
    {

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? kosuHizi : walkSpeed) * Input.GetAxis("Vertical") : 0; //W S
        float curSpeedY = canMove ? (isRunning ? kosuHizi : walkSpeed) * Input.GetAxis("Horizontal") : 0; //A D
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);


        if (canMove)
        {
            if (curSpeedX > 0 && isRunning)
            {

                anim.Play("Run");
            }
            else if (curSpeedX > 0)
            {
                //!Burayı eski haline getir bozuldu
                anim.Play("Run");
                Walk = true;
            }
            else if (curSpeedX == 0 && Walk)
            {
                Walk = false;
                Invoke(nameof(WalkStop), 0.05f);
            }
            //Saga sola donme ekle

        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= yercekimi * Time.deltaTime;
        }



        characterController.Move(moveDirection * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.Play("Punch");

        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.Play("Body Hit");

        }
        AnimasyonKontrol(animationName: "Punch");
        AnimasyonKontrol(animationName: "Body Hit");






    }

    void WalkStop()
    {

        anim.Play("Idle");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            print("fight Scene");
            SkillUI.SetActive(true);
         //   Invoke(nameof(FightScene), 1f);
            enemyPosition = other.transform.position;
        }
    }

    // void FightScene()
    // {
    //     anim.Play("Fight Idle");
    //     canMove = false;
    //     GetComponent<CameraFollow>().enabled = false;
    // }


    void AnimasyonKontrol(string animationName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(animationName) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            anim.Play("Fight Idle");
            Debug.Log(animationName + " Animasyon bitti");
            transform.position = currentPostion;
        }
    }


    public void Skill(string skillName)
    {
        currentPostion = transform.position;
        transform.position = enemyPosition;
        anim.Play(skillName);



    }




}
