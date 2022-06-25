using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{

    private GameObject spawnedPlayerPrefab;
    public int players = 0;
    
    public override void OnJoinedRoom()
    {
        players++;
        base.OnJoinedRoom();
        Vector3 player2Position = transform.position + Vector3.forward;
        Quaternion player2Rotation = Quaternion.AngleAxis(90f, Vector3.up);
        if(players > 1)
        {
            spawnedPlayerPrefab = PhotonNetwork.Instantiate("OVR Network Player", player2Position, player2Rotation);
        }
        else
        {
            spawnedPlayerPrefab = PhotonNetwork.Instantiate("OVR Network Player", transform.position, transform.rotation);
        }
    }

    public override void OnLeftRoom()
    {
        players--;
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
