using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace GetLoginKey_WebClient
{
    public class SeleniumHelper
    {
        static ChromeDriverService _driverService = null;
        static ChromeOptions _options = null;
        static ChromeDriver _driver = null;
        static string m_szLoginUrl = "https://member.valofe.com/login/login.asp?ret=http%3A%2F%2Flostsaga-ko.valofe.com%2Fmain%2Fmain.asp";
        static string m_szExecUrl = "http://lostsaga-ko.valofe.com/play/playUrl.asp";
        static string m_szResult = "";
        static string m_szChromeDriverPath = "C:\\Users\\Administrator\\Desktop\\LSLogin\\bin";
        public static string GetLoginKey(string szUserID, string szUserPWD)
        {
            Init();
            _driver.Url = m_szLoginUrl;
            _driver.ExecuteScript($"document.getElementById('uid').value = '{szUserID}'");
            _driver.ExecuteScript($"document.getElementById('passwd').value = '{szUserPWD}'");
            _driver.ExecuteScript($"document.getElementsByClassName('btnLogin')[0].click()");
            try
            {
                _driver.Url = m_szExecUrl;
                m_szResult = _driver.FindElementById("playgame").GetAttribute("href");
            }
            catch
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
            _options.AddArgument("headless");
            _options.AddArgument("disable-gpu");
            _options.AddArgument("ignore-certificate-errors");
            _options.AddArgument("lang=ko");
            _options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.20 Safari/537.36");
            _driver = new ChromeDriver(_driverService, _options);
        }
    }
}