drop database if exists ReficioDB;
create database if not exists ReficioDB;
use ReficioDB;

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
      RepairerSignature NVARCHAR(255)
);


CREATE TABLE IF NOT EXISTS CheckpointsEntry
(
    CheckpointId INT AUTO_INCREMENT PRIMARY KEY,
    ChecklistId INT UNIQUE, -- This ensures that each CheckpointsEntry can only be associated with one Checklist
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
    FOREIGN KEY (ChecklistId) REFERENCES Checklist (ChecklistId)
);


-- Table for the Checklist
    CREATE TABLE IF NOT EXISTS Checklist
    (
    ChecklistId INT AUTO_INCREMENT PRIMARY KEY,
    Sign VARCHAR(255), -- Signature
    Freeform TEXT, -- Any additional freeform text or comments
    CompletionDate DATE NOT NULL -- The date the checklist was completed
);