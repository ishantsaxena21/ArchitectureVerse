using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AVerse.UI{
    public class TopMenuButtonBehaviour : MonoBehaviour
    {
        [SerializeField] Sprite _normalImage;
        [SerializeField] Sprite _hoverImage;
        [SerializeField] Sprite _selectedImage;

        bool _isSelected = false;
    }

}