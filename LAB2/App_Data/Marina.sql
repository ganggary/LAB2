
/******************************** CREATE DOCK TABLE  ****************************************/

CREATE TABLE [dbo].[Dock](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[WaterService] [bit] NOT NULL,
	[ElectricalService] [bit] NOT NULL,
 CONSTRAINT [PK_Dock] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
))
GO

/******************************** CREATE SLIP TABLE  ****************************************/

CREATE TABLE [dbo].[Slip](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Width] [int] NOT NULL,
	[Length] [int] NOT NULL,
	[DockID] [int] NOT NULL,
 CONSTRAINT [PK_Slip] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
))

GO
ALTER TABLE [dbo].[Slip]  WITH CHECK ADD  CONSTRAINT [FK_SlipDock] FOREIGN KEY([DockID])
REFERENCES [dbo].[Dock] ([ID])
GO
ALTER TABLE [dbo].[Slip] CHECK CONSTRAINT [FK_SlipDock]

/******************************** CREATE CUSTOMER TABLE  ***********************************/

CREATE TABLE [dbo].[Customer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](30) NOT NULL,
	[LastName] [varchar](30) NOT NULL,
	[Phone] [varchar](14) NOT NULL,
	[City] [varchar](30) NOT NULL	
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
))
GO

/******************************** CREATE LEASE TABLE  *************************************/

CREATE TABLE [dbo].[Lease](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[SlipID] [int] NOT NULL,
	[CustomerID] [int] NOT NULL
 CONSTRAINT [PK_Lease] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
))
GO
ALTER TABLE [dbo].[Lease]  WITH CHECK ADD  CONSTRAINT [FK_LeaseCustomer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[Lease] CHECK CONSTRAINT [FK_LeaseCustomer]
GO
ALTER TABLE [dbo].[Lease]  WITH CHECK ADD  CONSTRAINT [FK_LeaseSlip] FOREIGN KEY([SlipID])
REFERENCES [dbo].[Slip] ([ID])
GO
ALTER TABLE [dbo].[Lease] CHECK CONSTRAINT [FK_LeaseSlip]
GO


/**************************************** PART 2 INSERT DATA ******************************/

--INSERT INTO DOCK

INSERT INTO Dock([Name], WaterService, ElectricalService) VALUES('Dock A', 1, 1)
INSERT INTO Dock([Name], WaterService, ElectricalService) VALUES('Dock B', 1, 0)
INSERT INTO Dock([Name], WaterService, ElectricalService) VALUES('Dock C', 0, 1)
GO


--INSERT INTO SLIP

-- Dock A slips

INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,16,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,16,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,16,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,16,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,16,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,16,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,20,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,20,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,20,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,20,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,20,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,22,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,22,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,22,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,22,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,24,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,24,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,24,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,24,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,26,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,26,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,20,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,26,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,26,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,26,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,28,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,28,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,28,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,28,1)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,28,1)
GO

-- Dock B slips

INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,18,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,18,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,18,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,18,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,18,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,18,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,20,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,20,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,20,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,22,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,22,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,22,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,22,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,24,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,24,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,24,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,24,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,28,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,28,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,28,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,30,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,30,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,30,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,30,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,30,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,32,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,32,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,32,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,32,2)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(8,32,2)
GO

-- Dock C slips

INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,22,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,22,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,22,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,22,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,22,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,22,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,22,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,22,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,22,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,22,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,24,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,24,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,24,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,24,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,24,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,24,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,24,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,24,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,24,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(10,24,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(12,28,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(12,28,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(12,28,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(12,28,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(12,28,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(12,28,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(12,28,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(12,28,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(12,28,3)
INSERT INTO SLIP(Width, [Length], DockId) VALUES(12,28,3)
GO


-- INSERT CUSTOMER
INSERT INTO Customer(FirstName,LastName,Phone,City) VALUES('John','Doe','265-555-1212','Phoenix');
INSERT INTO Customer(FirstName,LastName,Phone,City) VALUES('Sara','Williams','403-555-9585','Calgary');
INSERT INTO Customer(FirstName,LastName,Phone,City) VALUES('Ken','Wong','802-555-3214','Kansas City');
GO

-- INSERT LEASE
INSERT INTO Lease(SlipID,CustomerID) VALUES(20,1)
INSERT INTO Lease(SlipID,CustomerID) VALUES(42,2)
INSERT INTO Lease(SlipID,CustomerID) VALUES(88,3)
GO

---EXECUTE TEST SQL STATEMENT

SELECT c.FirstName, c.LastName, s.Length, d.Name 
FROM   Customer c inner join Lease l on c.ID = l.CustomerID
                  inner join Slip s on l.SlipID = s.ID
                  inner join Dock d on s.DockID = d.ID
GO