-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetAppById] 
	@id int

AS
	SELECT *
	FROM Appointments AS A 

	WHERE(A.AppointmentId = @id)