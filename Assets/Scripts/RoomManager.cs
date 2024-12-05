using Photon.Pun;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject player;
    [Space]
    public Transform spawnPoint;
    private void Start()
    {
        Debug.Log("Connecting...");

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

        GameObject playerLocal = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
    }
}
