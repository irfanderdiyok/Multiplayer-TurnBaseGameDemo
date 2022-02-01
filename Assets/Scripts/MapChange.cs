using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class MapChange : MonoBehaviourPunCallbacks
{
    public Transform p1,p2;
    public override void OnJoinedRoom()
    {
        GameObject player = PhotonNetwork.Instantiate("Cadi", p1.position, Quaternion.identity, 0, null);



        if (player.gameObject.GetComponent<PhotonView>().OwnerActorNr == 2)
        {
            PhotonNetwork.Destroy(player);
            GameObject player2 = PhotonNetwork.Instantiate("brute", p2.position, Quaternion.identity, 0, null);
        }

    }




}
