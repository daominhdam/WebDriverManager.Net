﻿using System.Linq;
using System.Net;
using AngleSharp;
using AngleSharp.Parser.Html;

namespace WebDriverManager.DriverConfigs.Impl
{
    public class FirefoxConfig : IDriverConfig
    {
        public virtual string GetName()
        {
            return "Firefox";
        }

        public virtual string GetUrl32()
        {
            return GetUrl64();
        }

        public virtual string GetUrl64()
        {
            return "https://github.com/mozilla/geckodriver/releases/download/v<version>/geckodriver-v<version>-win64.zip";
        }

        public virtual string GetBinaryName()
        {
            return "geckodriver.exe";
        }

        public virtual string GetLatestVersion()
        {
            using (var client = new WebClient())
            {
                var htmlCode = client.DownloadString("https://github.com/mozilla/geckodriver/releases");
                var parser = new HtmlParser(Configuration.Default.WithDefaultLoader());
                var document = parser.Parse(htmlCode);
                var version = document.QuerySelectorAll("[class='release-title'] a")
                    .Select(element => element.TextContent)
                    .FirstOrDefault()
                    ?.Remove(0, 1);
                return version;
            }
        }
    }
}