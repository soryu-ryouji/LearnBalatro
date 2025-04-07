using UnityEngine;
using UnityEngine.EventSystems;

namespace LearnBalatro
{
    public class CardBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [HideInInspector] public bool isHovering;
        [HideInInspector] public bool isDragging;
        [HideInInspector] public bool wasDragged;
        [HideInInspector] public bool isSelected;

        public VisualCard visualCard;

        public void OnPointerEnter(PointerEventData eventData)
        {
            visualCard.EnterPointer(this);
            isHovering = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            visualCard.ExitPointer(this);
            isHovering = false;
        }
    }
}
