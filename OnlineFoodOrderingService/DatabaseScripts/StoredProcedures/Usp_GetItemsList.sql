IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[Usp_GetItemsList]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].Usp_GetItemsList
END
GO
--EXEC Usp_GetItemsList 
CREATE PROCEDURE Usp_GetItemsList
@Categoryid			NUMERIC(10)	=0,
@Itemid				NUMERIC(10)	=0
As
BEGIN
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	SELECT ca.CategoryHeader,
		   ca.Categoryid,
		   ca.CategoryDescription,
		   it.Itemid,
		   it.ItemHeader,
		   it.ItemDescription,
		   it.QuaterPrice,
		   it.HalfPrice,
	       it.FullPrice,
		   it.ImageUrl
	FROM Category ca
	INNER JOIN Items it
	ON ca.Categoryid=it.Categoryid
	WHERE(@Categoryid=0 OR ca.Categoryid=@Categoryid)
	AND(@Itemid=0 OR it.Itemid=@Itemid)

END


