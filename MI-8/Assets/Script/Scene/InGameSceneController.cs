using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class InGameSceneController : MonoBehaviour
{
    [SerializeField]
    private List<Disturbance> disturbances = new List<Disturbance>();
    private List<Disturbance> inactivatedDisturbances = new List<Disturbance>();

    // [SerializeField]
    // private Button retry;

    private void Awake()
    {
        disturbances.ForEach(disturbance =>
        {
            disturbance.OnInactivatedAsObservable()
                .Subscribe(disturbanceObj =>
                {
                    inactivatedDisturbances.Add(disturbanceObj);
                })
                .AddTo(this);
        });

        // retry.OnClickAsObservable()
        //     .Subscribe(_ => ClearAllObj())
        //     .AddTo(this);
    }

    private void ClearAllObj()
    {
        inactivatedDisturbances.ForEach(disturbance =>
        {
            disturbance.Activate();
            // reset the player character
        });
    }
}
