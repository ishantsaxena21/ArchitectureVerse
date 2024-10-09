using AVerse.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalTranisitionCamera : MonoBehaviour
{

    [SerializeField] CameraTransitionModel _transitionModel;
    private Coroutine _transitionCorotuine;

    float _transitionProgress;
    bool _isTransitioning;

    static UniversalTranisitionCamera _instance;
    public static UniversalTranisitionCamera Instance {  get { return _instance; } }

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        gameObject.SetActive(false);
        _isTransitioning = false;
    }
    private void OnEnable()
    {
        _isTransitioning = false;
    }
    public void StartTransition(Transform startTransform, Transform endTransform)
    {
        print($"Start Pos for {startTransform.parent.name} : {startTransform.position}");
        print($"End Pos for {endTransform.parent.name} : {endTransform.position}");
        if (!_isTransitioning)
        {
            _transitionProgress = 0.0f;
            _transitionModel.startTransform = startTransform;
            _transitionModel.endTransform = endTransform;
            transform.SetPositionAndRotation(startTransform.position, startTransform.rotation);
            gameObject.SetActive(true);
            
            StartCoroutine(Transition(_transitionModel));
        }
    }

    IEnumerator Transition(CameraTransitionModel transitionModel)
    {
        print("Coroutine Started");        
        while (_transitionProgress < 1.0f)
        {
            _transitionProgress += Time.deltaTime / transitionModel.transitionDuration;
            print($"increasing transiiton by : {_transitionProgress}");
            transform.position = Vector3.Lerp(transitionModel.startTransform.position, transitionModel.endTransform.position, _transitionProgress);
            transform.rotation = Quaternion.Slerp(transitionModel.startTransform.rotation, transitionModel.endTransform.rotation, _transitionProgress);
            yield return null;
        }
        print("Transition Completed");
        _transitionProgress = 1.0f;
        _isTransitioning = false;
        GameEvents.TransitionCompleted();
        transitionModel.startTransform = null;
        transitionModel.endTransform = null;
        gameObject.SetActive(false);
    }
}
