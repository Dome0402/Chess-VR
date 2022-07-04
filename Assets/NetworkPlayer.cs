using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    private PhotonView photonView;

    public Transform headRig;   // bzw ORIGIN VERWENDEN mit using Unity.XR.CoreUtils;
    public Transform leftHandRig;
    public Transform rightHandRig;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        OVRCameraRig rig = FindObjectOfType<OVRCameraRig>();
        headRig = rig.transform.Find("Tracking Space/CenterEyeAnchor");
        leftHandRig = rig.transform.Find("Tracking Space/LeftHandAnchor");
        rightHandRig = rig.transform.Find("Tracking Space/RightHandAnchor");


        if (photonView.IsMine)
        {
            if (PhotonNetwork.PlayerList.Length > 1)
            {
                rig.transform.localPosition = new Vector3(0, 0, 1);
                rig.transform.localRotation = Quaternion.Euler(0, 180, 0);
                Debug.Log("PlayerList PART 2 !!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            }
            else
            {
                rig.transform.localPosition = new Vector3(0, 0, 0);
                rig.transform.localRotation = Quaternion.Euler(0, 0, 0);
                Debug.Log("PlayerList PART 1 !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }

        }
        
        
        


        head.gameObject.SetActive(false);
        rightHand.gameObject.SetActive(false);
        leftHand.gameObject.SetActive(false);
        
        if (photonView.IsMine)
        {

            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
        }

        /*
        if (photonView.IsMine)
        {
            GetComponent<Camera>().enabled = true;
        } else
        {
            GetComponent<Camera>().enabled = false;
        } 
        */


    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        //if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            //photonView.RequestOwnership();
            //Debug.Log("Getriggert !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            MapPosition(head, XRNode.Head);
            MapPosition(leftHand, XRNode.LeftHand);
            MapPosition(rightHand, XRNode.RightHand);
            //return;



            //UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            //UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);
        }
    }

    void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    void MapPosition(Transform target, XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        //target.position = rigTransform.position;
        //target.rotation = rigTransform.rotation;

    }



}