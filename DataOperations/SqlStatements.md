# Get Stored Procedures

Several SQL statements which can be executed in Visual Studio or SSMS (SQL-Server Management Studio)

### Get all Stored Procedures for a database

```sql
SELECT   name
FROM     sys.procedures
WHERE    name NOT LIKE 'sp_%'
ORDER BY name;
```
### Get definition of a  stored procedure by name representing the stored procedure name

```sql
DECLARE @StoredProcedureName AS NVARCHAR(50) = 'dbo.CustomerInsertOrUpdate';
SELECT definition
FROM   sys.sql_modules
WHERE  object_id = OBJECT_ID(@StoredProcedureName);
```

### Get definition and parameters

```sql
DECLARE @StoredProcedureName AS NVARCHAR(50) = 'dbo.CustomerInsertOrUpdate';
SELECT name,
       system_type_id,
       max_length,
       [precision],
       scale
FROM sys.parameters
WHERE object_id = OBJECT_ID(@StoredProcedureName);
```

### Using a Cursor to get Stored Procedure details and parameters

```sql
DECLARE @ProcName NVARCHAR(50);
DECLARE @IteratorProcedureName NVARCHAR(50);
DECLARE IteratorName CURSOR FAST_FORWARD READ_ONLY FOR
    SELECT   name
    FROM     sys.procedures
    WHERE    name NOT LIKE 'sp_%'
    ORDER BY name;
OPEN IteratorName;

FETCH NEXT FROM IteratorName
INTO @IteratorProcedureName;
WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT definition
        FROM   sys.sql_modules
        WHERE  object_id = OBJECT_ID(@IteratorProcedureName);
        FETCH NEXT FROM IteratorName
        INTO @IteratorProcedureName;

    END;

CLOSE IteratorName;
DEALLOCATE IteratorName;
```