#-------------------Download Image and Set Environment--------------#

FROM mcr.microsoft.com/mssql/server:2017-latest
ENV ACCEPT_EULA "Y"
ENV MSSQL_PID "Developer"
ENV SA_PASSWORD "Strong_Password"
EXPOSE 1433

#----Copy Restore Database and Bash Script File---------------------#

WORKDIR "/var/opt/mssql/data/"
COPY "./TodoDB.sh" .
COPY "./TodoDB.bak" .

#-------------------Set User Rights---------------------------------#

RUN chmod +x "TodoDB.sh"

#-------------------Run Script File---------------------------------#

ENTRYPOINT [ "/var/opt/mssql/data/TodoDB.sh" ]

#-------------------End---------------------------------------------#

