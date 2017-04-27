# TYLGallery

This application allows the user to view random images from the database.
The user can choose to either Like or Dislike the image, which is stored in the database, and can be viewed later on the "My Choices" page.
"My Choices" page displays latest images first, page by page.

The images are uploaded by the admin user that is created on the first run of the application.

<b>Assumptions:</b>
1. Application is configured to log events using Serilog framework to Seq.
2. No users can be created. The users are identified by cookie data, which gets created once they submit their first feedback. That means the user can be identified only on the same browser on the same device, provided the user does not clear cookies.
3. There is only one Admin user.
4. The application requires javascript to be enabled on the user's browser.
5. The application is tested on the latest versions of IE, Chrome, and Edge on a computer.
6. It requires a connection with SQL server to run.
7. The system may behave unexpectedly when the user table is removed/modified and user cookie is not cleared.

<b>Features to be added:</b>
1. User registration functionality
2. Unit test projects.
3. Custom error pages