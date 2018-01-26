using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ObservableInput : MonoBehaviour
{
    public static IObservable<GameObject> OnTappedAsObservable()
    {
        return Observable.EveryUpdate()
            .Select(_ => Input.GetMouseButtonDown(0))
            .Where(inputted => inputted)
            .Select(_ =>
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray.origin, ray.direction * 10, out hit);
                return hit;
            })
            .Where(hittedObj => hittedObj.collider != null)
            .Select(hittedObj => hittedObj.collider.gameObject)
            .Share();
    }
}
