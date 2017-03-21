use RRHH

go




CREATE PROC uspLogin
	@UserTableEmail NVARCHAR(50),
	@UserTablePassword NVARCHAR(15)

AS
	SELECT UserTableID FROM UserTable WHERE UserTableEmail=@UserTableEmail AND UserTablePassword=@UserTablePassword

go