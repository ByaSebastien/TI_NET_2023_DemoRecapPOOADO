CREATE TABLE product
(
	Id INT IDENTITY,
	Product_Name nvarchar(50) NOT NULL,
	Product_Description nvarchar(50) NULL,
	Quantity INT NOT NULL,
	Category varchar(50) NOT NULL,
	Price Money NOT NULL,
	CONSTRAINT PK_PRODUCT PRIMARY KEY (Id),
	CONSTRAINT CK_PRODUCT_PRICE CHECK (Price >= 0)
)
