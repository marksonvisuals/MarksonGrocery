CREATE PROCEDURE [dbo].[SetCartSizeValues]

AS
INSERT INTO CartSizes
VALUES ('None')

INSERT INTO CartSizes
VALUES ('Basket')

INSERT INTO CartSizes
VALUES ('Small')

INSERT INTO CartSizes
VALUES ('Medium')

INSERT INTO CartSizes
VALUES ('Large')

INSERT INTO CartSizes
VALUES ('Jumbo')
