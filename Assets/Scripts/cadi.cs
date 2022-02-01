using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class cadi : MonoBehaviourPun
{

    PhotonView MyPhotonView;


    // ! Değişkenler
    Vector3 moveDirection = Vector3.zero;
    private float vSpeed;
    private float walkSpeed = 2.5f;
    private bool canMove = true;
    private float yercekimi = 100.0f;
    private float moveWS, moveAD;

    // ! Referanslar
    private CharacterController characterController;
    public Animator anim;
    public Slider Can;


    public GameObject saldirsavunGO;
    public TextMeshProUGUI saldirsavun;



    public FixedJoystick variableJoystick;
    public GameObject Joystick;
    public GameObject Joystick2;

    public GameObject yetenekler;


    public GameObject buyu1, buyu2;

    private GameObject canvas;

    private GameObject playerG2;

    private Transform ptr1;
    private Yonetici oyunYoneticisi;


    public GameObject alev;


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
        // canvas = GetComponentInChildren<Canvas>().gameObject;

        ptr1 = GameObject.FindGameObjectWithTag("ptr1").transform;
        oyunYoneticisi = GameObject.FindGameObjectWithTag("yonetici").GetComponent<Yonetici>();





    }

    private void Update()
    {

        
        playerG2 = GameObject.FindGameObjectWithTag("player1");

        if (MyPhotonView.IsMine)
        {
            Move();
            AnimasyonKontrol(animationName: "Attack1");
            AnimasyonKontrol(animationName: "Attack2");
            AnimasyonKontrol(animationName: "Attack3");



            if (oyunYoneticisi.oyuncu2 && !canMove)
            {
                this.saldirsavun.text = "SALDIR";
                yetenekler.SetActive(true);
            }
            else if (!oyunYoneticisi.oyuncu2 && !canMove)
            {
                this.saldirsavun.text = "SAVUN";
                yetenekler.SetActive(false);
            }


        }




    }


    [PunRPC]
    public void SahneDegis()
    {

        canMove = false;

        ptr1 = GameObject.FindGameObjectWithTag("ptr1").transform;
        transform.position = ptr1.position;
        transform.rotation = ptr1.rotation;

        GetComponentInChildren<Kamera>().enabled = false;
        playerG2.gameObject.GetComponent<PhotonView>().RPC("SahneDegis", RpcTarget.All, null);
        anim.SetBool("Run", false);
        anim.SetBool("Back", false);
        anim.SetBool("Left", false);
        anim.SetBool("Right", false);
        yetenekler.SetActive(true);
        Joystick.SetActive(false);
        saldirsavunGO.SetActive(true);

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


    public void Yetenek(string yetenekadi)
    {
        if (oyunYoneticisi.oyuncu2)
        {

            playerG2.GetComponent<PhotonView>().RPC("HasarAl", RpcTarget.All, null);

            anim.SetBool(yetenekadi, true);

            if (yetenekadi == "Yetenek1")
            {
                MyPhotonView.RPC("atesYak", RpcTarget.All, true);
            }
            else if (yetenekadi == "Yetenek2")
            {
                MyPhotonView.RPC("buyuler", RpcTarget.All, true);

            }
            else if (yetenekadi == "Yetenek3")
            {
                MyPhotonView.RPC("meteor", RpcTarget.All, true);

            }

            oyunYoneticisi.gameObject.GetComponent<PhotonView>().RPC("oyuncuDegistir", RpcTarget.All, null);
        }
    }



    // ! Bunda sorun yok.
    [PunRPC]
    private void atesYak(bool durum)
    {
        alev.SetActive(durum);
    }

    // ! Bunda sorun yok.
    [PunRPC]
    private void buyuler(bool durum)
    {
        buyu1.SetActive(durum);
    }

    // ! Bunda sorun yok.
    [PunRPC]
    private void meteor(bool durum)
    {
        buyu2.SetActive(durum);
    }




    bool sahneDegis = true;








    void AnimasyonKontrol(string animationName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(animationName) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {

            anim.SetBool("Yetenek1", false);
            anim.SetBool("Yetenek2", false);
            anim.SetBool("Yetenek3", false);
            MyPhotonView.RPC("atesYak", RpcTarget.All, false);
            MyPhotonView.RPC("buyuler", RpcTarget.All, false);
            MyPhotonView.RPC("meteor", RpcTarget.All, false);



        }
    }


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


    [PunRPC]
    public void Bruteileri()
    {
        playerG2.GetComponent<PhotonView>().RPC("Ileri", RpcTarget.All, null);
    }

    [PunRPC]
    public void BruteGeri()
    {
        playerG2.GetComponent<PhotonView>().RPC("Geri", RpcTarget.All, null);
    }











}