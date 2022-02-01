using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Network : MonoBehaviourPunCallbacks
{

    public Transform p1, p2;


    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
    public override void OnJoinedRoom()
    {
        GameObject player = PhotonNetwork.Instantiate("brute", p1.position, Quaternion.identity, 0, null);
        if (player.gameObject.GetComponent<PhotonView>().OwnerActorNr == 2)
        {
            PhotonNetwork.Destroy(player);
            GameObject player2 = PhotonNetwork.Instantiate("Cadi", p2.position, Quaternion.identity, 0, null);
        }

    }




}
