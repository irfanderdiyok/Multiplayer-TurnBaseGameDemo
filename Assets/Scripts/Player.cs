using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviourPun
{




    // ! Değişkenler
    Vector3 moveDirection = Vector3.zero;
    private float vSpeed;
    private float walkSpeed = 5.5f;
    private bool canMove = true;

    private float yercekimi = 100.0f;
    private float moveWS, moveAD;
    private bool yetenekV = false;







    // ! Referanslar
    public GameObject yetenekler;
    private CharacterController characterController;
    public Animator anim;
    public Slider Can;
    PhotonView MyPhotonView;
    public GameObject saldirsavunGO;
    public TextMeshProUGUI saldirsavun;
    public Transform ptr2, ptr3;

    public GameObject playerG;

    public FixedJoystick variableJoystick;
    public GameObject Joystick;
    public GameObject Joystick2;

    private GameObject canvas;
    private Yonetici oyunYoneticisi;

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Joystick.SetActive(true);
        }
    }



    private void Start()
    {
        MyPhotonView = GetComponent<PhotonView>();
        characterController = GetComponent<CharacterController>();

        if (!MyPhotonView.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            GetComponentInChildren<Canvas>().gameObject.SetActive(false);
        }

        ptr2 = GameObject.FindGameObjectWithTag("ptr2").transform;
        ptr3 = GameObject.FindGameObjectWithTag("ptr3").transform;


        oyunYoneticisi = GameObject.FindGameObjectWithTag("yonetici").GetComponent<Yonetici>();
    }








    private void Update()
    {
        playerG = GameObject.FindGameObjectWithTag("player2");
        if (MyPhotonView.IsMine)
        {
            Move();
            AnimasyonKontrol(animationName: "Attack1");
            AnimasyonKontrol(animationName: "Attack2");
            AnimasyonKontrol(animationName: "Attack3");

            if (oyunYoneticisi.oyuncu1 && !canMove)
            {
                this.saldirsavun.text = "SALDIR";
                yetenekler.SetActive(true);
            }
            else if (!oyunYoneticisi.oyuncu1 && !canMove)
            {
                this.saldirsavun.text = "SAVUN";
                yetenekler.SetActive(false);
            }


        }


    }


    private void Move()
    {

        if (canMove)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                moveWS = Input.GetAxis("Vertical") * walkSpeed;
                moveAD = Input.GetAxis("Horizontal") * walkSpeed;

            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                moveWS = variableJoystick.Vertical * walkSpeed;
                moveAD = variableJoystick.Horizontal * walkSpeed;
            }


            moveDirection = (forward * moveWS) + (right * moveAD);

            if (moveDirection != Vector3.zero)
            {
                if (moveWS > 0)
                {
                    anim.SetBool("Back", false);
                    anim.SetBool("Left", false);
                    anim.SetBool("Right", false);
                    anim.SetBool("Run", true);

                }
                else if (moveWS < 0)
                {
                    anim.SetBool("Run", false);
                    anim.SetBool("Left", false);
                    anim.SetBool("Right", false);
                    anim.SetBool("Back", true);
                }
                if (moveAD > 0 && moveWS == 0)
                {
                    anim.SetBool("Run", false);
                    anim.SetBool("Back", false);
                    anim.SetBool("Right", false);
                    anim.SetBool("Left", true);
                }
                else if (moveAD < 0 && moveWS == 0)
                {
                    anim.SetBool("Run", false);
                    anim.SetBool("Back", false);
                    anim.SetBool("Left", false);
                    anim.SetBool("Right", true);
                }
            }
            else
            {
                anim.SetBool("Run", false);
                anim.SetBool("Back", false);
                anim.SetBool("Left", false);
                anim.SetBool("Right", false);

            }
        }






        vSpeed -= yercekimi * Time.deltaTime;
        moveDirection.y = vSpeed;

        characterController.Move(moveDirection * Time.deltaTime);
        moveDirection.x = 0;
        moveDirection.z = 0;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player2") && canMove)
        {
            other.gameObject.GetComponent<PhotonView>().RPC("SahneDegis", RpcTarget.All, null);

        }


    }









    public void Yetenek(string yetenekadi)
    {
        if (oyunYoneticisi.oyuncu1)
        {
            playerG.GetComponent<PhotonView>().RPC("HasarAl", RpcTarget.All, null);
            playerG.GetComponent<PhotonView>().RPC("Bruteileri", RpcTarget.All, null);
            anim.SetBool(yetenekadi, true);
            yetenekV = true;
            oyunYoneticisi.gameObject.GetComponent<PhotonView>().RPC("oyuncuDegistir", RpcTarget.All, null);

        }
    }


    // ! Bunda sorun yok.
    [PunRPC]
    public void AnimasyonHazirla(string yetenekadi)
    {



        Can.value -= 10;


    }










    void AnimasyonKontrol(string animationName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(animationName) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {

            anim.SetBool("Yetenek1", false);
            anim.SetBool("Yetenek2", false);
            anim.SetBool("Yetenek3", false);






            if (yetenekV)
            {
                playerG.GetComponent<PhotonView>().RPC("BruteGeri", RpcTarget.All, null);
                yetenekV = false;



            }


        }
    }





    [PunRPC]
    public void SahneDegis()
    {
        ptr2 = GameObject.FindGameObjectWithTag("ptr2").transform;
        transform.position = ptr2.position;
        transform.rotation = ptr2.rotation;
        canMove = false;
        GetComponentInChildren<Kamera>().enabled = false;
        anim.SetBool("Back", false);
        anim.SetBool("Left", false);
        anim.SetBool("Right", false);
        anim.SetBool("Run", false);
        yetenekler.SetActive(true);
        Joystick.SetActive(false);
        saldirsavunGO.SetActive(true);



    }



    // ! Bunda sorun yok.
    [PunRPC]
    public void HasarAl()
    {

        Can.value -= 10;
        if (Can.value <= 0)
        {
            anim.SetBool("Die", true);
            Destroy(this);
        }


    }

    // ! Bunda sorun yok.
    [PunRPC]
    public void Ileri()
    {
        transform.position = ptr3.position;
        transform.rotation = ptr3.rotation;
    }

    // ! Bunda sorun yok.
    [PunRPC]
    public void Geri()
    {
        transform.position = ptr2.position;
        transform.rotation = ptr2.rotation;
    }







}