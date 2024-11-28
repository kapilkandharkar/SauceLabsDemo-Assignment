using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanTech.Common
{
    public static class WebCommon
    {

        public static void SetText(this IWebElement element, string valueToSet)
        {
            element.Clear();
            element.SendKeys(valueToSet);
        }

        public static void SelectDropDownByText(this IWebElement element, string valueToSet)
        {
            var selectElement = new SelectElement(element);
            selectElement.SelectByText(valueToSet);
        }



    }
}
