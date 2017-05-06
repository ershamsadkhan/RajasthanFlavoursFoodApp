IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'OrderStatus'
          AND Object_ID = Object_ID(N'Orders'))
BEGIN
    ALTER TABLE Orders ADD  OrderStatus  VARCHAR(2)
END