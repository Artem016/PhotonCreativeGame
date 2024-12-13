using Photon.Pun;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static Action onConnectedToMaster;
    public static Action onJoinedRoom;
    public static Action onLeftRoom;

    public GameObject player;

    //public List<Transform> spawnPoints;

    //public GameObject roomCam;

    public GameObject nameUI;
    public GameObject connectingUI;

    //[SerializeField] private PlayerInteractUI _playerInteractUI;

    private string _roomForCreating = "hub";
    private string _sceneForLoad = "fps_scene";

    //private string _nickName = "unnamed";

    public void SwitchToRoom(string roomName)
    {
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("Leaving current room...");
            _roomForCreating = roomName;
            _sceneForLoad = "room";
            PhotonNetwork.LeaveRoom(); 
        }
    }

    public void SwitchToHub()
    {
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("Leaving current room...");
            _roomForCreating = "hub";
            _sceneForLoad = "fps_scene";
            PhotonNetwork.LeaveRoom();
        }
    }

    //public void ChangeNickName(string nickName)
    //{
    //    _nickName = nickName;
    //}

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

        onConnectedToMaster?.Invoke();

        PhotonNetwork.JoinOrCreateRoom(_roomForCreating, null, null);

        if (_sceneForLoad != SceneManager.GetActiveScene().name)
        {
            PhotonNetwork.LoadLevel(_sceneForLoad);
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedLobby();

        Debug.Log("We are connected and in room");

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
