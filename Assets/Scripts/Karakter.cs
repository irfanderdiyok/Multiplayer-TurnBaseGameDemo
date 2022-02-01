// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Photon.Realtime;
// using Photon.Pun;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class Player : MonoBehaviour
// {

//     PhotonView MyPhotonView;


//     // ! Değişkenler
//     Vector3 moveDirection = Vector3.zero;
//     private float vSpeed;
//     private float walkSpeed = 5.5f;
//     private bool canMove = true;

//     private float yercekimi = 100.0f;
//     private float moveWS, moveAD;


//     public GameObject yetenekler;





//     // ! Referanslar
//     private CharacterController characterController;
//     public Animator anim;
//     public Slider Can;


//     public Transform ptr2, ptr3;


//     public GameObject playerG;

//     public FixedJoystick variableJoystick;
//     public GameObject Joystick;
//     public GameObject Joystick2;

//     private GameObject canvas;

//     private void Awake()
//     {
//         if (Application.platform == RuntimePlatform.Android)
//         {
//             Joystick.SetActive(true);
//         }
//     }



//     private void Start()
//     {
//         MyPhotonView = GetComponent<PhotonView>();
//         characterController = GetComponent<CharacterController>();

//         if (!MyPhotonView.IsMine)
//         {
//             Destroy(GetComponentInChildren<Camera>().gameObject);
//             GetComponentInChildren<Canvas>().gameObject.SetActive(false);
//         }

//         // if (MyPhotonView.IsMine)
//         // {
//         //     canvas = GetComponentInChildren<Canvas>().gameObject;
//         // }


//         ptr2 = GameObject.FindGameObjectWithTag("ptr2").transform;
//         ptr3 = GameObject.FindGameObjectWithTag("ptr3").transform;


//         Debug.Log(ptr3.position);

//         //    canvas.SetActive(false);


//     }

//     bool sahneDegis = true;


//     [PunRPC]
//     public void SahneDegis()
//     {


//         ptr2 = GameObject.FindGameObjectWithTag("ptr2").transform;
//         transform.position = ptr2.position;
//         transform.rotation = ptr2.rotation;
//         canMove = false;

//         // GetComponentInChildren<Kamera>().transform.rotation =ptr2.transform.rotation;
//         GetComponentInChildren<Kamera>().enabled = false;
//         anim.SetBool("Back", false);
//         anim.SetBool("Left", false);
//         anim.SetBool("Right", false);
//         anim.SetBool("Run", false);
//         yetenekler.SetActive(true);
//         Joystick.SetActive(false);


//     }

//     private void Update()
//     {

//         playerG = GameObject.FindGameObjectWithTag("player2");

//         if (MyPhotonView.IsMine)
//         {
//             Move();
//             AnimasyonKontrol(animationName: "Attack1");
//             AnimasyonKontrol(animationName: "Attack2");
//             AnimasyonKontrol(animationName: "Attack3");
//             AnimasyonKontrol(animationName: "Attack4");
//             AnimasyonKontrol(animationName: "Attack5");




//         }


//         if (Input.GetKeyDown(KeyCode.J))
//         {
//             transform.position = ptr3.position;
//         }









//     }


//     private void Move()
//     {

//         if (canMove)
//         {
//             Vector3 forward = transform.TransformDirection(Vector3.forward);
//             Vector3 right = transform.TransformDirection(Vector3.right);

//             if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
//             {
//                 moveWS = Input.GetAxis("Vertical") * walkSpeed;
//                 moveAD = Input.GetAxis("Horizontal") * walkSpeed;

//             }
//             else if (Application.platform == RuntimePlatform.Android)
//             {
//                 moveWS = variableJoystick.Vertical * walkSpeed;
//                 moveAD = variableJoystick.Horizontal * walkSpeed;
//             }


//             moveDirection = (forward * moveWS) + (right * moveAD);

//             if (moveDirection != Vector3.zero)
//             {
//                 if (moveWS > 0)
//                 {
//                     anim.SetBool("Back", false);
//                     anim.SetBool("Left", false);
//                     anim.SetBool("Right", false);
//                     anim.SetBool("Run", true);

//                 }
//                 else if (moveWS < 0)
//                 {
//                     anim.SetBool("Run", false);
//                     anim.SetBool("Left", false);
//                     anim.SetBool("Right", false);
//                     anim.SetBool("Back", true);
//                 }
//                 if (moveAD > 0 && moveWS == 0)
//                 {
//                     anim.SetBool("Run", false);
//                     anim.SetBool("Back", false);
//                     anim.SetBool("Right", false);
//                     anim.SetBool("Left", true);
//                 }
//                 else if (moveAD < 0 && moveWS == 0)
//                 {
//                     anim.SetBool("Run", false);
//                     anim.SetBool("Back", false);
//                     anim.SetBool("Left", false);
//                     anim.SetBool("Right", true);
//                 }
//             }
//             else
//             {
//                 anim.SetBool("Run", false);
//                 anim.SetBool("Back", false);
//                 anim.SetBool("Left", false);
//                 anim.SetBool("Right", false);

//             }
//         }






//         vSpeed -= yercekimi * Time.deltaTime;
//         moveDirection.y = vSpeed;

//         characterController.Move(moveDirection * Time.deltaTime);
//         moveDirection.x = 0;
//         moveDirection.z = 0;

//     }


//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("player2") && canMove)
//         {
//             other.gameObject.GetComponent<PhotonView>().RPC("SahneDegis", RpcTarget.All, null);

//         }


//     }


//     bool yetenek = false;

//     public void Yetenek1()
//     {
//         yetenek = true;
//         transform.position = ptr3.position;
//         transform.rotation = ptr3.rotation;
//         playerG.GetComponent<PhotonView>().RPC("HasarAl", RpcTarget.All, null);
//         anim.SetBool("Yetenek1", true);
//     }
//     public void Yetenek2()
//     {
//         yetenek = true;
//         transform.position = ptr3.position;
//         transform.rotation = ptr3.rotation;
//         playerG.GetComponent<PhotonView>().RPC("HasarAl", RpcTarget.All, null);
//         anim.SetBool("Yetenek2", true);
//     }
//     public void Yetenek3()
//     {
//         yetenek = true;
//         transform.position = ptr3.position;
//         transform.rotation = ptr3.rotation;
//         playerG.GetComponent<PhotonView>().RPC("HasarAl", RpcTarget.All, null);
//         anim.SetBool("Yetenek3", true);
//     }
//     public void Yetenek4()
//     {
//         yetenek = true;
//         transform.position = ptr3.position;
//         transform.rotation = ptr3.rotation;
//         playerG.GetComponent<PhotonView>().RPC("HasarAl", RpcTarget.All, null);
//         anim.SetBool("Yetenek4", true);
//     }
//     public void Yetenek5()
//     {
//         yetenek = true;
//         transform.position = ptr3.position;
//         transform.rotation = ptr3.rotation;
//         playerG.GetComponent<PhotonView>().RPC("HasarAl", RpcTarget.All, null);
//         anim.SetBool("Yetenek5", true);
//     }




//     void AnimasyonKontrol(string animationName)
//     {
//         if (anim.GetCurrentAnimatorStateInfo(0).IsName(animationName) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
//         {

//             anim.SetBool("Yetenek1", false);
//             anim.SetBool("Yetenek2", false);
//             anim.SetBool("Yetenek3", false);
//             anim.SetBool("Yetenek4", false);
//             anim.SetBool("Yetenek5", false);
//             if (yetenek)
//             {

//                 transform.position = ptr2.position;

//                 transform.rotation = ptr2.rotation;
//                 yetenek = false;
//             }
//         }
//     }




//     [PunRPC]
//     public void HasarAl()
//     {

//         Can.value -= 10;

//     }






// }






// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Photon.Realtime;
// using Photon.Pun;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class cadi : MonoBehaviour
// {

//     PhotonView MyPhotonView;


//     // ! Değişkenler
//     Vector3 moveDirection = Vector3.zero;
//     private float vSpeed;
//     private float walkSpeed = 2.5f;
//     private bool canMove = true;

//     private float yercekimi = 100.0f;
//     private float moveWS, moveAD;





//     // ! Referanslar
//     private CharacterController characterController;
//     public Animator anim;
//     public Slider Can;



//     public FixedJoystick variableJoystick;
//     public GameObject Joystick;
//     public GameObject Joystick2;

//     public GameObject yetenekler;

//     private GameObject canvas;

//     private GameObject playerG2;

//     public Transform ptr1;


//     public GameObject alev;


//     private void Awake()
//     {
//         if (Application.platform == RuntimePlatform.Android)
//         {
//             Joystick.SetActive(true);
//         }
//     }


//     private void Start()
//     {
//         MyPhotonView = GetComponent<PhotonView>();
//         characterController = GetComponent<CharacterController>();
//         if (!MyPhotonView.IsMine)
//         {
//             Destroy(GetComponentInChildren<Camera>().gameObject);
//             GetComponentInChildren<Canvas>().gameObject.SetActive(false);
//         }
//         // canvas = GetComponentInChildren<Canvas>().gameObject;

//         ptr1 = GameObject.FindGameObjectWithTag("ptr1").transform;



//         // canvas.SetActive(false);


//     }

//     private void Update()
//     {

//         playerG2 = GameObject.FindGameObjectWithTag("player1");

//         if (MyPhotonView.IsMine)
//         {
//             Move();
//             AnimasyonKontrol(animationName: "Attack1");
//             AnimasyonKontrol(animationName: "Attack2");
//             AnimasyonKontrol(animationName: "Attack3");
//             AnimasyonKontrol(animationName: "Attack4");
//             AnimasyonKontrol(animationName: "Attack5");

//         }









//     }


//     private void Move()
//     {

//         if (canMove)
//         {

//             Vector3 forward = transform.TransformDirection(Vector3.forward);
//             Vector3 right = transform.TransformDirection(Vector3.right);

//             if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
//             {
//                 moveWS = Input.GetAxis("Vertical") * walkSpeed;
//                 moveAD = Input.GetAxis("Horizontal") * walkSpeed;

//             }
//             else if (Application.platform == RuntimePlatform.Android)
//             {
//                 moveWS = variableJoystick.Vertical * walkSpeed;
//                 moveAD = variableJoystick.Horizontal * walkSpeed;
//             }


//             moveDirection = (forward * moveWS) + (right * moveAD);

//             if (moveDirection != Vector3.zero)
//             {
//                 if (moveWS > 0)
//                 {
//                     anim.SetBool("Back", false);
//                     anim.SetBool("Left", false);
//                     anim.SetBool("Right", false);
//                     anim.SetBool("Run", true);

//                 }
//                 else if (moveWS < 0)
//                 {
//                     anim.SetBool("Run", false);
//                     anim.SetBool("Left", false);
//                     anim.SetBool("Right", false);
//                     anim.SetBool("Back", true);
//                 }
//                 if (moveAD > 0 && moveWS == 0)
//                 {
//                     anim.SetBool("Run", false);
//                     anim.SetBool("Back", false);
//                     anim.SetBool("Right", false);
//                     anim.SetBool("Left", true);
//                 }
//                 else if (moveAD < 0 && moveWS == 0)
//                 {
//                     anim.SetBool("Run", false);
//                     anim.SetBool("Back", false);
//                     anim.SetBool("Left", false);
//                     anim.SetBool("Right", true);
//                 }
//             }
//             else
//             {
//                 anim.SetBool("Run", false);
//                 anim.SetBool("Back", false);
//                 anim.SetBool("Left", false);
//                 anim.SetBool("Right", false);

//             }

//         }






//         vSpeed -= yercekimi * Time.deltaTime;
//         moveDirection.y = vSpeed;

//         characterController.Move(moveDirection * Time.deltaTime);
//         moveDirection.x = 0;
//         moveDirection.z = 0;

//     }


//     public void Yetenek1()
//     {


//         anim.SetBool("Yetenek1", true);
//     }
//     public void Yetenek2()
//     {

//         anim.SetBool("Yetenek2", true);
//     }
//     public void Yetenek3()
//     {

//         anim.SetBool("Yetenek3", true);
//     }
//     public void Yetenek4()
//     {

//         alev.SetActive(true);
//         playerG2.GetComponent<PhotonView>().RPC("HasarAl", RpcTarget.All, null);
//         anim.SetBool("Yetenek4", true);
//     }
//     public void Yetenek5()
//     {

//         anim.SetBool("Yetenek5", true);
//     }



//     bool sahneDegis = true;

//     [PunRPC]
//     public void SahneDegis()
//     {
       
//             canMove = false;

//             ptr1 = GameObject.FindGameObjectWithTag("ptr1").transform;
//             transform.position = ptr1.position;
//             transform.rotation = ptr1.rotation;

//             GetComponentInChildren<Kamera>().enabled = false;
//             playerG2.gameObject.GetComponent<PhotonView>().RPC("SahneDegis", RpcTarget.All, null);
//             anim.SetBool("Run", false);
//             anim.SetBool("Back", false);
//             anim.SetBool("Left", false);
//             anim.SetBool("Right", false);
//             yetenekler.SetActive(true);
//             Joystick.SetActive(false);
        
//     }






//     void AnimasyonKontrol(string animationName)
//     {
//         if (anim.GetCurrentAnimatorStateInfo(0).IsName(animationName) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
//         {

//             anim.SetBool("Yetenek1", false);
//             anim.SetBool("Yetenek2", false);
//             anim.SetBool("Yetenek3", false);
//             anim.SetBool("Yetenek4", false);
//             anim.SetBool("Yetenek5", false);
//             alev.SetActive(false);
//         }
//     }


//     [PunRPC]
//     public void HasarAl()
//     {

//         Can.value -= 10;

//     }





// }