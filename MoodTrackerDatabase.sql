USE master;

IF DB_ID('MoodTracker') IS NULL
BEGIN
	--ALTER DATABASE MoodTracker SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE MoodTracker;

CREATE DATABASE MoodTracker;

USE MoodTracker;

CREATE TABLE Entry(
Id int IDENTITY(1,1),
[Date] DateTime NOT NULL,
Level int NOT NULL,
Note nvarchar(500) NULL,
CONSTRAINT pk_EntryId PRIMARY KEY (Id))

CREATE TABLE Factor(
Id int IDENTITY(1,1),
[Name] nvarchar (30) NOT NULL,
IsDefault BIT NOT NULL DEFAULT 0,
QuestionText nvarchar (50) NULL,
ValueType int NOT NULL, -- 1 is 1-5, 2 is Yes/No
CONSTRAINT pk_FactorId PRIMARY KEY (Id))

CREATE TABLE FactorEntry(
EntryId int NOT NULL,
FactorId int NOT NULL,
[Value] int NOT NULL
CONSTRAINT pk_EntryIdFactorId PRIMARY KEY(EntryId,FactorId))

SET IDENTITY_INSERT Factor ON

INSERT INTO Factor(Id, Name, IsDefault, QuestionText, ValueType) VALUES (1, 'Sleep', 1, 'How did you sleep last night?', 1)
INSERT INTO Factor(Id, Name, IsDefault, QuestionText, ValueType) VALUES (2, 'Nutrition', 1, 'How has your diet been today?', 1)
INSERT INTO Factor(Id, Name, IsDefault, QuestionText, ValueType) VALUES (3, 'Stress', 1, 'How well are you handling stress today?', 1)

SET IDENTITY_INSERT Factor OFF

ALTER TABLE FactorEntry
ADD CONSTRAINT fk_EntryId FOREIGN KEY (EntryId) REFERENCES [Entry] (Id);

ALTER TABLE FactorEntry
ADD CONSTRAINT fk_FactorId FOREIGN KEY (FactorId) REFERENCES Factor (Id);

END



 
--//Each entry has an ID, a date/time, a Mood Level, and a list of factors (can be none), and an optional note.
--//Each factor has an ID, name, value for each factor 1-5, descripti question text.
--//
--//existing factor: look through list of factors (some marked default) in SQL. Print them in a nice list with ID numbers and names.
--//an entry can have muliple factors.
--//Need an Entry table with Entry ID, MoodLevel, DateTime, Note 
--//Need a Factor table with Id, Name, IsDefault, and Question text
--//Need a FactorEntry table with the Factor ID, Entry ID, Value, and Note.