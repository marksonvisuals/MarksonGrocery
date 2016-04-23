CREATE TABLE [dbo].[Customer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SessionId] NVARCHAR(25) NOT NULL, 
    [CartSizeId] INT NOT NULL, 
    [CheckoutTypeId] INT NULL, 
    [CheckoutTime] DATETIME2(0) NULL, 
    CONSTRAINT [FK_Customer_ToCartSizes] FOREIGN KEY ([CartSizeId]) REFERENCES [CartSizes]([Id]), 
    CONSTRAINT [FK_Customer_ToCheckoutTypes] FOREIGN KEY ([CheckoutTypeId]) REFERENCES [CheckoutTypes]([Id])
)
