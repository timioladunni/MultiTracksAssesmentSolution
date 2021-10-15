CREATE PROCEDURE dbo.GetArtistDetails
	@artistID INT 
AS
BEGIN

	SELECT *
	FROM [MultiTracksDB].[dbo].[Artist]
	WHERE artistID = @artistID

END

