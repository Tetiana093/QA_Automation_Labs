using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingSeleniumTests_L2.Pages
{
    public class ManagerPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        public ManagerPage(IWebDriver driver)
        {
            _driver = driver;
            // Розумне очікування (до 10 секунд), поки кнопка з'явиться на екрані
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        }

        // --- ЛОКАТОРИ (адреси кнопок на сайті) ---
        private By bankManagerLoginBtn = By.XPath("//button[contains(text(), 'Bank Manager Login')]");
        private By customersTabBtn = By.XPath("//button[contains(text(), 'Customers')]");
        private By firstNameColumn = By.XPath("//a[contains(text(), 'First Name')]");
        // Локатор для збору всіх імен у таблиці (перша колонка)
        private By customerFirstNames = By.XPath("//tbody/tr/td[1]");

        // --- МЕТОДИ (дії, які ми робимо мишкою) ---
        public void ClickBankManagerLogin()
        {
            _wait.Until(d => d.FindElement(bankManagerLoginBtn)).Click();
        }

        public void GoToCustomersTab()
        {
            _wait.Until(d => d.FindElement(customersTabBtn)).Click();
        }

        public void ClickFirstNameColumn()
        {
            _wait.Until(d => d.FindElement(firstNameColumn)).Click();
        }

        // Метод, який збирає всі імена в список, щоб ми могли перевірити, чи вони відсортовані
        public List<string> GetFirstNamesList()
        {
            var elements = _wait.Until(d => d.FindElements(customerFirstNames));
            return elements.Select(e => e.Text).ToList();
        }
    }
}