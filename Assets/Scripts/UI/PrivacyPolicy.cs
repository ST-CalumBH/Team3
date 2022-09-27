using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class PrivacyPolicy : MonoBehaviour
{
    // Store whether opt in consent is required, and what consent ID to use
    string consentIdentifier;
    bool consentHasBeenChecked = true;
  

    // Start is called before the first frame update
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
            //GenerateUserName();
            consentHasBeenChecked = true;
            OptOut();
        }
        catch (ConsentCheckException e)
        {
            Debug.LogException(e);
            // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately
        }
    }

    public string GenerateUserName()
    {
        string username = "";
        for (int i = 0; i < 10; i++)
        {
            username += Random.Range(0, 10).ToString();
        }
        Debug.Log(username);
        return username;
    }

    public void OptOut()
    {
        try
        {
            if (!consentHasBeenChecked)
            {
                // Show a GDPR/COPPA/other opt-out consent flow    
                // If a user opts out
                AnalyticsService.Instance.OptOut();
            }
            // Record that we have checked a user's consent, so we don't repeat the flow unnecessarily. 
            // In a real game, use PlayerPrefs or an equivalent to persist this state between sessions
            consentHasBeenChecked = true;
            AnalyticsService.Instance.ProvideOptInConsent("PIPL", consentHasBeenChecked);
            AnalyticsService.Instance.ProvideOptInConsent("GDPR", consentHasBeenChecked);
        }
        catch (ConsentCheckException e)
        {
            Debug.LogException(e);
            // Handle the exception by checking e.Reason
        }
    }

    public void onShowPrivacyPageButtonPressed()
    {
        // Open the Privacy Policy in the system's default browser
        Application.OpenURL(AnalyticsService.Instance.PrivacyUrl);
    }
}
