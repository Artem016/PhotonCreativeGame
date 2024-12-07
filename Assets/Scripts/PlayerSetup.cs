using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public Movement movement;
    public GameObject camera;

    public string nickName;

    public void IsLocalPlayer()
    {
        movement.enabled = true;
        camera.SetActive(true);
    }

    [PunRPC]
    public void SetNickName(string nickName)
    {
        this.nickName = nickName;
    }
}
