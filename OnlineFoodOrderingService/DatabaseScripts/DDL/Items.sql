IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE name=N'Items')
BEGIN
	ALTER TABLE Items ADD IsActive int
END