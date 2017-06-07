IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[Usp_UpdateDeliveryAddress]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].Usp_UpdateDeliveryAddress
END
GO
CREATE PROCEDURE Usp_UpdateDeliveryAddress
@Userid			 NUMERIC(5)	=0,
@DeliveryAddress  VARCHAR(200)  

As
BEGIN
	Update Users
	SET DeliveryAddress=@DeliveryAddress
		where Userid=@Userid
END