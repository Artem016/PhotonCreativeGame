using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionButton : MonoBehaviour, IInteraction
{
    public string GetInteractText()
    {
        return "������ ������";
    }

    public void Interact()
    {
        Debug.LogError("�� ������ ������");
    }

}
