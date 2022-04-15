USE master
IF NOT EXISTS (
    SELECT [name]
    FROM sys.databases
    WHERE [name] = 'MachineMonitoring'
)
CREATE DATABASE MachineMonitoring;
GO
USE MachineMonitoring;

IF OBJECT_ID('[dbo].[MachineProduction]') IS NOT NULL 
    DROP TABLE [dbo].[MachineProduction]
IF OBJECT_ID('[dbo].[Machine]') IS NOT NULL
    DROP TABLE [dbo].[Machine]

CREATE TABLE [dbo].[Machine]
(
    machineId INT NOT NULL PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(50) NOT NULL,
    [Description] NVARCHAR(250)
);

CREATE TABLE [dbo].[MachineProduction]
(
    MachineProductionId INT NOT NULL PRIMARY KEY IDENTITY,
    machineId INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Machine](machineId) ON DELETE CASCADE,
    totalProduction INT NOT NULL DEFAULT (0)
);

GO

INSERT INTO [dbo].[Machine]
( 
    Name,Description
)
VALUES
(
    'machine_1', 'this is a lorem ipsum description for machine 1 ...'
),
(
    'machine_2', 'this is a lorem ipsum description for machine 2 ...'
);

GO

INSERT INTO [dbo].[MachineProduction]
( 
    machineId, totalProduction
)
VALUES
(
    1, 100
),
(
    1, 90
),
( 
    2, 50
)