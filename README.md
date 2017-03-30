# Hockey Signups

## Setup
If you'd like to run this application, download the zip file and unzip it. In there, you'll find a file called `create_db`. This is a SQL file and when double clicked will open Management Studio. First, go to Management Studio, and create a database called HockeySignups. Then, double click that sql file and run it. This will create the database tables and stored procedures required by the application.

## Things to look out for
I didn't have enough time to really explain this in enough detail in class, but the trickiest part of this application was the page where we displa the history for the admin. If you run the application, you'll notice on the top menu, under Admin, there are two links for History. One that does it with a Group By and one without. Look at the code (specifically the one without the group by) make sure you understand how that works.