using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{

    private GameObject spawnedPlayerPrefab;
    private GameObject spawnedCubePrefab;
    private GameObject spawnedPiecesWhite;
    private GameObject spawnedPiecesBlack;

    public int players = 0;

    public override void OnJoinedRoom()
    {
        players++;
        base.OnJoinedRoom();
       
        Vector3 player2Position = new Vector3(0, 0, 10);
        Quaternion player2Rotation = Quaternion.Euler(0, 180, 0);

        if (PhotonNetwork.PlayerList.Length > 1)
        {
            //spawnedCameraRigPlayer2 = PhotonNetwork.Instantiate("OVRCameraRig", new Vector3(0, 0, 1), Quaternion.Euler(0, 180, 0));
            spawnedPlayerPrefab = PhotonNetwork.Instantiate("OVR Network Player", new Vector3(0, 0, 10), player2Rotation);
            //spawnedCubePrefab = PhotonNetwork.Instantiate("Cube_Sync", new Vector3(0, 1, 0.5f), transform.rotation);
            spawnedPiecesBlack = PhotonNetwork.Instantiate("Black_Pieces", new Vector3(0, 0.71f, 0.59f), Quaternion.Euler(-90, 0, 180));
            Debug.Log("P2");

        }
        else
        {
            //spawnedCameraRigPlayer1 = PhotonNetwork.Instantiate("OVRCameraRig", new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            spawnedPlayerPrefab = PhotonNetwork.Instantiate("OVR Network Player", transform.position, transform.rotation);
            //spawnedCubePrefab = PhotonNetwork.Instantiate("Cube_Sync", new Vector3(0, 3, 0.5f), transform.rotation);
            spawnedPiecesWhite = PhotonNetwork.Instantiate("White_Pieces", new Vector3(0, 0.71f, 0.41f), Quaternion.Euler(-90, 0, 0 ));
            Debug.Log("P1");
        }
    }

    public override void OnLeftRoom()
    {
        players--;
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
