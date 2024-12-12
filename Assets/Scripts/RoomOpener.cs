using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomOpener : MonoBehaviour, IInteractable
{
    public string GetInteractText()
    {
        return "������������ � �������";
    }

    public void Interact()
    {
        RoomManager.Instance.SwitchToNewRoom("newroom");
    }

}
