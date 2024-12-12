using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static Action onJoinedRoom;
    public static Action onLeftRoom;

    private string _nextCreateRoomName = "hub";

    private static RoomManager _instance;

    //избавиться от singleton
    public static RoomManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("instance is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    public void SwitchToNewRoom(string newRoomName)
    {
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("Leaving current room...");
            _nextCreateRoomName = "room";
            PhotonNetwork.LeaveRoom(); 
        }
    }

    public void ChangeNickName(string nickName)
    {
        _nickName = nickName;
    }

    public void JoinRoomClick()
    {
        Debug.Log("Connecting...");

        //перенести в другой скрипт
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

        PhotonNetwork.JoinOrCreateRoom(_nextCreateRoomName, null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedLobby();

        Debug.Log("We are connected and in room");

        if(_nextCreateRoomName != "hub")
        {
            PhotonNetwork.LoadLevel("room");
        }

        onJoinedRoom?.Invoke();

        //перенести в другой скрипт
        //roomCam.SetActive(false);
        //GameObject player = PhotonNetwork.Instantiate(this.player.name, spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count - 1)].position, Quaternion.identity);
        //player.GetComponent<PlayerSetup>().IsLocalPlayer();
        //_playerInteractUI.SetPlayerInteract(player.GetComponent<PlayerInteract>());
        //player.GetComponent<PhotonView>().RPC("SetNickName", RpcTarget.AllBuffered, _nickName);

        

        Debug.LogError(PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Successfully left the room. Now creating a new room...");
        onLeftRoom?.Invoke();   
    }

}
