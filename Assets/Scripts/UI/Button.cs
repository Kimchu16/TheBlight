using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonVisualHandler : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image backgroundImage;
    public Sprite defaultSprite;
    public Sprite pressedSprite;
    public Sprite selectedSprite;

    public void OnSelect(BaseEventData eventData)
    {
        backgroundImage.sprite = selectedSprite;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        backgroundImage.sprite = defaultSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        backgroundImage.sprite = pressedSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        backgroundImage.sprite = selectedSprite;
    }
}
