# PROTO Web API
C# WebAPI for NET6 using Clean Architecure with Repository Pattern and UnitOfWork

Reference: https://code-maze.com/using-dapper-with-asp-net-core-web-api/

## SQL Database Tasks

### Create Table
```sql
CREATE TABLE [dbo].[BasicAuth](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[PassWord] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
```

### Create Table
```sql
CREATE TABLE [dbo].[Host_Devices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Hostname] [nvarchar](120) NOT NULL,
	[Domain] [nvarchar](50) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[SerialNumber] [nvarchar](50) NULL
) ON [PRIMARY]
GO
```  

### Create Stored Procedures

```sql

CREATE PROCEDURE [dbo].[DeleteHostDevicesById] @Id int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM PROTO.dbo.Host_Devices  where Id=@Id;
END
GO

CREATE PROCEDURE [dbo].[GetAllHostDevices] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, Hostname, Domain,CreatedOn, SerialNumber  FROM PROTO.dbo.Host_Devices ;
END
GO

CREATE PROCEDURE [dbo].[GetHostDevicesByDate] @FindDate Date

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, Hostname, Domain,CreatedOn, SerialNumber  FROM PROTO.dbo.Host_Devices where CAST(CreatedOn AS Date)  = CAST(@FindDate AS Date);
END
GO

CREATE PROCEDURE [dbo].[GetHostDevicesById] @Id int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, Hostname, Domain,CreatedOn, SerialNumber  FROM PROTO.dbo.Host_Devices where Id=@Id;
END
GO

```