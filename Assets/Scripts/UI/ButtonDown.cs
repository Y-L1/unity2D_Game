using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class ButtonDown : MonoBehaviour, IPointerDownHandler,IPointerUpHandler,IPointerClickHandler
{
    public Sprite beforeBtn;
    public Sprite afterBtn;

    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = beforeBtn;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        image.sprite = afterBtn;

        SoundManger.instance.BtnDown();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = beforeBtn;
    }
    public void OnPointerClick(PointerEventData eventData)
    {

    }
    
}
