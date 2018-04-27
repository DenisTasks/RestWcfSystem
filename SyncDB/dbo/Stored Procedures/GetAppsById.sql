-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetAppsById] 
	@p1 int

AS
BEGIN
	SELECT A.[AppointmentId]
		  ,[Subject]
		  ,[BeginningDate]
		  ,[EndingDate]
		  ,[OrganizerId]
		  ,[LocationId]
		  ,[Organizer_UserId]
	FROM Appointments as A

	INNER JOIN UserAppointments AS UA ON A.AppointmentId = UA.AppointmentId
	INNER JOIN Users AS U ON U.UserId = UA.UserId

	WHERE UA.UserId = @p1
	AND U.UserId = @p1
END