using System;
using GoogleMobileAds.Api;

public class AdsScript {
    public static Boolean adInitialized = false;

    public static void InitAds()
    {
        if (!adInitialized)
        {
#if UNITY_ANDROID
            string appId = "ca-app-pub-5908593459613952~9748087693";
#elif UNITY_IPHONE
                string appId = "ca-app-pub-3940256099942544~1458002511";
#else
                string appId = "unexpected_platform";
#endif

            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(appId);
            adInitialized = true;
        }
    }
}
