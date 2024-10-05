using AVerse.Models;
using UnityEngine;

namespace AVerse.Controllers.Gameplay
{
    public class UIManager : MonoBehaviour
    {
        static UIManager _instance;
        public static UIManager Instance { get { return _instance; } }

        [SerializeField] GameObject _unitDetailsUI;

        private void Awake()
        {
            _instance = this;
        }
        private Vector3 pos;
        private void Start()
        {
        }

        private void OnEnable()
        {

        }
        public void OnClick_Reset()
        {
            Debug.Log("Reset");
            Camera.main.transform.position = pos;
        }

        public void ShowUnitDetails(Property property)
        {
            _unitDetailsUI.SetActive(true);
        }

        public void OnClick_CloseUnitDetails()
        {
            _unitDetailsUI.GetComponent<Animator>().SetTrigger("Close");
        }
        //public void OnClick_PropertyButton(string propertyId)
        //{
        //    GameEvents.PropertySelectionChanged(propertyId);
        //}
        public void Dummy()
        {
            print("I was clicked");
        }

    }
}