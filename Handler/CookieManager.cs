namespace FirstTest.Handler;

using OpenQA.Selenium;
using System.IO;
using System.Text.Json;

public class CookieManager
{
    private IWebDriver driver;
    private string filePath;
    public bool cookiedLoaded = false;
    public CookieManager(IWebDriver driver, string filePath)
    {
        this.driver = driver;
        this.filePath = filePath;
    }

    public void SaveCookies()
    {
        var cookies = driver.Manage().Cookies.AllCookies;
        var serializableCookies = cookies.Select(c => new SerializableCookie(c)).ToList();
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }
        File.WriteAllText(filePath, JsonSerializer.Serialize(serializableCookies));
    }

    public void LoadCookies()
    {
        if (!File.Exists(filePath))
        {
            List<SerializableCookie> serializableCookies = JsonSerializer.Deserialize<List<SerializableCookie>>(File.ReadAllText(filePath));
        foreach (var serializableCookie in serializableCookies.FindAll(cookie => cookie.Domain.Contains("gameforge")))
        {
            driver.Manage().Cookies.AddCookie(serializableCookie.ToSeleniumCookie());
        }
        cookiedLoaded = true;
        }
        
    }
}


public class SerializableCookie
{
    public string Domain { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public string Path { get; set; }
    public DateTime? Expiry { get; set; }
    public bool Secure { get; set; }
    public bool HttpOnly { get; set; }

    public SerializableCookie() { }

    public SerializableCookie(Cookie cookie)
    {
        Domain = cookie.Domain;
        Name = cookie.Name;
        Value = cookie.Value;
        Path = cookie.Path;
        Expiry = cookie.Expiry;
        Secure = cookie.Secure;
        HttpOnly = cookie.IsHttpOnly;
    }

    public Cookie ToSeleniumCookie()
    {
        return new Cookie(Name, Value, Domain, Path, Expiry);
    }
}

