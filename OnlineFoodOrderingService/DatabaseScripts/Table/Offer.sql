IF NOT EXISTS(SELECT 1 FROM information_schema.tables WHERE table_name = 'Offer')
BEGIN
	CREATE TABLE Offer( OfferId int PRIMARY KEY NOT NULL IDENTITY(1,1),OfferCode varchar(10) NOT NULL ,OfferHeader varchar(100), OfferDescription varchar(200),IsActive BIT, PercentOffer Numeric(3) Default 0, RsOffer Numeric(5) default 0, ExpiryDate DATE default GETDATE())
END


