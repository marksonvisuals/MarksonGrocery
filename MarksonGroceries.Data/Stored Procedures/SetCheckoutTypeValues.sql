CREATE PROCEDURE [dbo].[SetCheckoutTypeValues]

AS
INSERT INTO CheckoutTypes
VALUES ('Self-Checkout')

INSERT INTO CheckoutTypes
VALUES ('Cashier')

