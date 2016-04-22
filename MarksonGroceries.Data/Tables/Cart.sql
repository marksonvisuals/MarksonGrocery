CREATE TABLE [dbo].[Cart]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] INT NOT NULL, 
    [ProduceId] INT NULL, 
    CONSTRAINT [FK_Cart_ToCustomer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id]), 
    CONSTRAINT [FK_Cart_ToProduce] FOREIGN KEY ([ProduceId]) REFERENCES [Produce]([Id])
)
