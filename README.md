# fforum - An online forum using ASP.NET and MongoDB

All related resources (SRS, Design, Project tracking and Final Release Doc) can be found [here](https://drive.google.com/drive/u/0/folders/1_GkPKd3F95WwYmvEnqQ9bHg2J3TFCDwO)

## Installation guide

Download and install .NET 7.0 into your machine. [Download here](https://dotnet.microsoft.com/en-us/download)

You can check the version of your .net by running

>  dotnet --version

Download [The Release](https://github.com/iamReinir/FForum/releases/download/Final/fforum.rar) and extract it to an empty folder. The binary should be in net7.0 folder.

Now, you can run the app by right-clicking the .exe (in Window) or by running this command

> dotnet forum.dll

## Features

### Register & Login

1. User can create a new account and login to that account.
2. User can their password (provided they give us their email) using the "forget password" function.
3. User can change their account infomation

### Post

0. Guest users can see posts and comments, without any ways to interact with them.
1. User can post an article, with text and optionally with picture
2. User can delete their article.
3. User can edit their acticle.

### Moderation

1. Moderator can hide anypost

### Admin

0. Admin can see list of members
1. Admin can change members' role
2. Admin can ban others members


