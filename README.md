# Architectures2018
WPF MVVM project ( user sign in/ sign up and .txt-file statistics)
This project's goal is to follow MVVM pattern. 
There are Models (User, Query), Views (three User controls : LogIn, SignUp and Main view for actual use), ModelViews ( logic behind these views). Query remembers its date and file, it also can analyze given string (as a static method) and count its words, lines, chars. Firstly, User creates an account (password is stored encrypted). After logging in, User can either check his history of Queries or create a new one. For latter, FileChooseDialog appears where User selects a file to show statistics about. On error, no query is created and saved. User also can log out and leave the application.
*As for Homework #1 no multi-threading and DB is applied.* (Testing results https://docs.google.com/document/d/1JCF66_mkB41Sp7bp6tosH-ddSg0fzIezzuJMrRw-EOA/edit?usp=sharing) 
