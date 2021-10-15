CREATE PROCEDURE [dbo].[AlbumDetails]
		@artistID INT 
AS
BEGIN

	SELECT *
	FROM [MultiTracksDB].[dbo].[Album]
	WHERE artistID = @artistID

END
