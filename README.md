[droidDark]: ghImages/AndroidDarkMode.png "Android in Dark Mode"
[droidLight]: ghImages/AndroidLightMode.png "Android in Light Mode"
[iOSDark]: ghImages/iOSDarkMode.png "iOS in Dark Mode"
[iOSLightAtLaunch]: ghImages/iOSLightModeAtLaunch.png "iOS in Light Mode at App Launch"
[iOSLightAfterDark]: ghImages/iOSLightModeAfterDarkMode.png "iOS in Light Mode after app has been running in Dark Mode"

# Update (2020.04.30)
Wonderful news! The Xamarin.Forms team has [resolved the issue](https://github.com/xamarin/Xamarin.Forms/issues/8920) this repo was designed to demonstrate. I will leave this as is for anyone who wants to see the issue, but if you upgrade to [XF 4.6](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/release-notes/4.6/4.6.0), the issue should be gone. Big thanks to the Xamarin.Forms team for all their hard work, and for fixing this issue!

# FontIconColorChangeIssue
This sample app is designed to demonstrate a UI bug in supporting dark mode in Xamarin.Forms. I discovered the problem while working on an app and attempting to support both light & dark mode for iOS and Android. What you'll see in this sample app is a single view with a button and a CollectionView. The CollectionView is populated with some fake data generated in the MainPageViewModel class.

Using the [Material Design Icon font](https://materialdesignicons.com/), each row of the collection view has two or three icons:
* Left icon:  Designed to be a visual indicator of what this row is about.
* "New" icon:  This is appended immediately after the text description of the row, as if it was something "new" that should be highlighted.
* Right icon:  Simply a right-facing chevron designed to indicate that a tap on the row would navigate to another page for more details (note that this small sample does not include said navigation... Tapping on the rows will not do anything).

## Android
On Android devices new enough to support dark mode, you can see the expected behavior: You can launch the app in either dark or light mode and all text, colors, and icons display appropriately. Switch modes while the app is running in the background, and the app will update its look when it returns to the foreground.
![droidLight] ![droidDark]

## iOS
The bug I am demonstrating has only occurred on iOS devices for me. Here is a quick summary:
* Launch the app with the phone in light mode, and everything displays correctly:
    ![iOSLightAtLaunch]
* Whether you launch the app in dark mode or switch to dark mode while the app is running in the background, you should see that many (sometimes all) of the icons do not switch to their correct look (the icons at far-left and far-right should all be white):
![iOSDark]
* Switching the app back and forth from light to dark mode and vice-versa, you should see that some rows get updated icons, others do not.
    * Since there are enough items to scroll, if you scroll up & down in dark mode, you will often (but not always) see that *which icons have updated their look to the appropriate mode will actually change* when you scroll an item out of and then back into view.
* The screenshot below shows iOS in light mode after the app was in dark mode. The icons appear to be missing, but they are just white on a white background, making them essentially invisible.
![iOSLightAfterDark]
