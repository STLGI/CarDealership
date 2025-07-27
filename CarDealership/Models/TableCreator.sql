DROP TABLE IF EXISTS Companies
DROP TABLE IF EXISTS Cars

CREATE TABLE [dbo].[Companies] (
    [Id]    INT        NOT NULL,
    [Name]  NCHAR (30) NOT NULL,
    [sName] NCHAR (30) NOT NULL,
    [img]   NCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Cars] (
    [Id]             INT        NOT NULL,
    [Model]          NCHAR (30) NOT NULL,
    [ManufacturerId] INT        NOT NULL,
    [Transmission]   NCHAR (20) NOT NULL,
    [Fuel]           NCHAR (20) NOT NULL,
    [MileAge]        INT        NOT NULL,
    [Price]          INT        NOT NULL,
    [Pics]           INT        NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
    
INSERT INTO [dbo].[Companies] (Id, Name, sName, img) VALUES
(0, 'None', 'None', 'img/cross.png'),
(1, 'Mercedes-Benz', 'Mercedes', 'img/mercedes.png'),
(2, 'Toyota Motor Corporation', 'Toyota', 'img/toyota.png'),
(3, 'Audi AG', 'Audi', 'img/audi.png'),
(4, 'Volksvagen', 'Volksvagen', 'img/volkswagen.png');

INSERT INTO [dbo].[Cars] 
    ([Id], [Model], [ManufacturerId], [Transmission], [Fuel], [MileAge], [Price], [Pics])
VALUES
    (1, 'W124', 1, 'auto', 'Gasoline', 370000, 5000, 10),
    (2, 'Supra', 2, 'manual', 'Gasoline', 0, 17000, 1),
    (3, 'A8 D5', 3, 'auto', 'Gasoline', 0, 60000, 1),
    (4, 'A8 D5', 3, 'auto', 'Gasoline', 0, 60000, 1),
    (5, 'A8 D5', 3, 'auto', 'Gasoline', 0, 60000, 1);