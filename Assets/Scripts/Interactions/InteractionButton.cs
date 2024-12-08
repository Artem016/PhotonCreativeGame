using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionButton : MonoBehaviour, IInteraction
{
    public string GetInteractText()
    {
        return "Нажать кнопку";
    }

    public void Interact()
    {
        Debug.LogError("Вы нажали кнопку");
    }

}
