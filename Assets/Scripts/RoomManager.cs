using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject player;
    [Space]
    public List<Transform> spawnPoints;

    [Space]
    public GameObject roomCam;

    [Space]
    public GameObject nameUI;
    public GameObject connectingUI;

    [SerializeField] private PlayerInteractUI _playerInteractUI;

    private string _nickName = "unnamed";

    private void Start()
    {

    }

    public void ChangeNickName(string nickName)
    {
        _nickName = nickName;
    }

    public void JoinRoomClick()
    {
        Debug.Log("Connecting...");

        nameUI.SetActive(false);
        connectingUI.SetActive(true);

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to Server");

        PhotonNetwork.JoinLobby(); 
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log("We are in the lobby");

        PhotonNetwork.JoinOrCreateRoom("test", null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedLobby();

        Debug.Log("We are connected and in room");

        roomCam.SetActive(false);

        GameObject player = PhotonNetwork.Instantiate(this.player.name, spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position, Quaternion.identity);
        player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _playerInteractUI.SetPlayerInteract(player.GetComponent<PlayerInteract>());

        player.GetComponent<PhotonView>().RPC("SetNickName", RpcTarget.AllBuffered, _nickName);       
    }
}
