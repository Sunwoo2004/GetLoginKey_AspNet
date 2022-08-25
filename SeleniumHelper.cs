using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Threading;

namespace GetLoginKey_WebClient
{
    public class SeleniumHelper
    {
        static ChromeDriverService _driverService = null;
        static ChromeOptions _options = null;
        static ChromeDriver _driver = null;
        static string m_szLoginUrl_Valofe = "https://member.valofe.com/login/login.asp?ret=http%3A%2F%2Flostsaga-ko.valofe.com%2Fmain%2Fmain.asp";
        static string m_szLoginUrl_MGame = "https://msign.mgame.com/login/?tu=http://lostsaga.mgame.com";
        static string m_szExecUrl_Valofe = "http://lostsaga-ko.valofe.com/play/playUrl.asp";
        static string m_szExecUrl_MGame = "http://lostsaga.mgame.com/play/playUrl.asp";
        static string m_szResult = "";
        static string m_szChromeDriverPath = "C:\\Users\\Admin\\Desktop\\개발\\LSKR_Login\\GetLoginKey_WebClient\\bin";
        //static string m_szChromeDriverPath = "C:\\Users\\Administrator\\Desktop\\LSLogin\\bin";
        public static string GetLoginKey_Valofe(string szUserID, string szUserPWD)
        {
            Init();
            _driver.Url = m_szLoginUrl_Valofe;
            _driver.ExecuteScript($"document.getElementById('uid').value = '{szUserID}'");
            _driver.ExecuteScript($"document.getElementById('passwd').value = '{szUserPWD}'");
            _driver.ExecuteScript($"document.getElementsByClassName('btnLogin')[0].click()");
            try
            {
                _driver.Url = m_szExecUrl_Valofe;
                m_szResult = _driver.FindElementById("playgame").GetAttribute("href");
            }
            catch //계정이 존재하지 않을경우
            {
                m_szResult = "errorcode2";
            }

            _driver.Quit();

            return m_szResult;
        }

        public static string GetLoginKey_MGame(string szUserID, string szUserPWD)
        {
            Init();
            _driver.Url = m_szLoginUrl_MGame;
            _driver.ExecuteScript($"document.getElementById('_mgid_enc').value = '{"error"}'");
            _driver.ExecuteScript($"document.getElementById('_mgpwd_enc').value = '{"error"}'");
            _driver.FindElementByXPath("//*[@id='loginForm']/div[2]/div[1]/input[3]").Click();
            //_driver.ExecuteScript($"chkSubmit()");
            try
            {
                _driver.Url = m_szExecUrl_MGame;
                //m_szResult = _driver.FindElementById("playgame").GetAttribute("href");
            }
            catch //계정이 존재하지 않을경우
            {
                m_szResult = "errorcode2";
            }

            _driver.Quit();

            return m_szResult;
        }

        public static void Init()
        {
            _driverService = ChromeDriverService.CreateDefaultService(m_szChromeDriverPath);
            _driverService.HideCommandPromptWindow = true;

            _options = new ChromeOptions();
            _options.AddArgument("window-size=1920,1000");
            //_options.AddArgument("headless");
            _options.AddArgument("disable-gpu");
            _options.AddArgument("ignore-certificate-errors");
            _options.AddArgument("lang=ko");
            _options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.20 Safari/537.36");
            _driver = new ChromeDriver(_driverService, _options);
        }
    }
}