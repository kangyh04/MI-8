using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ObservableInput : MonoBehaviour
{
    [SerializeField]
    private float InaccessibleTime = 0f;

    private static ObservableInput instance;
    public static ObservableInput Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ObservableInput>() ?? null;
                if (instance == null)
                {
                    var go = new GameObject("ObservableInput");
                    instance = go.AddComponent<ObservableInput>();
                }
            }
            return instance;
        }
    }

    private InGameSceneController InGameSceneController
    {
        get { return InGameSceneController.Instance; }
    }

    public IObservable<GameObject> OnTappedAsObservable()
    {
        return Observable.EveryUpdate()
            .Select(_ => Input.GetMouseButtonDown(0))
            .Where(inputted => inputted)
            .Select(_ =>
            {
                // NOTE : for collider
                // RaycastHit hit;
                // var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Physics.Raycast(ray.origin, ray.direction * 10, out hit);
                // return hit;

                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
                return hit;
            })
            .Where(hittedObj => hittedObj.collider != null)
            .Select(hittedObj => hittedObj.collider.gameObject)
            .Zip(
                InGameSceneController.Disposers.ToObservable(),
                (hittedObj, targetObj) => Tuple.Create(hittedObj, targetObj.gameObject))
            .TakeWhile(item =>
            {
                var hittedObj = item.Item1;
                var currentTarget = item.Item2;
                return hittedObj == currentTarget;
            })
            .Select(item => item.Item1)
//             .Skip(TimeSpan.FromSeconds(InaccessibleTime))
            .Share();
    }
}
