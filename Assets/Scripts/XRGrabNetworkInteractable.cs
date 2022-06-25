using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Unity.XR.CoreUtils;

public class XRGrabNetworkInteractable : XRGrabInteractable
{

    private PhotonView photonView;


    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnSelectEntering(XRBaseInteractor interactor)
    {
        photonView.RequestOwnership();

        Debug.Log("Request Ownership success");

        base.OnSelectEntering(interactor);
    }

    protected override void OnSelectExiting(XRBaseInteractor interactor)
    {
        photonView.TransferOwnership(null);

        Debug.Log("Request Ownership success");
        base.OnSelectExiting(interactor);
    }
}