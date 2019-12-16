# FontIconColorChangeIssue
This sample app is designed to demonstrate a UI bug in supporting dark mode in Xamarin.Forms. I discovered the problem while working on an app and attempting to support both light & dark mode for iOS and Android. What you'll see in this sample app is a single view with a button and a CollectionView. The CollectionView is populated with some fake data generated in the MainPageViewModel class.

Using the [Material Design Icon font](https://materialdesignicons.com/), each row of the collection view has two or three icons:
* Left icon:  Designed to be a visual indicator of what this row is about.
* "New" icon:  This is appended immediately after the text description of the row, as if it was something "new" that should be highlighted.
* Right icon:  Simply a right-facing chevron designed to indicate that a tap on the row would navigate to another page for more details (note that this small sample does not include said navigation... Tapping on the rows will not do anything).

## Android
On Android devices new enough to support dark mode, you can see the expected behavior: You can launch the app in either dark or light mode and all text, colors, and icons display appropriately. Switch modes while the app is running in the background, and the app will update its look when it returns to the foreground.

## iOS
The bug I am demonstrating has only occurred on iOS devices for me. Here is a quick summary:
* Launch the app with the phone in light mode, and everything displays correctly.
* Launch the app in dark mode, and you should see that many (sometimes all) of the icons do not switch to their correct look.
* Switching the app back and forth from light to dark mode and vice-versa, you should see that some rows get updated icons, others do not.
    * Since there are enough items to scroll, if you scroll up & down in dark mode, you will often (but not always) see that which icons have updated their look to the appropriate mode will actually change when you scroll an item out of and then back into view.
