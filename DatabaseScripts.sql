--USE [CustomerDatabase]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
CREATE PROCEDURE [dbo].[ContactByType] 
    @ContactTitleTypeIdentifier INT
AS 
BEGIN 
    SET NOCOUNT ON; 
    SELECT  
        Identifier,  
        CompanyName,  
        ContactName  
    FROM  
        Customer  
    WHERE  
        Customer.ContactTypeIdentifier = @ContactTitleTypeIdentifier
END 
 


GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[CustomerInsertOrUpdate] 
    @Identifier int = NULL OUTPUT, 
    @CompanyName nvarchar(255), 
    @ContactName nvarchar(255), 
    @ContactTitle nvarchar(255) 
AS 
BEGIN 
    SET NOCOUNT ON; 
    IF @Identifier IS NULL 
    BEGIN 
        INSERT INTO Customer  
            (CompanyName,ContactName,ContactTitle) 
        VALUES  
            (@CompanyName, @ContactName, @ContactTitle) 
        SET @Identifier = SCOPE_IDENTITY() 
    END 
    ELSE 
    BEGIN 
        UPDATE Customer 
        SET    CompanyName = @CompanyName, 
            ContactName = @ContactName, 
            ContactTitle = @ContactTitle 
    END     
END 
 

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================= 
-- Author:        <Author,,Name> 
-- Create date: <Create Date,,> 
-- Description:    <Description,,> 
-- ============================================= 
CREATE PROCEDURE [dbo].[DeleteCustomer] 
    @flag bit output,-- return 0 for fail,1 for success 
    @Identity int 
AS 
BEGIN 
    -- SET NOCOUNT ON added to prevent extra result sets from 
    -- interfering with SELECT statements. 
    SET NOCOUNT ON; 
    BEGIN TRANSACTION  
    BEGIN TRY 
        DELETE FROM Customer WHERE Identifier = @Identity set @flag=1;  
        IF @@TRANCOUNT > 0 
            BEGIN commit TRANSACTION; 
        END 
    END TRY 
    BEGIN CATCH 
        IF @@TRANCOUNT > 0 
            BEGIN rollback TRANSACTION;  
        END 
        set @flag=0;  
    END CATCH  
 END  
 
 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertCustomer]  
    @CompanyName NVARCHAR(200), 
    @ContactName NVARCHAR(200), 
    @ContactTypeIdentifier INT, 
    @Identity INT OUT 
AS 
BEGIN 
    -- SET NOCOUNT ON added to prevent extra result sets from 
    -- interfering with SELECT statements. 
    SET NOCOUNT ON; 
 
INSERT INTO Customer(CompanyName,ContactName,ContactTypeIdentifier)  
    VALUES(@CompanyName,@ContactName,@ContactTypeIdentifier) 
 
SET @Identity = SCOPE_IDENTITY() 
 
END 
 


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
CREATE PROCEDURE [dbo].[SelectAllCustomers] 
AS 
BEGIN 
    SET NOCOUNT ON; 
SELECT Cust.Identifier ,
       Cust.CompanyName ,
       Cust.ContactName ,
       Cust.ContactTypeIdentifier ,
       CT.ContactType AS ContactTitle
FROM   Customer AS Cust
       INNER JOIN ContactTypes AS CT ON Cust.ContactTypeIdentifier = CT.Identifier;
END 


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ============================================= 
-- Author:        <Author,,Name> 
-- Create date: <Create Date,,> 
-- Description:    <Description,,> 
-- ============================================= 
CREATE PROCEDURE [dbo].[SelectContactTitles] 
AS 
BEGIN 
    SET NOCOUNT ON; 

SELECT Identifier
      ,ContactType
  FROM dbo.ContactTypes
END 
 


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ============================================= 
-- Author:        <Author,,Name> 
-- Create date: <Create Date,,> 
-- Description:    <Description,,> 
-- ============================================= 
CREATE PROCEDURE [dbo].[UpateCustomer] 
    @flag BIT OUTPUT,-- return 0 for fail,1 for success 
    @CompanyName NVARCHAR(200), 
    @ContactName NVARCHAR(200), 
    @ContactTypeIdentifier INT, 
    @Identity INT 
AS 
BEGIN 
    -- SET NOCOUNT ON added to prevent extra result sets from 
    -- interfering with SELECT statements. 
    SET NOCOUNT ON; 
    BEGIN TRANSACTION  
    BEGIN TRY 
        UPDATE dbo.Customer SET  
            CompanyName = @CompanyName,  
            ContactName = @ContactName,  
            ContactTypeIdentifier = @ContactTypeIdentifier 
        WHERE Identifier = @Identity 
        SET @flag=1;  
        IF @@TRANCOUNT > 0 
            BEGIN COMMIT TRANSACTION; 
        END 
    END TRY 
    BEGIN CATCH 
        IF @@TRANCOUNT > 0 
            BEGIN ROLLBACK TRANSACTION;  
        END 
        SET @flag=0; 
    END CATCH 
 END  
 
 


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ThrowDummyException] 
  @ErrorMessage   varchar(2000) OUTPUT
 ,@ErrorSeverity  INT OUTPUT
 ,@ErrorState     INT OUTPUT
AS
BEGIN


BEGIN TRY

	RAISERROR('your message here',16,1)

END TRY

BEGIN CATCH
    SET @ErrorMessage  = ERROR_MESSAGE()
    SET @ErrorSeverity = ERROR_SEVERITY()
    SET @ErrorState    = ERROR_STATE()
    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
END CATCH
END


GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================= 
-- Author:        <Author,,Name> 
-- Create date: <Create Date,,> 
-- Description:    <Description,,> 
-- ============================================= 
CREATE PROCEDURE [dbo].[uspInsertPerson]   
    @FirstName nvarchar(50), 
    @LastName nvarchar(200), 
    @Identity int OUT 
AS 
BEGIN 
    -- SET NOCOUNT ON added to prevent extra result sets from 
    -- interfering with SELECT statements. 
    SET NOCOUNT ON; 
 
INSERT INTO [dbo].[Table_1]([FirstName],[LastName]) 
    VALUES(@FirstName,@LastName) 
 
SET @Identity = SCOPE_IDENTITY() 
END 
 

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[uspUpatePerson] 
    @flag bit output,-- return 0 for fail,1 for success 
    @FirstName nvarchar(200), 
    @LastName nvarchar(200), 
    @Identity int 
AS 
BEGIN 
    -- SET NOCOUNT ON added to prevent extra result sets from 
    -- interfering with SELECT statements. 
    SET NOCOUNT ON; 
    BEGIN TRANSACTION  
    BEGIN TRY 
        UPDATE [dbo].[Table_1] SET  
            FirstName = @FirstName,  
            LastName = @LastName 
        WHERE Identifier = @Identity 
        set @flag=1;  
        IF @@TRANCOUNT > 0 
            BEGIN commit TRANSACTION; 
        END 
    END TRY 
    BEGIN CATCH 
        IF @@TRANCOUNT > 0 
            BEGIN rollback TRANSACTION;  
        END 
        set @flag=0; 
    END CATCH 
END 
 

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactTypes](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[ContactType] [nvarchar](50) NULL,
 CONSTRAINT [PK_ContactTypes] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](255) NULL,
	[ContactName] [nvarchar](255) NULL,
	[ContactTypeIdentifier] [int] NULL,
	[GenderIdentifier] [int] NULL,
 CONSTRAINT [Customer$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[GenderType] [nvarchar](max) NULL,
 CONSTRAINT [PK_Genders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[ContactTypes] ON 

GO
INSERT [dbo].[ContactTypes] ([Identifier], [ContactType]) VALUES (1, N'Account Manager')
GO
INSERT [dbo].[ContactTypes] ([Identifier], [ContactType]) VALUES (2, N'Assistant Sales Agent')
GO
INSERT [dbo].[ContactTypes] ([Identifier], [ContactType]) VALUES (3, N'Assistant Sales representative')
GO
INSERT [dbo].[ContactTypes] ([Identifier], [ContactType]) VALUES (4, N'Marketing assistant')
GO
INSERT [dbo].[ContactTypes] ([Identifier], [ContactType]) VALUES (5, N'Marketing Manager')
GO
INSERT [dbo].[ContactTypes] ([Identifier], [ContactType]) VALUES (6, N'Order Administrator')
GO
INSERT [dbo].[ContactTypes] ([Identifier], [ContactType]) VALUES (7, N'Owner')
GO
INSERT [dbo].[ContactTypes] ([Identifier], [ContactType]) VALUES (8, N'Owner/Marketing Assistant')
GO
INSERT [dbo].[ContactTypes] ([Identifier], [ContactType]) VALUES (9, N'Sales Agent')
GO
INSERT [dbo].[ContactTypes] ([Identifier], [ContactType]) VALUES (10, N'Sales Associate')
GO
INSERT [dbo].[ContactTypes] ([Identifier], [ContactType]) VALUES (11, N'Sales Manager')
GO
INSERT [dbo].[ContactTypes] ([Identifier], [ContactType]) VALUES (12, N'Sales Representative')
GO
SET IDENTITY_INSERT [dbo].[ContactTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

GO
INSERT [dbo].[Customer] ([Identifier], [CompanyName], [ContactName], [ContactTypeIdentifier], [GenderIdentifier]) VALUES (1, N'Fly Girls', N'Tim Clark', 7, 2)
GO
INSERT [dbo].[Customer] ([Identifier], [CompanyName], [ContactName], [ContactTypeIdentifier], [GenderIdentifier]) VALUES (2, N'Coffee Paradise', N'Ann Adams', 12, 1)
GO
INSERT [dbo].[Customer] ([Identifier], [CompanyName], [ContactName], [ContactTypeIdentifier], [GenderIdentifier]) VALUES (3, N'Garrys Coffee', N'Mary Clime', 7, 2)
GO
INSERT [dbo].[Customer] ([Identifier], [CompanyName], [ContactName], [ContactTypeIdentifier], [GenderIdentifier]) VALUES (4, N'Salem Boat Rentals', N'Bill Willis', 9, 2)
GO
INSERT [dbo].[Customer] ([Identifier], [CompanyName], [ContactName], [ContactTypeIdentifier], [GenderIdentifier]) VALUES (5, N'Knifes are us', N'Kathy Willians', 7, 1)
GO
INSERT [dbo].[Customer] ([Identifier], [CompanyName], [ContactName], [ContactTypeIdentifier], [GenderIdentifier]) VALUES (6, N'Karen''s fish', N'Karen Payne', 7, 1)
GO
INSERT [dbo].[Customer] ([Identifier], [CompanyName], [ContactName], [ContactTypeIdentifier], [GenderIdentifier]) VALUES (7, N'Best tax preparers', N'Hank Jones', 7, 3)
GO
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Genders] ON 

GO
INSERT [dbo].[Genders] ([id], [GenderType]) VALUES (1, N'Female')
GO
INSERT [dbo].[Genders] ([id], [GenderType]) VALUES (2, N'Male')
GO
INSERT [dbo].[Genders] ([id], [GenderType]) VALUES (3, N'Other')
GO
SET IDENTITY_INSERT [dbo].[Genders] OFF
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_ContactTypes] FOREIGN KEY([ContactTypeIdentifier])
REFERENCES [dbo].[ContactTypes] ([Identifier])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_ContactTypes]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Genders] FOREIGN KEY([GenderIdentifier])
REFERENCES [dbo].[Genders] ([id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Genders]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'CustomersDatabase.[Customer].[Identifier]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'Identifier'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'CustomersDatabase.[Customer].[CompanyName]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'CompanyName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'CustomersDatabase.[Customer].[ContactName]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'ContactName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'CustomersDatabase.[Customer]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'CustomersDatabase.[Customer].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'CONSTRAINT',@level2name=N'Customer$PrimaryKey'
GO