IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[Usp_OfferAppliedInLast30Days]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].Usp_OfferAppliedInLast30Days
END
GO
CREATE PROCEDURE Usp_OfferAppliedInLast30Days
@Userid		NUMERIC(5) =0,
@OfferCode  Varchar(10)

AS
BEGIN
Select oh.Userid, orders.Orderid, OfferCode , OfferDate, GrandTotal 
	   from OfferHistory oh INNER JOIN Orders orders
	   on oh.OrderId= orders.OrderId 
	   where OfferDate > DATEADD(day,-30,GETDATE()) and oh.Userid=@Userid and OfferCode=@OfferCode
	   order by OfferDate desc
END

