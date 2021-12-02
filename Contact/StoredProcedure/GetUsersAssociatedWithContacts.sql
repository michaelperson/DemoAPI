CREATE PROCEDURE [dbo].[GetUsersAssociatedWithContacts]
	@UserId int
	
AS
	SELECT * From [User] WHERE Email in (select Email From Contact)
	AND Id=@UserId
RETURN 0
