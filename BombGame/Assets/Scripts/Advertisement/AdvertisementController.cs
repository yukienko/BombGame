using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisementController : SingletonMonoBehaviour<AdvertisementController>
{
    protected override bool dontDestroy { get => true;}

    private static readonly string BannerId = "banner";
    private static readonly string VideoId = "video";

    public Action onFinished { get; set; }
    public Action onFailed { get; set; }
    public Action onSkipped { get; set; }

    public void Init()
    {
        string gameID = "";
#if UNITY_ANDROID
        gameID = "";
#endif
        Advertisement.Initialize(gameID, testMode: true, enablePerPlacementLoad: false);
    }
}
