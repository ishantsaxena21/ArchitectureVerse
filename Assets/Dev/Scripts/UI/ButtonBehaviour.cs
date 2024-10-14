using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace AVerse.UI{
    [RequireComponent(typeof(Button))]
    public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] Sprite _normalImage;
        [SerializeField] Sprite _hoverImage;
        [SerializeField] Sprite _selectedImage;

        Button _button;

        protected void Start(){
            _button = GetComponent<Button>();
            _button.image.sprite = _normalImage;
        }

        public void OnPointerEnter(PointerEventData eventData){
            _button.image.sprite = _hoverImage;
        }
        
        public void OnPointerDown(PointerEventData eventData){
            _button.image.sprite = _selectedImage;
        }

        public void OnPointerExit(PointerEventData eventData){
            _button.image.sprite = _normalImage;
        }

        public void OnPointerUp(PointerEventData eventData){
            _button.image.sprite = _normalImage;
        }
    }
}
