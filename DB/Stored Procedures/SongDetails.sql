CREATE PROCEDURE [dbo].[SongDetails]
		@artistID INT 
AS
BEGIN

	 select songID, albumID, artistID, title, bpm
  from [MultiTracksDB].[dbo].[Song]
  where artistID = @artistID

END