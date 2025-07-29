# Stored Procedure: GetCreditLimit

Stored procedure used to determine the **maximum credit limit** available for a customer based on the **monthly income**.

## 📄 Definition

```sql
CREATE PROCEDURE GetCreditLimit
    @MonthlyIncome DECIMAL(10,2),
    @MaxCreditLimit DECIMAL(10,2) OUTPUT
AS
BEGIN
    IF @MonthlyIncome <= 1000
        SET @MaxCreditLimit = 1000
    ELSE IF @MonthlyIncome <= 2000
        SET @MaxCreditLimit = 2000
    ELSE
        SET @MaxCreditLimit = 5000
END
