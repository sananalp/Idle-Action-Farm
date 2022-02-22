using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiView : MonoBehaviour
{
    [SerializeField] private Button hitButton;
    public delegate void ClickHandler();
    public event ClickHandler OnHitButtonClicked;

    public void HarvestButtonActivate()
    {
        hitButton.interactable = true;
    }
    public void HarvestButtonDeactivate()
    {
        hitButton.interactable = false;
    }
    public void HitButtonClick()
    {
        OnHitButtonClicked?.Invoke();
    }
}
