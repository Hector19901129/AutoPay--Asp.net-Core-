Follow these instruction to setup the application - 
1) Do publish the code everytime when want to deploy the application.
2) Change the connection string and baseUrl settings.
3) Change the Crytography settings if required.
4) Make sure InitVector is exactly 16 character long.
5) Don't change cryptography settings after deployment otherwise user won't be able to decrypt the information.
6) Take the backup/script of the updated database and restore the database in the server where you want to deploy the application.
7) Deploy the published code in the server.
8) We are done!