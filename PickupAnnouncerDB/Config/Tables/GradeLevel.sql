﻿CREATE TABLE [Config].[GradeLevel]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(50) NOT NULL, 
	[BackgroundColor] VARCHAR(7) NOT NULL,
	[TextColor] VARCHAR(7) NOT NULL
)