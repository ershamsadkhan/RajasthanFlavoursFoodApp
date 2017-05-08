IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE name=N'OrderLineItem')
BEGIN
	ALTER TABLE OrderLineItem ADD ItemId numeric(8,0) FOREIGN KEY (ItemId) REFERENCES Items(ItemId)
END