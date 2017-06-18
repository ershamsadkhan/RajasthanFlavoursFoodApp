IF NOT EXISTS(SELECT 1 FROM information_schema.tables WHERE table_name = 'OfferHistory')
BEGIN
	CREATE TABLE 
	OfferHistory(OfferHistoryId NUMERIC(8) PRIMARY KEY NOT NULL IDENTITY,
	OrderId NUMERIC(8) NOT NULL FOREIGN KEY REFERENCES Orders(OrderId),
	OfferCode varchar(10) NOT NULL FOREIGN KEY REFERENCES Offer(OfferCode),
	Userid  NUMERIC(8) NOT NULL FOREIGN KEY REFERENCES Users(Userid),
	OfferDate DateTime )
	END
	

