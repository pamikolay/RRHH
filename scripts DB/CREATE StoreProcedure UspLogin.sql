use RRHH

go




CREATE PROC uspLogin
	@Email NVARCHAR(50),
	@Password NVARCHAR(15)

AS
	SELECT ID FROM Users WHERE Email=@Email AND Password=@Password

go