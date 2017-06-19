
 IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[Usp_GrandTotal]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].Usp_GrandTotal
END
GO
--EXEC Usp_GrandTotal
CREATE PROCEDURE Usp_GrandTotal
@Userid			NUMERIC(10)	=0 

As
BEGIN
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	select GrandTotal
 from Orders orders 
 where orders.Userid=@Userid and OrderDate< DATEADD(day,-30, GETDATE()) 
END

