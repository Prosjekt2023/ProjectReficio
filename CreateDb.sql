drop database if exists ReficioDB;
create database if not exists ReficioDB;
use ReficioDB;

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
                                                       
`MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
                                                       
`ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
                                                       
CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `ApplicationUsers` (
                                    
`Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                                    
`Firstname` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                                    
`Lastname` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                                    
`UserName` longtext CHARACTER SET utf8mb4 NULL,
                                    
`NormalizedUserName` longtext CHARACTER SET utf8mb4 NULL,
                                    
`Email` longtext CHARACTER SET utf8mb4 NULL,
                                    
`NormalizedEmail` longtext CHARACTER SET utf8mb4 NULL,
                                    
`EmailConfirmed` tinyint(1) NOT NULL,
                                    
`PasswordHash` longtext CHARACTER SET utf8mb4 NULL,
                                    
`SecurityStamp` longtext CHARACTER SET utf8mb4 NULL,
                                    
`ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
                                    
`PhoneNumber` longtext CHARACTER SET utf8mb4 NULL,
                                    
`PhoneNumberConfirmed` tinyint(1) NOT NULL,
                                    
`TwoFactorEnabled` tinyint(1) NOT NULL,
                                    
`LockoutEnd` datetime(6) NULL,
                                    
`LockoutEnabled` tinyint(1) NOT NULL,
                                    
`AccessFailedCount` int NOT NULL,
                                    
CONSTRAINT `PK_ApplicationUsers` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetRoles` (
                               
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                               
    `Name` varchar(256) CHARACTER SET utf8mb4 NULL,
                               
    `NormalizedName` varchar(256) CHARACTER SET utf8mb4 NULL,
                               
    `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
                               
    CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUsers` (
                               
`Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                               
`UserName` varchar(256) CHARACTER SET utf8mb4 NULL,
                               
`NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 NULL,
                               
`Email` varchar(256) CHARACTER SET utf8mb4 NULL,
                               
`NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 NULL,
                               
`EmailConfirmed` tinyint(1) NOT NULL,
                               
`PasswordHash` longtext CHARACTER SET utf8mb4 NULL,
                               
`SecurityStamp` longtext CHARACTER SET utf8mb4 NULL,
                               
`ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
                               
`PhoneNumber` longtext CHARACTER SET utf8mb4 NULL,
                               
`PhoneNumberConfirmed` tinyint(1) NOT NULL,
                               
`TwoFactorEnabled` tinyint(1) NOT NULL,
                               
`LockoutEnd` datetime(6) NULL,
                               
`LockoutEnabled` tinyint(1) NOT NULL,
                               
`AccessFailedCount` int NOT NULL,
                               
CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `AspNetRoles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`)
VALUES ('490dcd67-8acc-4b60-914a-a103b79b0321', 'Admin', 'ADMIN', 'your-concurrency-stamp');

INSERT INTO `AspNetRoles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`)
VALUES ('50459076-8fc6-4ba7-836f-5338b726bc27', 'Ansatt', 'ANSATT', 'your-concurrency-stamp');

INSERT INTO `AspNetRoles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`)
VALUES ('70ce6502-c8ea-4ce4-9e2f-40659fd32d9f', 'Elektriker', 'ELEKTRIKER', 'your-concurrency-stamp');

INSERT INTO `AspNetRoles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`)
VALUES ('f0fe3f4c-9545-4654-a6fc-57fde30fc7c8', 'Mekaniker', 'MEKANIKER', 'your-concurrency-stamp');

CREATE TABLE `AspNetRoleClaims` (
                                    
`Id` int NOT NULL AUTO_INCREMENT,
                                    
`RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                                    
`ClaimType` longtext CHARACTER SET utf8mb4 NULL,
                                    
`ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
                                    
CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY (`Id`),
                                    
CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUserClaims` (
                                    
`Id` int NOT NULL AUTO_INCREMENT,
                                    
`UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                                    
`ClaimType` longtext CHARACTER SET utf8mb4 NULL,
                                    
`ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
                                    
CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY (`Id`),
                                    
CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUserLogins` (
                                    
`LoginProvider` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
                                    
`ProviderKey` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
                                    
`ProviderDisplayName` longtext CHARACTER SET utf8mb4 NULL,
                                    
`UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                                    
CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
                                    
CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUserRoles` (
                                   
`UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                                   
`RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                                   
CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY (`UserId`, `RoleId`),
                                   
CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
                                   
CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUserTokens` (
                                    
`UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                                    
`LoginProvider` varchar(128) CHARACTER SET utf8mb4 NOT NULL,                                
    
`Name` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
                                    
`Value` longtext CHARACTER SET utf8mb4 NULL,
                                    
CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
                                    
CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20231110215715_IdentityTables', '7.0.13');

COMMIT;

START TRANSACTION;

ALTER TABLE `AspNetUsers` ADD `Firstname` nvarchar(255) NOT NULL DEFAULT '';

ALTER TABLE `AspNetUsers` ADD `Lastname` nvarchar(255) NOT NULL DEFAULT '';

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20231110221531_NewRegistrationsColums', '7.0.13');

COMMIT;

-- Create table ServiceFormEntry, if it doesn't exists
create table if not EXISTS ServiceFormEntry
(
      ServiceFormId INT not null unique auto_increment PRIMARY KEY,
      Customer NVARCHAR(255) NOT NULL,
      DateReceived DATE NOT NULL,
      Address NVARCHAR(255),
      Email NVARCHAR(255),
      OrderNumber INT,
      Phone INT,
      ProductType NVARCHAR(255),
      Year INT,
      Service NVARCHAR(255),
      Warranty NVARCHAR(255),
      SerialNumber INT,
      Agreement NVARCHAR(255),
      RepairDescription NVARCHAR(255),
      UsedParts NVARCHAR(255),
      WorkHours NVARCHAR(255),
      CompletionDate NVARCHAR(255),
      ReplacedPartsReturned NVARCHAR(255),
      ShippingMethod NVARCHAR(255),
      CustomerSignature NVARCHAR(255),
      RepairerSignature NVARCHAR(255),
      ServiceOrderStatus NVARCHAR(255)   
);

-- Table for the Checklist
CREATE TABLE IF NOT EXISTS Checklist
(
    ChecklistId INT AUTO_INCREMENT PRIMARY KEY,
    ClutchCheck VARCHAR(50),
    BrakeCheck VARCHAR(50),
    DrumBearingCheck VARCHAR(50),
    PTOCheck VARCHAR(50),
    ChainTensionCheck VARCHAR(50),
    WireCheck VARCHAR(50),
    PinionBearingCheck VARCHAR(50),
    ChainWheelKeyCheck VARCHAR(50),
    HydraulicCylinderCheck VARCHAR(50),
    HoseCheck VARCHAR(50),
    HydraulicBlockTest VARCHAR(50),
    TankOilChange VARCHAR(50),
    GearboxOilChange VARCHAR(50),
    RingCylinderSealsCheck VARCHAR(50),
    BrakeCylinderSealsCheck VARCHAR(50),
    WinchWiringCheck VARCHAR(50),
    RadioCheck VARCHAR(50),
    ButtonBoxCheck VARCHAR(50),
    PressureSettings VARCHAR(50),
    FunctionTest VARCHAR(50),
    TractionForceKN VARCHAR(50),
    BrakeForceKN VARCHAR(50),
    Sign VARCHAR(255), -- Signature
    Freeform TEXT, -- Any additional freeform text or comments
    CompletionDate DATE NOT NULL -- The date the checklist was completed
);
