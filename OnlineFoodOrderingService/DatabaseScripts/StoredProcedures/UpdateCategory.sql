IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[Usp_UpdateCategory]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].Usp_UpdateCategory
END
GO
CREATE PROCEDURE Usp_UpdateCategory
@Categoryid	             NUMERIC(5)	=0 ,
@CategoryHeader		     VARCHAR(50)   ,
@CategoryDescription     VARCHAR(300)  
As
BEGIN
	Update Category
	SET CategoryHeader=@CategoryHeader,
		CategoryDescription=@CategoryDescription
		where Categoryid=@Categoryid
END