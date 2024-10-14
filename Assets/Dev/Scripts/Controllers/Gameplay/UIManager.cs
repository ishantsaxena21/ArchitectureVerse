using AVerse.Models;
using UnityEngine;
using System.Collections;

namespace AVerse.Controllers.Gameplay
{
    public class UIManager : MonoBehaviour
    {
        static UIManager _instance;
        public static UIManager Instance { get { return _instance; } }

        [SerializeField] GameObject _unitDetailsUI, _topFilterUI, _queryForm;

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

        public void TriggerUnitDetails(Property property, bool show)
        {
            if (show) _unitDetailsUI.SetActive(show);
            else OnClick_CloseUnitDetails();
        }

        public void OnClick_BookNowOnUnitDetails(){
            StartCoroutine(BookNowMenu_Coroutine());
        }
        public IEnumerator BookNowMenu_Coroutine(){
            OnClick_CloseUnitDetails();
            yield return new WaitForSeconds(0.5f);
            _queryForm.SetActive(true);

        }

        public void ShowTopFilter(Property property)
        {
            _topFilterUI.SetActive(true);
        }
        public void HideTopFilter(Property property)
        {
            _topFilterUI.GetComponent<Animator>().SetTrigger(GameConstants.ANIMATION_TRIGGER_CLOSE);
        }
        public void OnClick_CloseUnitDetails()
        {
            _unitDetailsUI.GetComponent<Animator>().SetTrigger(GameConstants.ANIMATION_TRIGGER_CLOSE);
        }
        public void OnClick_CloseQueryForm()
        {
            _queryForm.GetComponent<Animator>().SetTrigger(GameConstants.ANIMATION_TRIGGER_CLOSE);
        }
        public void Dummy()
        {
            print("I was clicked");
        }

    }
}