#! /bin/bash

spwd="${SA_PASSWORD:-Strong_Password}"                     # Get Env Variable
export PATH=$PATH:"/opt/mssql/bin:/opt/mssql-tools/bin"    # Add Custom Paths

echo "===> SQL Server: Starting" &&

(sqlservr &) | grep -q "Service Broker manager has started" &&  # Check Server is UP

echo "===> SQL Server: Done" &&
echo "===> Check TodoDB: Starting" &&

sqlcmd -S 127.0.0.1 -U sa -P $spwd -Q "SELECT name from SYS.DATABASES" | grep -q "TodoDB" ||
(
   echo "===> Restore TodoDB: Starting" &&

   sqlcmd -S 127.0.0.1 -U sa -P $spwd -Q  \
   "RESTORE DATABASE TodoDB
    FROM DISK='/var/opt/mssql/data/TodoDB.bak' WITH REPLACE,
    MOVE 'TODO' TO '/var/opt/mssql/data/TodoDB.mdf',
    MOVE 'TODO_log' TO '/var/opt/mssql/data/TodoDB.ldf'"  &&

    echo "===> Restore TodoDB: Done"
)

echo "===> DONE"; sleep infinity;