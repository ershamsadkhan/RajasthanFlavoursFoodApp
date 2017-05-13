--EXEC Usp_GetOrders 2
ALTER PROCEDURE Usp_GetOrders
@Userid			NUMERIC(10)	=0,
@FromDate		DateTime=Null,
@ToDate			DateTime=Null

As
BEGIN	
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
 if(@FromDate IS NULL)
	BEGIN
		SELECT o.OrderId,
			   o.OrderDate,
			   U.Userid
		FROM Orders o
		INNER JOIN Users U
		ON o.Userid=U.Userid
		WHERE(@Userid=0 OR U.Userid=@Userid)
	END
ELSE
	BEGIN
		SELECT o.OrderId,
			   o.OrderDate,
			   U.Userid
		FROM Orders o
		INNER JOIN Users U
		ON o.Userid=U.Userid
		WHERE(@Userid=0 OR U.Userid=@Userid)
		AND (o.OrderDate BETWEEN @FromDate AND @ToDate)
	END
END

