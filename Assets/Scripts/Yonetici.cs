using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Yonetici : MonoBehaviour
{
    public bool oyuncu1 = true;
    public bool oyuncu2 = false;

    [PunRPC]
    public void oyuncuDegistir(){
        oyuncu1 = !oyuncu1;
        oyuncu2 = !oyuncu2;
    }




}
