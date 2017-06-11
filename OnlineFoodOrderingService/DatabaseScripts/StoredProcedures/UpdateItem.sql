IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[Usp_UpdateProfile]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].Usp_UpdateItem
END
GO
CREATE PROCEDURE Usp_UpdateItem
@Itemid				 NUMERIC(5) =0 ,
@Categoryid	         NUMERIC(5)	=0 ,
@ItemHeader		     VARCHAR(50)   ,
@ItemDescription     VARCHAR(300)  ,
@QuaterPrice		 NUMERIC(10)   ,
@HalfPrice           NUMERIC(10)   ,
@FullPrice           NUMERIC(10)   ,
@ImageUrl			 VARCHAR(100)  ,  
@IsActive			 BIT 
As
BEGIN
	Update Items
	SET Categoryid=@Categoryid,
		ItemHeader=@ItemHeader,
		ItemDescription=@ItemDescription,
		QuaterPrice=@QuaterPrice,
		HalfPrice=@HalfPrice,
		FullPrice=@FullPrice,
		ImageUrl=@ImageUrl,
		IsActive=@IsActive
		where Itemid=@Itemid
END
