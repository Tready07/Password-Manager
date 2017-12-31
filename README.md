# Password-Manager

## About
The Password-Manager is intended to be a client server architecture. One (or more) admin has the ability to set up user accounts. The user can then use the program to store, view, set, and auto-generate create passwords for different applications. I originally had this idea when I was sick of reusing the same three passwords (mixing capital letters) as well as remembering my mother's passwords and my grandmothers passwords. I liked the idea of having a program for them to easily get this information with as well as learning the language of C#. I feel as if this program will adequately do that job.

## Architecture
As stated above there is a client and server architecture, and the main reason I did this is because I wanted to practice networking in C#. However, that shouldn't stop anyone from setting up the server connection as localhost and hosting the server individually on each computer.

### Client
The Password-Manager client is a graphical user interface intended to be used by each user. The main features of the client are:
- Viewing all applications the client has information for.
- Viewing all usernames for each application.
- Viewing the password for each username.
- Editing the password for each username.

### Server
The Password-Manager server is a console application that is responsible for handling the requests from the client information and delivering back the desired information. The server has a few main responsibilities. They are:
- Encrypting and Decrypting passwords.
- Storing client information about application name, username, password into a sqlite database.
- Delivering the desired client info on request to the proper clientclient.

### Database
The database is managed by the Password-Manager server. It currently consists of two tables *Users* and *Applications*.
