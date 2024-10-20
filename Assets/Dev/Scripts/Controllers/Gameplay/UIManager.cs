using AVerse.Models;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace AVerse.Controllers.Gameplay
{
    public class UIManager : MonoBehaviour
    {
        static UIManager _instance;
        public static UIManager Instance { get { return _instance; } }

        [SerializeField] GameObject _unitDetailsUI, _topFilterUI, _queryForm, _2DImage;

        private void Awake()
        {
            _instance = this;
        }
        private Vector3 pos;

        #region Main Scene Callbacks
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

        public void OnClick_MainSceneBookNow(){
            StartCoroutine(MainSceneBookNowMenu_Coroutine());
        }
        public IEnumerator MainSceneBookNowMenu_Coroutine(){
            OnClick_CloseUnitDetails();
            yield return new WaitForSeconds(0.3f);
            _queryForm.SetActive(true);

        }

        public void OnClick_MainMenuExploreUnits()
        {
            SceneManager.LoadScene(1);
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

        public void OnClick_ShowUI(bool isEnabled)
        {
            GameEvents.ToggleUI(isEnabled);
        }
        #endregion

        #region FloorView Scene Callbacks

        public void OnClick_ExploreSceneBack()
        {
            SceneManager.LoadScene(0);
        }
        public void OnClick_ExploreScene2DView()
        {
           _2DImage.SetActive(true);
        }
        public void OnClick_ExploreSceneClose2DView()
        {
            _2DImage.SetActive(false);
        }
        public void OnClick_ExploreSceneBookNow()
        {
            _queryForm.SetActive(true);
        }
        #endregion
        public void Dummy()
        {
            print("I was clicked");
        }
        
    }
}