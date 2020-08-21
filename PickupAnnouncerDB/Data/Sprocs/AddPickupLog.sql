CREATE PROCEDURE [Data].[AddPickupLog]
	@json NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO Data.Pickup (
		RegistrationId,
		Cone,
		PickupTimeUTC,
		Name,
		Teacher,
		GradeLevel
	)
	SELECT
		a.RegistrationId AS RegistrationId,
		a.Cone AS Cone,
		a.PickupTimeUTC AS PickupTimeUTC,
		b.Name AS Name,
		b.Teacher AS Teacher,
		b.GradeLevel AS GradeLevel
	FROM OPENJSON(@json)
		WITH (
			RegistrationId INT '$.car',
			Cone INT '$.cone',
			Students NVARCHAR(MAX) '$.students' AS JSON,
			PickupTimeUTC DATETIME2 '$.pickupTimeUTC'
		) AS a
	CROSS APPLY
		OPENJSON(a.Students)
		WITH (
			Name VARCHAR(50) '$.name',
			Teacher VARCHAR(50) '$.teacher',
			GradeLevel VARCHAR(50) '$.gradeLevel'
		) AS b
END;
