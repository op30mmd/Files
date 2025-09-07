// Copyright (c) Files Community
// Licensed under the MIT License.

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Files.InteractionTests.Tests
{
	[TestClass]
	public sealed class FolderTests
	{

		[TestCleanup]
		public void Cleanup()
		{
			// Navigate back home
			TestHelper.InvokeButtonByIdWithWait("Home");
		}

		[TestMethod]
		public void TestFolders()
		{
			NavigationTest();

			CreateFolderTest();

			RenameFolderTest();

			CopyPasteFolderTest();

			DeleteFolderTest();
		}

		/// <summary>
		/// Tests folder navigation
		/// </summary>
		private void NavigationTest()
		{
			// Click on the desktop item in the sidebar
			TestHelper.InvokeButtonByIdWithWait("Desktop");

			// Wait for the desktop folder to load
			TestHelper.FindElementByIdWithWait("Horizontal-grid");
		}


		/// <summary>
		/// Tests folder creation and checks for accessibility issues along the way
		/// </summary>
		private void CreateFolderTest()
		{
			// Click the "New" button on the toolbar
			TestHelper.InvokeButtonByIdWithWait("InnerNavigationToolbarNewButton");

			// Click the "Folder" item from the menu flyout
			TestHelper.InvokeButtonByIdWithWait("InnerNavigationToolbarNewFolderButton");

			// Check for accessibility issues in the new folder prompt
			AxeHelper.AssertNoAccessibilityErrors();

			// Type the folder name
			var action = new Actions(SessionManager.Session);
			action.SendKeys("New Folder").Perform();

			// Press the enter button to confirm
			action = new Actions(SessionManager.Session);
			action.SendKeys(Keys.Enter).Perform();

			// Wait for folder to be created
			TestHelper.FindElementByNameWithWait("New Folder");

			// Check for accessibility issues in the file area
			AxeHelper.AssertNoAccessibilityErrors();
		}

		/// <summary>
		/// Tests renaming a folder
		/// </summary>
		private void RenameFolderTest()
		{
			// Click the "Rename" button on the toolbar
			TestHelper.InvokeButtonByIdWithWait("InnerNavigationToolbarRenameButton");

			// Type the new name into the inline text box
			var action = new Actions(SessionManager.Session);
			action.SendKeys("Renamed Folder").Perform();

			// Press the enter button to save the new name
			action = new Actions(SessionManager.Session);
			action.SendKeys(Keys.Enter).Perform();

			// Wait for the folder to be renamed
			TestHelper.FindElementByNameWithWait("Renamed Folder");
		}

		/// <summary>
		/// Tests copying and pasting a folder
		/// </summary>
		private void CopyPasteFolderTest()
		{
			// Click the "copy" button on the toolbar
			TestHelper.InvokeButtonByIdWithWait("InnerNavigationToolbarCopyButton");

			// Click the "paste" button on the toolbar
			TestHelper.InvokeButtonByIdWithWait("InnerNavigationToolbarPasteButton");

			// Wait for folder to be pasted
			TestHelper.FindElementByNameWithWait("Renamed Folder - Copy");
		}

		/// <summary>
		/// Tests deleting folders
		/// </summary>
		private void DeleteFolderTest()
		{
			// Select the "Renamed Folder" folder and clicks the "delete" button on the toolbar
			TestHelper.InvokeButtonByNameWithWait("Renamed Folder");
			TestHelper.InvokeButtonByIdWithWait("InnerNavigationToolbarDeleteButton");

			// Check for accessibility issues in the confirm delete prompt
			AxeHelper.AssertNoAccessibilityErrors();

			// Press the enter key to confirm
			var action = new Actions(SessionManager.Session);
			action.SendKeys(Keys.Enter).Perform();


			// Select the "Renamed Folder - Copy" folder and clicks the "delete" button on the toolbar
			TestHelper.InvokeButtonByNameWithWait("Renamed Folder - Copy");
			TestHelper.InvokeButtonByIdWithWait("InnerNavigationToolbarDeleteButton");

			// Check for accessibility issues in the confirm delete prompt
			AxeHelper.AssertNoAccessibilityErrors();

			// Press the enter key to confirm
			action = new Actions(SessionManager.Session);
			action.SendKeys(Keys.Enter).Perform();
		}
	}
}
