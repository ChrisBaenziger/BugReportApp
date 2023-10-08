ECHO off

sqlcmd -S localhost -E -i BugReportAppDB.sql

rem server is localhost

ECHO .
ECHO if no errors appear DB was created
PAUSE