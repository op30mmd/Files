// Copyright (c) Files Community
// Licensed under the MIT License.

using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace Files.InteractionTests.Helper
{
	public static class TestHelper
	{
		private static readonly WebDriverWait wait = new(SessionManager.Session, TimeSpan.FromSeconds(10));

		public static ICollection<WindowsElement> GetElementsOfType(string elementType)
			=> SessionManager.Session.FindElementsByTagName(elementType);

		public static List<WindowsElement> GetElementsOfTypeWithContent(string elementType, string content)
			=> GetItemsWithContent(GetElementsOfType(elementType), content);

		public static List<WindowsElement> GetItemsWithContent(ICollection<WindowsElement> elements, string content)
		{
			List<WindowsElement> elementsToReturn = [];
			foreach (WindowsElement element in elements)
			{
				if (element.Text.Contains(content, StringComparison.OrdinalIgnoreCase))
				{
					elementsToReturn.Add(element);
					continue;
				}
				// Check children if we did not find it in the items name
				System.Collections.ObjectModel.ReadOnlyCollection<OpenQA.Selenium.Appium.AppiumWebElement> children = element.FindElementsByTagName("Text");
				foreach (OpenQA.Selenium.Appium.AppiumWebElement child in children)
				{
					if (child.Text.Contains(content, StringComparison.OrdinalIgnoreCase))
					{
						elementsToReturn.Add(element);
					}
				}
			}
			return elementsToReturn;
		}

		public static void InvokeButtonByName(string uiaName)
			=> SessionManager.Session.FindElementByName(uiaName).Click();

		public static WindowsElement FindElementByNameWithWait(string uiaName)
			=> wait.Until(e => e.FindElementByName(uiaName) as WindowsElement);

		public static void InvokeButtonByNameWithWait(string uiaName)
			=> FindElementByNameWithWait(uiaName).Click();

		public static void InvokeButtonById(string uiaName)
					=> SessionManager.Session.FindElementByAccessibilityId(uiaName).Click();

		public static WindowsElement FindElementByIdWithWait(string uiaName)
			=> wait.Until(e => e.FindElementByAccessibilityId(uiaName) as WindowsElement);

		public static void InvokeButtonByIdWithWait(string uiaName)
			=> FindElementByIdWithWait(uiaName).Click();
	}
}