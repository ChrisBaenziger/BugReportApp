/* 
    File: BugReportAppDB.sql
    Date: 2023/10/04
    Author: Chris Baenziger
    Description: Database for Bug Report App for .NET II class.
 */

 /* 
    Header Line List
    Create_Tables
    Create_Indexes
    Create_Views
    Create_Triggers
    Insert_data_into_the_tables
    Functions
    Stored_Procedures
*/
print '' print '***  ***'
GO


  /* Check to see if the database exsists, if so, drop it */

IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases
    WHERE name = 'BugReportAppDB')
BEGIN 
    DROP DATABASE BugReportAppDB
    print '' print '*** dropping database BugReportAppDB ***'
END
GO

print '' print '*** creating database BugReportAppDB ***'
GO
CREATE DATABASE BugReportAppDB
GO

print '' print '*** using database BugReportAppDB ***'
GO
USE BugReportAppDB
GO

/*
Table Create Order
    BugStatus
    ProductVersion
    ProductArea
    Feature
    Employee
    Role
    Customer
    ComputerInformation
    BugTicket
    BugTicketArchive
*/

/*
print '' print '*** creating  table ***'
GO
CREATE TABLE [dbo].[] (


    CONSTRAINT [pk_] PRIMARY KEY ([]),
    CONSTRAINT [fk_] FOREIGN KEY ([])
        REFERENCES [dbo].[] ([]),
    CONSTRAINT [] UNIQUE ([])
);
GO


print '' print '*** insterting  ***'
GO
INSERT INTO [dbo].[]
        ([])
    VALUES
        (),
        ();
GO
*/

print '' print '*** creating bug status table ***'
GO
CREATE TABLE [dbo].[BugStatus] (
    [Status]    [nvarchar](50)      NOT NULL,
    [Active]    [bit]               NOT NULL DEFAULT 1

    CONSTRAINT [pk_BugStatus] PRIMARY KEY ([Status]),
    CONSTRAINT [ak_BugStatus] UNIQUE ([Status])
);
GO

print '' print '*** instert into bug status table ***'
GO
INSERT INTO [dbo].[BugStatus]
        ([Status])
    VALUES
        ('New'),
        ('Locating'),
        ('In-Progress'),
        ('Testing'),
        ('Resolved');
GO

print '' print '*** creating product version ***'
GO
CREATE TABLE [dbo].[ProductVersion] (
    [VersionNumber]     [nvarchar](16)  NOT NULL,
    [VersionStartDate]  [date]          NOT NULL,
    [VersionEndDate]    [date]          NULL,
    [Active]    [bit]               NOT NULL DEFAULT 1

    CONSTRAINT [pk_ProductVersion] PRIMARY KEY ([VersionNumber]),
    CONSTRAINT [ak_VersionNumber] UNIQUE ([VersionNumber])
);
GO

print '' print '*** instert into product version ***'
GO
INSERT INTO [dbo].[ProductVersion]
        ([VersionNumber], [VersionStartDate], [VersionEndDate])
    VALUES
        ('1.02.001', '2023-10-01', null),
        ('1.01.001', '2023-08-21', '2023-10-01');
GO

print '' print '*** creating product area table ***'
GO
CREATE TABLE [dbo].[ProductArea] (
    [AreaName]      [nvarchar](50)      NOT NULL,
    [FirstVersionNumber]  [nvarchar](16)      NOT NULL,
    [Active]    [bit]               NOT NULL DEFAULT 1


    CONSTRAINT [pk_AreaName] PRIMARY KEY ([AreaName]),
    CONSTRAINT [fk_ProductArea_FirstVersionNumber] FOREIGN KEY ([FirstVersionNumber])
        REFERENCES [dbo].[ProductVersion]([VersionNumber]),
    CONSTRAINT [ak_AreaName] UNIQUE ([AreaName])
);
GO

print '' print '*** instert into product area ***'
GO
INSERT INTO [dbo].[ProductArea]
        ([AreaName], [FirstVersionNumber])
    VALUES
        ('Core', '1.01.001'),
        ('Save', '1.01.001'),
        ('Load', '1.01.001'),
        ('Network', '1.01.001'),
        ('Downloading', '1.01.001'),
        ('Installation', '1.01.001'),
        ('Uninstall', '1.01.001'),
        ('Other', '1.01.001');
GO

print '' print '*** creating feature list table ***'
GO
CREATE TABLE [dbo].[Feature] (
    [FeatureName]           [nvarchar](100)       NOT NULL,
    [FirstVersionNumber]    [nvarchar](16)        NOT NULL,
    [FeatureArea]           [nvarchar](50)        NOT NULL,
    [FeatureDescription]    [nvarchar](max)                NOT NULL,
    [LastVersionNumber]     [nvarchar](50)        NULL,
    [Active]    [bit]               NOT NULL DEFAULT 1

    CONSTRAINT [pk_FeatureName] PRIMARY KEY ([FeatureName]),
    CONSTRAINT [fk_FeatureList_FirstVersionNumber] FOREIGN KEY ([FirstVersionNumber])
        REFERENCES [dbo].[ProductVersion] ([VersionNumber]),
    CONSTRAINT [fk_FeatureArea] FOREIGN KEY ([FeatureArea])
        REFERENCES [dbo].[ProductArea] ([AreaName])
);
GO

/* Test insert code worked 2023-10-05
print '' print '*** instert into feature list table ***'
GO
INSERT INTO [dbo].[Feature]
        ([FeatureName], [FirstVersionNumber], [FeatureArea], [FeatureDescription], [LastVersionNumber])
    VALUES
        ('Test', '1.01.001', 'Core', 'Testing', null);
GO
*/

print '' print '*** creating employee table ***'
GO
CREATE TABLE [dbo].[Employee] (
	[EmployeeID]	[int]	IDENTITY(100000,1)	NOT NULL,
	[GivenName]		[nvarchar](50)				NOT NULL,
	[FamilyName]	[nvarchar](100)				NOT NULL,
    [Address1]      [nvarchar](50)              NOT NULL,
    [Address2]      [nvarchar](50)              NULL,
    [City]          [nvarchar](100)             NOT NULL,
    [State]         [nvarchar](2)               NOT NULL,
    [Zip]           [nvarchar](10)              NOT NULL,
	[PhoneNumber]	[nvarchar](11)				NOT NULL,
	[Email]			[nvarchar](150)				NOT NULL,
	[PasswordHash]	[nvarchar](100)				NOT NULL DEFAULT
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Active]		[bit]						NOT NULL DEFAULT 1,
	CONSTRAINT [pk_EmployeeID] PRIMARY KEY([EmployeeID]),
	CONSTRAINT [ak_Employee_Email] UNIQUE([Email])
)
GO

print '' print '*** instert into employee table ***'
GO
INSERT INTO [dbo].[Employee]
        ([GivenName], [FamilyName], [Address1], [Address2], [City], [State], [Zip], [PhoneNumber], [Email])
    VALUES
        ('Chris', 'Baenziger', '229 Nielsen Hall', null, 'Cedar Rapids', 'IA', '52405', '3193101111', 'christopher-baenziger@student.kirkwood.edu'),
        ('Jim', 'Glasgow', '229 Nielsen Hall', null, 'Cedar Rapids', 'IA', '52405', '3103102222','Jim.Glasgow@kirkwood.edu');
GO

print '' print '*** creating role table ***'
GO
CREATE TABLE [dbo].[Role] (
	[RoleID]	[nvarchar](50)				NOT NULL,
    [Active]    [bit]                       NOT NULL DEFAULT 1

	CONSTRAINT [pk_RoleID] PRIMARY KEY([RoleID])
)
GO

print '' print '*** inserting role test records ***'
GO

INSERT INTO [dbo].[Role]
		([RoleID])
	VALUES
		('Manager'),
		('Programmer'),
		('SeniorProgrammer'),
		('ProjectLead'),
		('Admin')
GO

print '' print '*** creating employee role table ***'
GO
CREATE TABLE [dbo].[EmployeeRole] (
	[EmployeeID]	[int]						NOT NULL,
	[RoleID]		[nvarchar](50)				NOT NULL,
    [Active]    [bit]                       NOT NULL DEFAULT 1
	
	CONSTRAINT [pk_EmployeeRole_EmployeeID] FOREIGN KEY([EmployeeID])
		REFERENCES [dbo].[Employee]([EmployeeID]),
	
	CONSTRAINT [pk_EmployeeRole_Role] FOREIGN KEY([RoleID])
		REFERENCES [dbo].[Role]([RoleID]),
		
	CONSTRAINT [pk_EmployeeRole] PRIMARY KEY([EmployeeID], [RoleID])
)
GO

print '' print '*** instert into employee role table ***'
GO
INSERT INTO [dbo].[EmployeeRole]
        ([EmployeeID], [RoleID])
    VALUES
        (100000, 'Programmer'),
        (100001, 'ProjectLead');
GO



print '' print '*** creating customer table ***'
GO
CREATE TABLE [dbo].[Customer] (
	[CustomerID]	[int]	IDENTITY(100000,1)	NOT NULL,
	[GivenName]		[nvarchar](50)				NOT NULL,
	[FamilyName]	[nvarchar](100)				NOT NULL,
    [Address1]      [nvarchar](50)              NULL,
    [Address2]      [nvarchar](50)              NULL,
    [City]          [nvarchar](100)             NULL,
    [State]         [nvarchar](2)               NULL,
    [Zip]           [nvarchar](10)              NULL,
	[PhoneNumber]	[nvarchar](11)				NULL,
	[Email]			[nvarchar](150)				NOT NULL,
	[PasswordHash]	[nvarchar](100)				NOT NULL DEFAULT
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[VersionNumber] [nvarchar](16)              NULL,
    [Active]		[bit]						NOT NULL DEFAULT 1
    
	CONSTRAINT [pk_CustomerID] PRIMARY KEY([CustomerID]),
	CONSTRAINT [ak_Customer_Email] UNIQUE([Email])
)
GO

print '' print '*** instert customer into ***'
GO
INSERT INTO [dbo].[Customer]
        ([GivenName], [FamilyName], [Email], [VersionNumber])
    VALUES
        ('James', 'Williams', 'jwiliams@test.com', '1.01.001'),
        ('Jacob', 'Wendt', 'jwendt@test.com', null)
        ;
GO

print '' print '*** creating computer information table ***'
GO
CREATE TABLE [dbo].[ComputerInformation] (
    [CustomerID]        [int]           NOT NULL,
    [Manufacturer]      [nvarchar](100) NULL,
    [ModelNumber]       [nvarchar](50)  NULL,
    [OperatingSystem]   [nvarchar](50)  NULL,
    [CPU]               [nvarchar](100) NULL,
    [GraphicsCard]      [nvarchar](100) NULL,
    [RamType]           [nvarchar](100) NULL,
    [RamGB]             [decimal]       NULL,
    [IPAddress]         [nvarchar](16)  NULL   

    CONSTRAINT [pk_ComputerInformation] PRIMARY KEY ([CustomerID]),
);
GO

print '' print '*** instert into computer information table ***'
GO
INSERT INTO [dbo].[ComputerInformation]
        ([CustomerID], [Manufacturer], [ModelNumber], [OperatingSystem], [CPU], [GraphicsCard], [RamType], [RamGB], [IPAddress])
    VALUES
        (100001, null, null, 'Windows', null, 'Nvidia 3080', 'DDR5', 32, '127.0.0.1'),
        (100002, null, null, 'Windows', null, 'Nvidia 3070', 'DDR5', 16, '127.0.0.2');
GO

print '' print '*** creating bug ticket table ***'
GO
CREATE TABLE [dbo].[BugTicket] (
    [BugTicketID]       [int]   IDENTITY(100000, 1) NOT NULL,
    [BugDate]           [date]                      NOT NULL DEFAULT GETDATE(),
    [SubmitID]          [int]                       NOT NULL,
    [VersionNumber]     [nvarchar](16)              NOT NULL,
    [AreaName]          [nvarchar](50)              NOT NULL,
    [Description]       [nvarchar](max)             NOT NULL,
    [Status]            [nvarchar](50)              NOT NULL DEFAULT 'New',
    [Feature]           [nvarchar](100)             NULL,
    [AssignedTo]        [int]                       NULL,
    [LastWorkedDate]    [date]                      NULL,
    [LastWorkedEmployee][int]                       NULL,
    [Active]            [bit]                       NOT NULL DEFAULT 1

    CONSTRAINT [pk_BugTicket] PRIMARY KEY ([BugTicketID]),
    CONSTRAINT [fk_TicketCustomerID] FOREIGN KEY ([SubmitID])
        REFERENCES [dbo].[Customer] ([CustomerID]),
    CONSTRAINT [fk_TicketEmployeeID] FOREIGN KEY ([AssignedTo])
        REFERENCES [dbo].[Employee] ([EmployeeID]),
    CONSTRAINT [fk_TicketVersion] FOREIGN KEY ([VersionNumber])
        REFERENCES [dbo].[ProductVersion] ([VersionNumber]),
    CONSTRAINT [fk_TicketArea] FOREIGN KEY ([AreaName])
        REFERENCES [dbo].[ProductArea] ([AreaName]),
    CONSTRAINT [fk_TicketStatus] FOREIGN KEY ([Status])
        REFERENCES [dbo].[BugStatus] ([Status]),
    CONSTRAINT [fk_TicketFeature] FOREIGN KEY ([Feature])
        REFERENCES [dbo].[Feature] ([FeatureName])
);
GO

print '' print '*** instert into bug ticket table ***'
GO
INSERT INTO [dbo].[BugTicket]
        ([SubmitID], [VersionNumber], [AreaName], [Description])
    VALUES
        (100000, '1.01.001', 'Core', 'Test description'),
        (100001, '1.02.001', 'Other', 'Test two descriptoin');
GO

print '' print '*** creating bug ticket archive table ***'
GO
CREATE TABLE [dbo].[BugTicketArchive] (
    [BugTicketID]       [int]                       NOT NULL,
    [BugDate]           [date]                      NOT NULL DEFAULT GETDATE(),
    [SubmitID]          [int]                       NOT NULL,
    [VersionNumber]     [nvarchar](16)              NOT NULL,
    [AreaName]          [nvarchar](50)              NOT NULL,
    [Description]       [nvarchar](max)             NOT NULL,
    [Status]            [nvarchar](50)              NOT NULL DEFAULT 'New',
    [Feature]           [nvarchar](100)             NULL,
    [AssignedTo]        [int]                       NULL,
    [LastWorkedDate]    [date]                      NULL,
    [LastWorkedEmployee][int]                       NULL

    CONSTRAINT [pk_BugTicketArchive] PRIMARY KEY ([BugTicketID]),
    CONSTRAINT [fk_ArchiveTicketCustomerID] FOREIGN KEY ([SubmitID])
        REFERENCES [dbo].[Customer] ([CustomerID]),
    CONSTRAINT [fk_ArchiveTicketEmployeeID] FOREIGN KEY ([SubmitID])
        REFERENCES [dbo].[Employee] ([EmployeeID]),
    CONSTRAINT [fk_ArchiveTicketVersion] FOREIGN KEY ([VersionNumber])
        REFERENCES [dbo].[ProductVersion] ([VersionNumber]),
    CONSTRAINT [fk_ArchiveTicketArea] FOREIGN KEY ([AreaName])
        REFERENCES [dbo].[ProductArea] ([AreaName]),
    CONSTRAINT [fk_ArchiveTicketStatus] FOREIGN KEY ([Status])
        REFERENCES [dbo].[BugStatus] ([Status]),
    CONSTRAINT [fk_ArchiveTicketFeature] FOREIGN KEY ([Feature])
        REFERENCES [dbo].[Feature] ([FeatureName])
);
GO

/* Test insert code worked 2023-10-05 
print '' print '*** instert into bug ticket archive table ***'
GO
INSERT INTO [dbo].[BugTicketArchive]
        ([BugTicketID], [SubmitID], [VersionNumber], [AreaName], [Description])
    VALUES
        (100000, 100000, '1.01.001', 'Core', 'Test description'),
        (100001, 100001, '1.02.001', 'Other', 'Test two descriptoin');
GO
*/


/* login related stored procedures */

print '' print '*** creating sp_authenticate_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_employee]
(
	@Email			[nvarchar](100),
	@PasswordHash	[nvarchar](100)
)
AS
	BEGIN
		SELECT 	COUNT([EmployeeID]) as 'Authenticated'
		FROM	[Employee]
		WHERE	@Email = [Email]
			AND @PasswordHash = [PasswordHash]
			AND [Active] = 1
	END
GO

print '' print '*** creating sp_authenticate_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_customer]
(
	@Email			[nvarchar](100),
	@PasswordHash	[nvarchar](100)
)
AS
	BEGIN
		SELECT 	COUNT([CustomerID]) as 'Authenticated'
		FROM	[Customer]
		WHERE	@Email = [Email]
			AND @PasswordHash = [PasswordHash]
			AND [Active] = 1
	END
GO

print '' print '*** creating sp_select_employee_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_by_email]
(
	@Email				[nvarchar](100)
)
AS
	BEGIN
		SELECT	[EmployeeID], [GivenName], [FamilyName], [PhoneNumber],
					[Email], [Active]
		FROM	[Employee]
		WHERE	@Email = [Email]
	END
GO

print '' print '*** creating sp_select_customer_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_customer_by_email]
(
	@Email				[nvarchar](100)
)
AS
	BEGIN
		SELECT	[EmployeeID], [GivenName], [FamilyName], [PhoneNumber],
					[Email], [Active]
		FROM	[Customer]
		WHERE	@Email = [Email]
	END
GO

print '' print '*** creating sp_select_roles_by_employeeID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_roles_by_employeeID]
(
	@EmployeeID			[int]
)
AS
	BEGIN
		SELECT	[RoleID]
		FROM	[EmployeeRole]
		WHERE	@EmployeeID = [EmployeeID]
	END
GO

print '' print '*** creating sp_update_employee_passwordHash ***'
GO
CREATE PROCEDURE [dbo].[sp_update_employee_passwordHash]
(
	@Email				[nvarchar](100),
	@NewPasswordHash	[nvarchar](100),
	@OldPasswordHash	[nvarchar](100)
)
AS
	BEGIN
		UPDATE		[Employee]
		SET			[PasswordHash] = @NewPasswordHash
		WHERE		@Email = [Email]
			AND		@OldPasswordHash = [PasswordHash]
		RETURN 		@@ROWCOUNT
	END
GO

print '' print '*** creating sp_update_customer_passwordHash ***'
GO
CREATE PROCEDURE [dbo].[sp_update_customer_passwordHash]
(
	@Email				[nvarchar](100),
	@NewPasswordHash	[nvarchar](100),
	@OldPasswordHash	[nvarchar](100)
)
AS
	BEGIN
		UPDATE		[Customer]
		SET			[PasswordHash] = @NewPasswordHash
		WHERE		@Email = [Email]
			AND		@OldPasswordHash = [PasswordHash]
		RETURN 		@@ROWCOUNT
	END
GO


/* Bug Ticket Stored Procedures */

print '' print '*** creating sp_create_bug_ticket ***'
GO
CREATE PROCEDURE [dbo].[sp_create_bug_ticket]
(
    @BugDate        [date],
    @SubmitID       [int],
    @VersionNumber  [nvarchar](16),
    @AreaName       [nvarchar](50),
    @Description    [nvarchar](max),
    @Status         [nvarchar](50),
    @Feature        [nvarchar](100),
    @AssignedTo     [int]
)
AS
    BEGIN
    INSERT INTO [dbo].[BugTicket]
            ([BugDate], [SubmitID], [VersionNumber], [AreaName], [Description], [Status], [Feature], [AssignedTo])
        VALUES
            (@BugDate, @SubmitID, @VersionNumber, @AreaName, @Description, @Status, @Feature, @AssignedTo)
    END
GO

/* Test worked 2023-10-05
print '' print '*** testing sp_create_bug_ticket ***'
GO
EXECUTE [dbo].[sp_create_bug_ticket]
    @BugDate = '2023-10-03',
    @SubmitID = 100001,
    @VersionNumber = '1.02.001',
    @AreaName = 'Save',
    @Description = 'Test create procedure',
    @Status = 'New',
    @AssignedTo = '100000';
GO
*/

print '' print '*** creating sp_update_bug_ticket ***'
GO
CREATE PROCEDURE [dbo].[sp_update_bug_ticket]
(
    /* new data */
    @BugTicketID        [int],
    @BugDate            [date],
    @SubmitID           [int],
    @VersionNumber      [nvarchar](16),
    @AreaName           [nvarchar](50),
    @Description        [nvarchar](max),
    @Status             [nvarchar](50),
    @Feature            [nvarchar](100),
    @AssignedTo         [int],
    @LastWorkedDate     [date],
    @LastWorkedEmployee [int],
    @Active             [bit],

    /* old data */
    @OldBugDate            [date],
    @OldSubmitID           [int],
    @OldVersionNumber      [nvarchar](16),
    @OldAreaName           [nvarchar](50),
    @OldDescription        [nvarchar](max),
    @OldStatus             [nvarchar](50),
    @OldFeature            [nvarchar](100),
    @OldAssignedTo         [int],
    @OldLastWorkedDate     [date],
    @OldLastWorkedEmployee [int],
    @OldActive             [bit]
)
AS
    BEGIN
        UPDATE  [BugTicket]
        SET     [BugDate] = @BugDate,
                [SubmitID] = @SubmitID,
                [VersionNumber] = @VersionNumber,
                [AreaName] = @AreaName,
                [Description] = @Description,
                [Status] = @Status,
                [Feature] = @Feature,
                [AssignedTo] = @AssignedTo,
                [LastWorkedDate] = @LastWorkedDate,
                [LastWorkedEmployee] = @LastWorkedEmployee,
                [Active] = @Active
        WHERE   @BugTicketID           = [BugTicketID]
            AND @OldBugDate            = [BugDate]
            AND @OldSubmitID           = [SubmitID]
            AND @OldVersionNumber      = [VersionNumber]
            AND @OldAreaName           = [AreaName]
            AND @OldDescription        = [Description]
            AND @OldStatus             = [Status]
            AND @OldFeature            = [Feature] 
            AND @OldAssignedTo         = [AssignedTo]
            AND @OldLastWorkedDate     = [LastWorkedDate]
            AND @OldLastWorkedEmployee = [LastWorkedEmployee]
            AND @OldActive             = [Active]
        RETURN  @@ROWCOUNT
    END
GO

print '' print '*** creating sp_select_active_bug_ticket_by_BugTicketID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_bug_ticket_by_BugTicketID]
(
    @BugTicketID    [int]
)
AS
    BEGIN
        SELECT  [BugTicketID], [BugDate], [SubmitID], [VersionNumber], [AreaName], 
            [Description], [Status], [Feature], [AssignedTo], [LastWorkedDate], 
            [LastWorkedEmployee], [Active]
        FROM    [BugTicket]
        WHERE   @BugTicketID = [BugTicketID]
            AND [Active] = 1
    END
GO

print '' print '*** creating sp_select_active_bug_ticket_by_BugDate ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_bug_ticket_by_BugDate]
(
    @BugDate    [date]
)
AS
    BEGIN
        SELECT  [BugTicketID], [BugDate], [SubmitID], [VersionNumber], [AreaName], 
            [Description], [Status], [Feature], [AssignedTo], [LastWorkedDate], 
            [LastWorkedEmployee], [Active]
        FROM    [BugTicket]
        WHERE   @BugDate = [BugDate]
            AND [Active] = 1
    END
GO

print '' print '*** creating sp_active_select_bug_ticket_by_SubmitID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_bug_ticket_by_SubmitID]
(
    @SubmitID    [int]
)
AS
    BEGIN
        SELECT  [BugTicketID], [BugDate], [SubmitID], [VersionNumber], [AreaName], 
            [Description], [Status], [Feature], [AssignedTo], [LastWorkedDate], 
            [LastWorkedEmployee], [Active]
        FROM    [BugTicket]
        WHERE   @SubmitID = [SubmitID]
            AND [Active] = 1
    END
GO

print '' print '*** creating sp_select_active_bug_ticket_by_VersionNumber ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_bug_ticket_by_VersionNumber]
(
    @VersionNumber    [nvarchar](16)
)
AS
    BEGIN
        SELECT  [BugTicketID], [BugDate], [SubmitID], [VersionNumber], [AreaName], 
            [Description], [Status], [Feature], [AssignedTo], [LastWorkedDate], 
            [LastWorkedEmployee], [Active]
        FROM    [BugTicket]
        WHERE   @VersionNumber = [VersionNumber]
            AND [Active] = 1
    END
GO

print '' print '*** creating sp_select_active_bug_ticket_by_AreaName ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_bug_ticket_by_AreaName]
(
    @AreaName    [nvarchar](50)
)
AS
    BEGIN
        SELECT  [BugTicketID], [BugDate], [SubmitID], [VersionNumber], [AreaName], 
            [Description], [Status], [Feature], [AssignedTo], [LastWorkedDate], 
            [LastWorkedEmployee], [Active]
        FROM    [BugTicket]
        WHERE   @AreaName = [AreaName]
            AND [Active] = 1
    END
GO

print '' print '*** creating sp_select_active_bug_ticket_by_Status ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_bug_ticket_by_Status]
(
    @Status    [nvarchar](50)
)
AS
    BEGIN
        SELECT  [BugTicketID], [BugDate], [SubmitID], [VersionNumber], [AreaName], 
            [Description], [Status], [Feature], [AssignedTo], [LastWorkedDate], 
            [LastWorkedEmployee], [Active]
        FROM    [BugTicket]
        WHERE   @Status = [Status]
            AND [Active] = 1
    END
GO

print '' print '*** creating sp_select_active_bug_ticket_by_Feature ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_bug_ticket_by_Feature]
(
    @Feature    [nvarchar](100)
)
AS
    BEGIN
        SELECT  [BugTicketID], [BugDate], [SubmitID], [VersionNumber], [AreaName], 
            [Description], [Status], [Feature], [AssignedTo], [LastWorkedDate], 
            [LastWorkedEmployee], [Active]
        FROM    [BugTicket]
        WHERE   @Feature = [Feature]
            AND [Active] = 1
    END
GO

print '' print '*** creating sp_select_active_bug_ticket_by_AssingedTo ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_bug_ticket_by_AssignedTo]
(
    @AssignedTo    [int]
)
AS
    BEGIN
        SELECT  [BugTicketID], [BugDate], [SubmitID], [VersionNumber], [AreaName], 
            [Description], [Status], [Feature], [AssignedTo], [LastWorkedDate], 
            [LastWorkedEmployee], [Active]
        FROM    [BugTicket]
        WHERE   @AssignedTo = [AssignedTo]
            AND [Active] = 1
    END
GO

print '' print '*** creating sp_deactivate_bug_ticket ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_bug_ticket]
(
    @BugTicketID    [int]
)
AS
    BEGIN
        UPDATE  [BugTicket]
        SET     [Active] = 0
        WHERE   @BugTicketID = [BugTicketID]
        RETURN  @@ROWCOUNT
    END
GO

print '' print '*** creating sp_delete_bug_ticket ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_bug_ticket]
(
    @BugTicketID        [int],
    @BugDate            [date],
    @SubmitID           [int],
    @VersionNumber      [nvarchar](16),
    @AreaName           [nvarchar](50),
    @Description        [nvarchar](max),
    @Status             [nvarchar](50),
    @Feature            [nvarchar](100),
    @AssignedTo         [int],
    @LastWorkedDate     [date],
    @LastWorkedEmployee [int],
    @Active             [bit]
)
AS
    BEGIN
        DELETE FROM [BugTicket]
        WHERE       [BugDate] = @BugDate
            AND     [SubmitID] = @SubmitID
            AND     [VersionNumber] = @VersionNumber
            AND     [AreaName] = @AreaName
            AND     [Description] = @Description
            AND     [Status] = @Status
            AND     [Feature] = @Feature
            AND     [AssignedTo] = @AssignedTo
            AND     [LastWorkedDate] = @LastWorkedDate
            AND     [LastWorkedEmployee] = @LastWorkedEmployee
            AND     [Active] = @Active
        RETURN  @@ROWCOUNT
    END
GO

/* Employee stored procedures */

print '' print '*** creating sp_create_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_create_employee]
(
    @GivenName      [nvarchar](50),
    @FamilyName     [nvarchar](100),
    @Address1       [nvarchar](50),
    @Address2       [nvarchar](50),
    @City           [nvarchar](100),
    @State          [nvarchar](2),
    @Zip            [nvarchar](10),
    @PhoneNumber    [nvarchar](11),
    @Email          [nvarchar](150)
)
AS
    BEGIN
        INSERT INTO [dbo].[Employee]
                ([GivenName], [FamilyName], [Address1], [Address2], [City], 
                [State], [Zip], [PhoneNumber], [Email])
            VALUES
                (@GivenName, @FamilyName, @Address1, @Address2, @City, 
                @State, @Zip, @PhoneNumber, @Email)
    END
GO

print '' print '*** creating sp_update_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_update_employee]
(
    /* new data */
    @EmployeeID         [int],
    @GivenName          [nvarchar](50),
    @FamilyName         [nvarchar](100),
    @Address1           [nvarchar](50),
    @Address2           [nvarchar](50),
    @City               [nvarchar](100),
    @State              [nvarchar](2),
    @Zip                [nvarchar](10),
    @PhoneNumber        [nvarchar](11),
    @Email              [nvarchar](150),

    /* old data */
    @OldGivenName       [nvarchar](50),
    @OldFamilyName      [nvarchar](100),
    @OldAddress1        [nvarchar](50),
    @OldAddress2        [nvarchar](50),
    @OldCity            [nvarchar](100),
    @OldState           [nvarchar](2),
    @OldZip             [nvarchar](10),
    @OldPhoneNumber     [nvarchar](11),
    @OldEmail           [nvarchar](150) 
)
AS
    BEGIN
        UPDATE [dbo].[Employee]
        SET [GivenName] = @GivenName,
            [FamilyName] = @FamilyName,
            [Address1] = @Address1,
            [Address2] = @Address2,
            [City] = @City,
            [State] = @State,
            [Zip] = @Zip,
            [PhoneNumber] = @PhoneNumber,
            [Email] = @Email
        WHERE   @EmployeeID = [EmployeeID]
            AND @GivenName = [GivenName]
            AND @FamilyName = [FamilyName]
            AND @Address1 = [Address1]
            AND @Address2 = [Address2]
            AND @City = [City]
            AND @State = [State]
            AND @Zip = [Zip]
            AND @PhoneNumber = [PhoneNumber]
            AND @Email = [Email]
        RETURN @@ROWCOUNT
    END
GO

print '' print '*** creating sp_select_employee_by_employeeID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_employee-by_employeeID]
(
    @EmployeeID     [int]
)
AS
    BEGIN
        SELECT [GivenName], [FamilyName], [Address1], [Address2],
            [City], [State], [Zip], [PhoneNumber], [Email]
        FROM [Employee]
        WHERE   @EmployeeID = [EmployeeID]
            AND [Active] = 1

    END
GO

print '' print '*** creating sp_deactivate_employee_by_employeeID ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_employee_by_employeeID]
(
    @EmployeeID     [int]
)
AS
    BEGIN
        UPDATE [Employee]
        SET    [Active] = 0
        WHERE  @EmployeeID = [EmployeeID]
        RETURN @@ROWCOUNT
    END
GO

print '' print '*** creating sp_delete_employee_by_employeeID ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_employee_by_employeeID]
(
    @EmployeeID         [int],
    @GivenName          [nvarchar](50),
    @FamilyName         [nvarchar](100),
    @Address1           [nvarchar](50),
    @Address2           [nvarchar](50),
    @City               [nvarchar](100),
    @State              [nvarchar](2),
    @Zip                [nvarchar](10),
    @PhoneNumber        [nvarchar](11),
    @Email              [nvarchar](150)
)
AS
    BEGIN
        DELETE FROM [Employee]
        WHERE   @EmployeeID = [EmployeeID]
            AND @GivenName = [GivenName]
            AND @FamilyName = [FamilyName]
            AND @Address1 = [Address1]
            AND @Address2 = [Address2]
            AND @City = [City]
            AND @State = [State]
            AND @Zip = [Zip]
            AND @PhoneNumber = [PhoneNumber]
            AND @Email = [Email]
        RETURN @@ROWCOUNT
    END
GO

/* Role CRUD */

print '' print '*** creating sp_create_role ***'
GO
CREATE PROCEDURE [dbo].[sp_create_role]
(
    @RoleID     [nvarchar](50)
)
AS
    BEGIN
        INSERT INTO [Role]
                ([RoleID])
            VALUES
                (@RoleID)
        RETURN @@ROWCOUNT
    END
GO
 
print '' print '*** creating sp_update_role ***'
GO
CREATE PROCEDURE [dbo].[sp_update_role]
(
    @OldRoleID      [nvarchar](50),
    @OldActive      [bit],
    @NewRoleID      [nvarchar](50),
    @NewActive      [bit]
)
AS
    BEGIN
        UPDATE [Role]
        SET [RoleID] = @NewRoleID,
            [Active] = @NewActive
        WHERE @OldRoleID = [RoleID]
            AND @OldActive = [Active]
        RETURN @@ROWCOUNT
    END
GO

print '' print '*** creating sp_select_role ***'
GO
CREATE PROCEDURE [dbo].[sp_select_role]
(
    @RoleID     [nvarchar](50)
)
AS
    BEGIN
        SELECT [RoleID], [Active]
        FROM [Role]
        WHERE [RoleID] = @RoleID
    END
GO

print '' print '*** creating sp_deactivate_role ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_role]
(
    @RoleID     [nvarchar](50)
)
AS
    BEGIN
        UPDATE [Role]
        SET [Active] = 0
        WHERE @RoleID = [RoleID]
        RETURN @@ROWCOUNT
    END
GO

print '' print '*** creating sp_delete_role ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_role]
(
    @RoleID     [nvarchar](50),
    @Active     [bit]
)
AS
    BEGIN
        DELETE FROM [Role]
        WHERE [RoleID] = @RoleID
            AND [Active] = @Active
        RETURN @@ROWCOUNT
    END
GO

/* Employee Role CRUD */

print '' print '*** creating sp_create_employee_role ***'
GO
CREATE PROCEDURE [dbo].[sp_create_employee_role]
(
    @EmployeeID     [int],
    @RoleID         [nvarchar](50)
)
AS
    BEGIN
        INSERT INTO [EmployeeRole]
                ([EmployeeID], [RoleID])
            VALUES
                (@EmployeeID, @RoleID)
        RETURN @@ROWCOUNT
    END
GO

print '' print '*** creating sp_update_employee_role ***'
GO
CREATE PROCEDURE [dbo].[sp_update_employee_role]
(
    @OldEmployeeID     [int],
    @OldRoleID         [nvarchar](50),
    @NewEmployeeID     [int],
    @NewRoleID         [nvarchar](50)
)
AS
    BEGIN
        UPDATE [EmployeeRole]
        SET [EmployeeID] = @NewEmployeeID,
            [RoleID] = @NewRoleID
        WHERE @OldEmployeeID = [EmployeeID]
            AND @OldRoleID = [RoleID]
        RETURN @@ROWCOUNT
    END
GO

print '' print '*** creating sp_select_employee_role_by_employeeID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_role_by_employeeID]
(
    @EmployeeID     [int]
)
AS
    BEGIN
        SELECT [EmployeeID], [RoleID]
        FROM [EmployeeRole]
        WHERE [EmployeeID] = @EmployeeID
    END
GO

print '' print '*** creating sp_deactivate_employee_role ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_employee_role]
(
    @EmployeeID     [int]
)
AS
    BEGIN
        UPDATE [EmployeeRole]
        SET [Active] = 0
        WHERE @EmployeeID = [EmployeeID]
        RETURN @@ROWCOUNT
    END
GO

print '' print '*** creating sp_delete_employee_role ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_employee_role]
(
    @EmployeeID     [int],
    @RoleID         [nvarchar](50),
    @Active         [bit]
)
AS
    BEGIN
        DELETE FROM [EmployeeRole]
        WHERE [EmployeeID] = @EmployeeID
            AND [RoleID] = @RoleID
            AND [Active] = @Active
        RETURN @@ROWCOUNT
    END
GO

/* Customer stored procedures */

print '' print '*** creating sp_create_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_create_customer]
(
	@GivenName		[nvarchar](50),
	@FamilyName     [nvarchar](100),
    @Address1       [nvarchar](50),
    @Address2       [nvarchar](50),
    @City           [nvarchar](100),
    @State          [nvarchar](2),
    @Zip            [nvarchar](10),
	@PhoneNumber	[nvarchar](11),
	@Email			[nvarchar](150),
    @VersionNumber  [nvarchar](16)
)
AS
    BEGIN
        INSERT INTO [Customer]
                ([GivenName], [FamilyName], [Address1], [Address2], [City], [State], [Zip], [PhoneNumber], [Email], [VersionNumber])
            VALUES
                (@GivenName, @FamilyName, @Address1, @Address2, @City, @State, @Zip, @PhoneNumber, @Email, @VersionNumber)
        RETURN [CustomerID]
    END
GO

print '' print '*** creating sp_update_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_update_customer]
(
    @CustomerID         [int],
    @OldGivenName		[nvarchar](50),
	@OldFamilyName      [nvarchar](100),
    @OldAddress1        [nvarchar](50),
    @OldAddress2        [nvarchar](50),
    @OldCity            [nvarchar](100),
    @OldState           [nvarchar](2),
    @OldZip             [nvarchar](10),
	@OldPhoneNumber	    [nvarchar](11),
	@OldEmail			[nvarchar](150),
    @OldVersionNumber   [nvarchar](16),
    @OldActive          [bit],

    @NewGivenName		[nvarchar](50),
	@NewFamilyName      [nvarchar](100),
    @NewAddress1        [nvarchar](50),
    @NewAddress2        [nvarchar](50),
    @NewCity            [nvarchar](100),
    @NewState           [nvarchar](2),
    @NewZip             [nvarchar](10),
	@NewPhoneNumber	    [nvarchar](11),
	@NewEmail			[nvarchar](150),
    @NewVersionNumber   [nvarchar](16),
    @NewActive          [bit]
)
AS
    BEGIN
        UPDATE [Customer]
        SET [GivenName] = @NewGivenName,
            [FamilyName] = @NewFamilyName,
            [Address1] = @NewAddress1,
            [Address2] = @NewAddress2,
            [City] = @NewCity,
            [State] = @NewState,
            [Zip] = @NewZip,
            [PhoneNumber] = @NewPhoneNumber,
            [Email] = @NewEmail,
            [VersionNumber] = @NewVersionNumber,
            [Active] = @NewActive
        WHERE @CustomerID = [CustomerID]
            AND @OldGivenName = [GivenName]
            AND @OldFamilyName = [FamilyName]
            AND @OldAddress1 = [Address1]
            AND @OldAddress2 = [Address2]
            AND @OldCity = [City]
            AND @OldState = [State]
            AND @OldZip = [Zip]
            AND @OldPhoneNumber = [PhoneNumber]
            AND @OldEmail = [Email]
            AND @OldVersionNumber = [VersionNumber] 
            AND @OldActive = [Active]
        RETURN @@ROWCOUNT
    END
GO

print '' print '*** creating sp_select_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_select_customer]
(

)
AS
    BEGIN

    END
GO

print '' print '*** creating sp_deactivate_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_customer]
(

)
AS
    BEGIN

    END
GO

print '' print '*** creating sp_delete_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_customer]
(

)
AS
    BEGIN

    END
GO

/* Computer Information CRUD */

print '' print '*** creating sp_create_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_update_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_select_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_deactivate_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_delete_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

/* Bug Status CRUD */

print '' print '*** creating sp_create_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_update_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_select_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_deactivate_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_delete_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

/* Product Version CRUD */

print '' print '*** creating sp_create_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_update_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_select_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_deactivate_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_delete_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

/* Product Area CRUD*/

print '' print '*** creating sp_create_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_update_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_select_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_deactivate_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_delete_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

/* Feature CRUD */

print '' print '*** creating sp_create_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_update_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_select_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_deactivate_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

print '' print '*** creating sp_delete_ ***'
GO
-- CREATE PROCEDURE [dbo].[sp_]
-- (

-- )
-- AS
--     BEGIN

--     END
-- GO

/* Bug Ticket Archive CRUD */

print '' print '*** creating sp_create_bug_ticket_archive ***'
GO
CREATE PROCEDURE [dbo].[sp_create_bug_ticket_archive]
(
    @BugTicketID    [int],
    @BugDate        [date],
    @SubmitID       [int],
    @VersionNumber  [nvarchar](16),
    @AreaName       [nvarchar](50),
    @Description    [nvarchar](max),
    @Status         [nvarchar](50),
    @Feature        [nvarchar](100),
    @AssignedTo     [int]
)
AS
    BEGIN
    INSERT INTO [dbo].[BugTicketArchive]
            ([BugTicketID], [BugDate], [SubmitID], [VersionNumber], [AreaName], [Description], [Status], [Feature], [AssignedTo])
        VALUES
            (@BugTicketID, @BugDate, @SubmitID, @VersionNumber, @AreaName, @Description, @Status, @Feature, @AssignedTo)
    END
GO

print '' print '*** creating sp_update_bug_ticket_archive ***'
GO
CREATE PROCEDURE [dbo].[sp_update_bug_ticket_archive]
(
    /* new data */
    @BugTicketID        [int],
    @BugDate            [date],
    @SubmitID           [int],
    @VersionNumber      [nvarchar](16),
    @AreaName           [nvarchar](50),
    @Description        [nvarchar](max),
    @Status             [nvarchar](50),
    @Feature            [nvarchar](100),
    @AssignedTo         [int],
    @LastWorkedDate     [date],
    @LastWorkedEmployee [int],

    /* old data */
    @OldBugDate            [date],
    @OldSubmitID           [int],
    @OldVersionNumber      [nvarchar](16),
    @OldAreaName           [nvarchar](50),
    @OldDescription        [nvarchar](max),
    @OldStatus             [nvarchar](50),
    @OldFeature            [nvarchar](100),
    @OldAssignedTo         [int],
    @OldLastWorkedDate     [date],
    @OldLastWorkedEmployee [int]
)
AS
    BEGIN
        UPDATE  [BugTicket]
        SET     [BugDate] = @BugDate,
                [SubmitID] = @SubmitID,
                [VersionNumber] = @VersionNumber,
                [AreaName] = @AreaName,
                [Description] = @Description,
                [Status] = @Status,
                [Feature] = @Feature,
                [AssignedTo] = @AssignedTo,
                [LastWorkedDate] = @LastWorkedDate,
                [LastWorkedEmployee] = @LastWorkedEmployee
        WHERE   @BugTicketID           = [BugTicketID]
            AND @OldBugDate            = [BugDate]
            AND @OldSubmitID           = [SubmitID]
            AND @OldVersionNumber      = [VersionNumber]
            AND @OldAreaName           = [AreaName]
            AND @OldDescription        = [Description]
            AND @OldStatus             = [Status]
            AND @OldFeature            = [Feature] 
            AND @OldAssignedTo         = [AssignedTo]
            AND @OldLastWorkedDate     = [LastWorkedDate]
            AND @OldLastWorkedEmployee = [LastWorkedEmployee]
        RETURN  @@ROWCOUNT
    END
GO

print '' print '*** creating sp_select_active_bug_ticket_by_BugTicketID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_bug_ticket_archive_by_BugTicketID]
(
    @BugTicketID    [int]
)
AS
    BEGIN
        SELECT  [BugTicketID], [BugDate], [SubmitID], [VersionNumber], [AreaName], 
            [Description], [Status], [Feature], [AssignedTo], [LastWorkedDate], 
            [LastWorkedEmployee]
        FROM    [BugTicket]
        WHERE   @BugTicketID = [BugTicketID]
    END
GO

print '' print '*** creating sp_delete_bug_ticket_archive ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_bug_ticket_archive]
(
    /* new data */
    @BugTicketID        [int],
    @BugDate            [date],
    @SubmitID           [int],
    @VersionNumber      [nvarchar](16),
    @AreaName           [nvarchar](50),
    @Description        [nvarchar](max),
    @Status             [nvarchar](50),
    @Feature            [nvarchar](100),
    @AssignedTo         [int],
    @LastWorkedDate     [date],
    @LastWorkedEmployee [int]
)
AS
    BEGIN
        DELETE FROM [BugTicketArchive]
        WHERE   [BugTicketID] = @BugTicketID
            AND [BugDate] = @BugDate
            AND [SubmitID] = @SubmitID
            AND [VersionNumber] = @VersionNumber
            AND [AreaName] = @AreaName
            AND [Description] = @Description
            AND [Status] = @Status
            AND [Feature] = @Feature
            AND [AssignedTo] = @AssignedTo
            AND [LastWorkedDate] = @LastWorkedDate
            AND [LastWorkedEmployee] = @LastWorkedEmployee
        RETURN @@ROWCOUNT
    END
GO