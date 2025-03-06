# EasyRent_C-_windown_form_project
Running the Project
Prerequisites
Visual Studio (Latest version recommended)
Microsoft SQL Server (Ensure it's installed and running)
Steps to Run the Project
Download the Project

Clone the repository or download the .sln file and open it in Visual Studio.
Set Up the Database

Download the database backup file (.bak) provided.
Open SQL Server Management Studio (SSMS).
Restore the database using the .bak file.
Configure the database settings properly.
Update Connection String

Inside the project, locate the database connection string (typically found in App.config or inside the database-related class).
Modify the connection string to match your SQL Server instance, database name, and authentication method.
Example:


string connectionString = "Data Source=YOUR_SERVER_NAME;Initial Catalog=YOUR_DATABASE_NAME;Integrated Security=True";


Run the Project

Build and run the project through Visual Studio.
Ensure that the application connects to the database correctly.
