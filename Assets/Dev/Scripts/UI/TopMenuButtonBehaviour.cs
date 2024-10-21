using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace AVerse.UI{
    [RequireComponent(typeof(Button))]
    public class TopMenuButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] Sprite _normalImage;
        [SerializeField] Sprite _hoverImage;
        [SerializeField] Sprite _selectedImage;

        Sprite _lastImage;
        Button _button;
        bool _isSelected = false;        

        protected void Start(){
            _button = GetComponent<Button>();
            _button.image.sprite = _normalImage;
        }

        public void OnPointerEnter(PointerEventData eventData){
            _lastImage = _button.image.sprite;
            _button.image.sprite = _hoverImage;
        }
        
         public void OnPointerDown(PointerEventData eventData){
            if(!_isSelected){
                _button.image.sprite = _selectedImage;
                _isSelected = true;
            }
            else {
                _button.image.sprite = _normalImage;
                _isSelected = false;
            }
            _lastImage = _button.image.sprite;
        }

        public void OnPointerExit(PointerEventData eventData){
            if(_lastImage && _lastImage == _selectedImage){
                _button.image.sprite = _selectedImage;
                _lastImage = null;
                return;
            }
            else if(_lastImage && _lastImage ==_normalImage)
            {
                _button.image.sprite = _normalImage;
            }
        }


    }

}