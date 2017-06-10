IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[Usp_GetCategoryList]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].Usp_GetCategoryList
END
GO
--EXEC Usp_GetItemsList 
CREATE PROCEDURE Usp_GetCategoryList
@Categoryid			NUMERIC(10)	=0
As
BEGIN
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	SELECT ca.CategoryHeader,
		   ca.Categoryid,
		   ca.CategoryDescription
	FROM Category ca
	WHERE(@Categoryid=0 OR ca.Categoryid=@Categoryid)

END


