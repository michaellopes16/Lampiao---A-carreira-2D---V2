using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ADS : MonoBehaviour {

	// Use this for initialization
	void Start () {
		showBannerAd ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void showBannerAd(){
	
		string adID = "sdasdasdasfasfafaf";

		// para testar no dispositivo
		AdRequest request = new AdRequest.Builder ()
			.AddTestDevice (AdRequest.TestDeviceSimulator) // Simulador
			.AddTestDevice ("----------------------") //Dispositivo de teste
			.Build();

		//**** Para o produto quando for upar o aplicativo
		// AdResquest resquest = new AdRequest.Builder().Build();

		BannerView bannerAd = new BannerView (adID, AdSize.SmartBanner,AdPosition.Top);
	
	}
}
