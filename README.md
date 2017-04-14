# TYLGallery

This application allows the user to view random images from the database.
The user can choose to either Like or Dislike the image, which is stored in the database, and can be viewed later on the "My Choices" page.

The images are uploaded by the admin user that is created on the first run of the application.

<b>Assumptions:</b>
1. No paging on My Choices page, so if there are too many images to load it may take a long time or the application may break.
2. No general users can be created. The users are identified by cookie data, which gets created once they submit their first feedback. That means the user can be identified only on the same browser on the same device, provided the user does not clear cookies.
3. There is only one Admin user, and currently there is no provision to create another one.
4. The application requires javascript to be enabled on the user's browser.
5. The application is primarily tested on the latest versions of IE, Chrome, and Edge on a computer.
6. It does not include any unit test projects.
7. It requires a connection with SQL server to run.
