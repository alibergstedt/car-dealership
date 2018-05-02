USE master
GO
 
CREATE LOGIN DvdLibraryApp WITH PASSWORD='testing123'
GO

USE DvdLibrary
GO
 
CREATE USER DvdLibraryApp FOR LOGIN DvdLibraryApp
GO


CREATE ROLE db_executor
 

GRANT EXECUTE TO db_executor
 

ALTER ROLE db_executor ADD MEMBER DvdLibraryApp
ALTER ROLE db_datawriter ADD MEMBER DvdLibraryApp
ALTER ROLE db_datareader ADD MEMBER DvdLibraryApp

GO
