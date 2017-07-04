
--Usp_OfferAppliedInLast30Days 7,'C200'
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[Usp_OfferAppliedInLast30Days]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE Usp_OfferAppliedInLast30Days
END
GO
CREATE PROCEDURE Usp_OfferAppliedInLast30Days
@Userid		NUMERIC(5) =0,
@OfferCode  Varchar(10)

AS
BEGIN
DECLARE @retVal int=0
if(@OfferCode='C200')
	BEGIN
		   Select @retVal=COUNT(*)
		   from 
		   offer ohf INNER JOIN OfferHistory oh
		   ON ohf.OfferId=oh.OfferId
		   INNER JOIN Orders orders
		   on oh.OrderId= orders.OrderId 
		   where OfferDate > DATEADD(day,-30,GETDATE()) and orders.Userid=@Userid and OfferCode=@OfferCode

		   if(@retVal=0)
		   BEGIN
			   Select  ohf.OfferCode ,ohf.OfferHeader, ohf.OfferDescription, ohf.PercentOffer, ohf.RsOffer
			   from 
			   offer ohf 
			   where OfferCode=@OfferCode
		   END
	END
ELSE
	BEGIN
		   Select @retVal=COUNT(*)
		   from 
		   offer ohf INNER JOIN OfferHistory oh
		   ON ohf.OfferId=oh.OfferId
		   INNER JOIN Orders orders
		   on oh.OrderId= orders.OrderId 
		   where OfferDate > DATEADD(day,-30,GETDATE()) and orders.Userid=@Userid and (OfferCode=@OfferCode OR OfferCode='C200')

		   if(@retVal=0)
		   BEGIN
			   Select  ohf.OfferCode ,ohf.OfferHeader, ohf.OfferDescription, ohf.PercentOffer, ohf.RsOffer
			   from 
			   offer ohf 
			   where OfferCode=@OfferCode
		   END
	END
END

 