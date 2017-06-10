--EXEC Usp_GetOrders 0,'2017-06-07','2017-06-08'
ALTER PROCEDURE Usp_GetOrders
@Userid			NUMERIC(10)	=0,
@FromDate		DateTime=Null,
@ToDate			DateTime=Null

As
BEGIN	
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
 if(@Userid =0)
	BEGIN
		SELECT o.OrderId,
			   o.OrderDate,
			   U.Userid,
			   U.UserName,
			   o.OrderStatus,
			   o.DeliveryAddress,
			   o.CityCode,
			   Gt.GrandTotal
		FROM Orders o
		INNER JOIN Users U
		ON o.Userid=U.Userid
		INNER JOIN (
			SELECT SUM (OLI. Price*OLI.Quantity) AS GrandTotal,OLI.OrderId AS OrderId
			FROM Orders o
			inner join OrderLineItem OLI
			on o.OrderId=OLI.OrderId
			group by OLI.OrderId
		) Gt 
		ON Gt.OrderId =o.OrderId
		WHERE (ISNULL(@FromDate,'')='' OR  ISNULL(@ToDate,'')='' OR 
			  o.OrderDate BETWEEN @FromDate AND @ToDate)
		ORDER BY 1 DESC
	END
ELSE
	BEGIN
		SELECT o.OrderId,
			   o.OrderDate,
			   U.Userid,
			   U.UserName,
			   o.OrderStatus,
			   o.DeliveryAddress,
			   o.CityCode,
			   Gt.GrandTotal
		FROM Orders o
		INNER JOIN Users U
		ON o.Userid=U.Userid
		INNER JOIN (
			SELECT SUM (OLI. Price) AS GrandTotal,OLI.OrderId AS OrderId
			FROM Orders o
			inner join OrderLineItem OLI
			on o.OrderId=OLI.OrderId
			group by OLI.OrderId
		) Gt 
		ON Gt.OrderId =o.OrderId
		WHERE U.Userid=@Userid
		ORDER BY 1 DESC
	END
END

