IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'OrderStatus'
          AND Object_ID = Object_ID(N'Orders'))
BEGIN
    ALTER TABLE Orders ADD  OrderStatus  VARCHAR(2)
END

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'DeliveryAddress'
          AND Object_ID = Object_ID(N'Orders'))
BEGIN
    ALTER TABLE Orders ADD  DeliveryAddress  VARCHAR(1000)
END

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'CityCode'
          AND Object_ID = Object_ID(N'Orders'))
BEGIN
    ALTER TABLE Orders ADD  CityCode  NUMERIC(3)
END