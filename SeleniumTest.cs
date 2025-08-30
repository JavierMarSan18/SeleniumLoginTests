using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.Extensions.Configuration;

namespace SeleniumLoginTests
{
    public class LoginTests
    {
        private ChromeDriver driver;
        private string baseUrl;
        private string testEmail;
        private string testPassword;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            testEmail = config["TestUser:Email"]!;
            testPassword = config["TestUser:Password"]!;
            baseUrl = config["BaseUrl"]!;

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Login_Exitoso()
        {
            driver.Navigate().GoToUrl($"{baseUrl}/login");

            driver.FindElement(By.CssSelector(".MuiInputBase-input.MuiFilledInput-input.MuiInputBase-inputSizeSmall.mui-146q1ys"))
                .SendKeys(testEmail);

            driver.FindElement(By.CssSelector(".MuiInputBase-input.MuiFilledInput-input.MuiInputBase-inputSizeSmall.MuiInputBase-inputAdornedEnd.mui-1yvh7bn"))
                .SendKeys(testPassword);

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            bool urlOk = wait.Until(d => d.Url.Contains("/home"));

            Assert.That(urlOk, "El login no fue exitoso");
        }

        [Test]
        public void Navegacion_Empleados()
        {
            driver.Navigate().GoToUrl($"{baseUrl}/login");

            driver.FindElement(By.CssSelector(".MuiInputBase-input.MuiFilledInput-input.MuiInputBase-inputSizeSmall.mui-146q1ys"))
                .SendKeys(testEmail);

            driver.FindElement(By.CssSelector(".MuiInputBase-input.MuiFilledInput-input.MuiInputBase-inputSizeSmall.MuiInputBase-inputAdornedEnd.mui-1yvh7bn"))
                .SendKeys(testPassword);

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("/home"));

            driver.FindElement(By.CssSelector("a[href='/Empleados']")).Click();

            wait.Until(d => d.Url.Contains("/Empleados"));

            Assert.That(driver.Url, Does.Contain("/Empleados"), "No se navegó a la página de empleados");
        }

        [Test]
        public void Navegacion_Configuracion()
        {
            driver.Navigate().GoToUrl($"{baseUrl}/login");

            driver.FindElement(By.CssSelector(".MuiInputBase-input.MuiFilledInput-input.MuiInputBase-inputSizeSmall.mui-146q1ys"))
                .SendKeys(testEmail);

            driver.FindElement(By.CssSelector(".MuiInputBase-input.MuiFilledInput-input.MuiInputBase-inputSizeSmall.MuiInputBase-inputAdornedEnd.mui-1yvh7bn"))
                .SendKeys(testPassword);

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("/home"));

            driver.FindElement(By.CssSelector("a[href='/configuracion']")).Click();

            wait.Until(d => d.Url.Contains("/configuracion"));

            Assert.That(driver.Url, Does.Contain("/configuracion"), "No se navegó a la página de configuración");
        }

        [Test]
        public void Validar_PageNotFound()
        {
            driver.Navigate().GoToUrl($"{baseUrl}/login");

            driver.FindElement(By.CssSelector(".MuiInputBase-input.MuiFilledInput-input.MuiInputBase-inputSizeSmall.mui-146q1ys"))
                .SendKeys(testEmail);

            driver.FindElement(By.CssSelector(".MuiInputBase-input.MuiFilledInput-input.MuiInputBase-inputSizeSmall.MuiInputBase-inputAdornedEnd.mui-1yvh7bn"))
                .SendKeys(testPassword);

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("/home"));

            driver.Navigate().GoToUrl($"{baseUrl}/dashboard");

            var notFoundMsg = wait.Until(d =>
                d.FindElement(By.CssSelector("h4.MuiTypography-root.MuiTypography-h4"))
            );
            Assert.That(notFoundMsg.Text, Does.Contain("Page Not Found"),
                "El mensaje de error no es el esperado");

            var backBtn = driver.FindElement(By.LinkText("Back To Home"));
            Assert.That(backBtn.Displayed, "No se encontró el botón 'Back To Home'");

            backBtn.Click();

            wait.Until(d => d.Url.Contains("login") || d.Url.Contains("home"));

            Assert.That(driver.Url, Does.Contain("/home"),
                $"BUG DETECTADO: el botón 'Back To Home' redirigió a {driver.Url} en lugar de /home");
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
