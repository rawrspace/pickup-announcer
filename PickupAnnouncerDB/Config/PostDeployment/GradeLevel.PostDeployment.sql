IF (SELECT COUNT(*) FROM Config.GradeLevel) < 1 
BEGIN
	INSERT INTO 
		Config.GradeLevel(Name, BackgroundColor, TextColor) 
		VALUES
			('PK', '#4C6472', '#FFFFFF'),
			('KG', '#57A4B1', '#FFFFFF'),
			('01', '#B0D894', '#FFFFFF'),
			('02', '#FADE89', '#FFFFFF'),
			('03', '#F95355', '#FFFFFF'),
			('04', '#23254C', '#FFFFFF')
END;
GO