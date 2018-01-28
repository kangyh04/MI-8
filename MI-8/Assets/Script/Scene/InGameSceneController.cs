using System;
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

    private static InGameSceneController instance;
    public static InGameSceneController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<InGameSceneController>() ?? null;
                if (instance == null)
                {
                    var go = new GameObject("InGameSceneController");
                    instance = go.AddComponent<InGameSceneController>();
                }
            }
            return instance;
        }
    }

    public IEnumerable<DisturbanceDisposer> Disposers
    {
        get
        {
            return disturbances
                .Aggregate(
                new List<DisturbanceDisposer>(),
                (container, disturbance) =>
                {
                    container.AddRange(
                        disturbance.Disposers
                        .Where(disposer => disposer.DisposerType == DisposerType.Correct));
                    return container;
                });
        }
    }

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
