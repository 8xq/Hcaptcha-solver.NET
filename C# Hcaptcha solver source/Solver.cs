using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using System.Threading;
namespace HcaptchaSolverNET
{
    public class Solver
    {
        private static int MaximumRetries = 5; // Maximum retries if captcha has no result <3
        private static int Timeout = 5000; // Wait time between retries (1000ms - 1 second) - recommend you keep this above 3000
        private static string API_Key; // Used to store API key
        public string apikey { get { return API_Key; } set { API_Key = value; } } // Create a public accessor to set API key


        // This method will be used to prepare for captcha solving :)
        // Using 2captcha API it will take the target URL & sitekey to give captcha to a "solver"
        // If 2captcha takes the request a response of 'OK|' + a captcha ID will be returned :)
        public string GetCaptchaToken(string SiteKey, string PageURL , out bool Result)
        {
            HttpRequest req = new HttpRequest();
            try
            {
                string GetCaptcha = req.Get("https://2captcha.com/in.php?key=" + API_Key + "&method=hcaptcha&sitekey=" + SiteKey + "&pageurl=" + PageURL + "&soft_id=2640").ToString();
                if (GetCaptcha.Substring(0, 3) == "OK|")
                {
                    string captchaID = GetCaptcha.Remove(0, 3);
                    Result = true;
                    return captchaID;
                }
                else if (GetCaptcha.Contains("ERROR_WRONG_USER_KEY"))
                {
                    Result = false;
                    return "Error wrong user key - please check captcha key !";
                }
            }
            catch (Exception ex)
            {
                Result = false;
                return "Error grabbing captcha - please check request details !";
            }
            Result = false;
            return "Global unknown error";
        }

        // This is the main method that will handle the "solving"
        // This method simply uses the captcha ID to check if the captcha has been solved yet
        // If the captcha response is 'OK|' it will return the captcha token
        // If the captcha response is not 'OK|' it will re-attempt to check according to settings
        // Default re-tries is set to '5' with a 5 second delay between each attempt
        public string GetSolvedCaptchaID(string CaptchaID, out bool Result)
        {
            HttpRequest req = new HttpRequest();
            try
            {
                int i = 0;
                RETRY:
                string CheckCaptcha = req.Get("https://2captcha.com/res.php?key=" + API_Key + "&action=get&id=" + CaptchaID).ToString();
                if (CheckCaptcha.Substring(0, 3) == "OK|")
                {
                    string FinalCaptcha = CheckCaptcha.Remove(0, 3);
                    Result = true;
                    return FinalCaptcha;
                }
                else if (CheckCaptcha.Contains("ERROR_WRONG_CAPTCHA_ID"))
                {
                    Result = false;
                    return "Incorrect captcha ID from 2captcha API";
                }
                else
                {
                    i++;
                    Thread.Sleep(Timeout);
                    if (i <= MaximumRetries)
                    {
                        goto RETRY;
                    }
                    else if (i >= MaximumRetries)
                    {
                        Result = false;
                        return "Failed to grab captcha result - exceeded maximum retries";
                    }
                }
            }
            catch (Exception ex)
            {
                Result = false;
                return "Error grabbing captcha response";
            }
            Result = false;
            return "Global unknown error";
        }
    }
}
