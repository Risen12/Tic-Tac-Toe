using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    private bool _isBusy;
    private GameObject _currentObject;

    public event Action<Cell> Clicked;

    private void Awake()
    {
        _isBusy = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke(this);
    }

    public void SetChestFigure(Chest chestFigure)
    {
        if (ValidState() == false)
            return;

        _isBusy = true;
        Chest figure = Instantiate(chestFigure, transform);
        _currentObject = figure.gameObject;
    }

    public void SetNullFigure(Null nullFigure)
    {
        if (ValidState() == false)
            return;

        _isBusy = true;
        Null figure = Instantiate(nullFigure, transform);
        _currentObject = figure.gameObject;
    }

    public Sprite GetCurrentSprite() 
    {
        if(_currentObject == null)
            return null;

        return _currentObject.GetComponent<Image>().sprite;
    }

    private bool ValidState()
    {
        if (_isBusy)
            return false;
        else
            return true;
    }
}
