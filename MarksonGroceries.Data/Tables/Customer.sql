CREATE TABLE [dbo].[Customer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SessionId] NVARCHAR(25) NOT NULL, 
    [CartSizeId] INT NOT NULL, 
    CONSTRAINT [FK_Customer_ToCartSizes] FOREIGN KEY ([CartSizeId]) REFERENCES [CartSizes]([Id])
)
