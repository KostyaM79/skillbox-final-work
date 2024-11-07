IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = 'IIS APPPOOL\FinalJwtApiPool')
BEGIN
    CREATE LOGIN [IIS APPPOOL\FinalJwtApiPool] 
      FROM WINDOWS WITH DEFAULT_DATABASE=[master], 
      DEFAULT_LANGUAGE=[us_english]
END
GO
CREATE USER [ContosoUniversityUser] 
  FOR LOGIN [IIS APPPOOL\FinalJwtApiPool]
GO
EXEC sp_addrolemember 'db_owner', 'ContosoUniversityUser'
GO