
 IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[Usp_GrandTotal]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE Usp_GrandTotal
END
GO
--EXEC Usp_GrandTotal 3
CREATE PROCEDURE Usp_GrandTotal
@Userid			NUMERIC(10)	=0 

As
BEGIN
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
 select SUM(GrandTotal) AS GrandTotal
 from Orders orders 
 where orders.Userid=@Userid and OrderDate> DATEADD(day,-30, GETDATE()) 
 GROUP BY orders.Userid
END

