﻿using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Configuration;
using OpenQA.Selenium.Interactions;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");

            using (IWebDriver driver = new ChromeDriver(options))
            {
                driver.Navigate().GoToUrl("https://login.yahoo.com/");

                IWebElement username = driver.FindElement(By.Id("login-username"));
                username.SendKeys("vrishalipal");
                username.Submit();
               
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement password = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("login-passwd")));

                string ypwd = ConfigurationManager.AppSettings["ypwd"];
                password.SendKeys(ypwd);
                password.SendKeys(Keys.Return);

                WebDriverWait waitForFinanceLink = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                
                //IWebElement financeLink = waitForFinanceLink.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='https://finance.yahoo.com/']")));
                IWebElement financeLink = waitForFinanceLink.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='mega-bottombar']/ul/li[3]/a[@href='https://finance.yahoo.com/']")));

                try
                {
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
                    financeLink.Click(); 
                }
                catch (WebDriverTimeoutException) { }
                finally
                {
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
                }

                Console.WriteLine("Title: " + driver.Title);

                WebDriverWait waitForFolioList = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IWebElement folioList = waitForFolioList.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("My Portfolio")));
                folioList.Click();

                WebDriverWait waitForFolio = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IWebElement folio = waitForFolioList.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("ul[data-test='secnav-list'] li:nth-child(2)>a[title='Solid Folio']")));
                folio.Click();

                Thread.Sleep(10000);
                driver.Quit();
            }
        }
    }
}
