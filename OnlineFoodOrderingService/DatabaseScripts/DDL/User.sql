IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE name=N'Users')
BEGIN
	ALTER TABLE Users ADD UserPhoneNumber VARCHAR(10)
	ALTER TABLE Users ADD UserEmailAddress VARCHAR(100)
	ALTER TABLE Users ADD RegisterDate SMALLDATETIME
END