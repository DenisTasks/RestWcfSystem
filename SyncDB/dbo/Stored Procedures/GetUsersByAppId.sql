-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetUsersByAppId] 
	@a1 int

AS
BEGIN
	SELECT  U.[UserId]
		    ,[IsActive]
			,[Name]
			,[UserName]
			,[Password]
			,[Salt]
	FROM Users as U

	INNER JOIN UserAppointments AS UA ON U.UserId = UA.UserId
	INNER JOIN Appointments AS A ON A.AppointmentId = UA.AppointmentId

	WHERE UA.AppointmentId = @a1
	AND A.AppointmentId = @a1
END