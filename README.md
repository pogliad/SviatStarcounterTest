# SviatStarcounterTest

Start page: http://127.0.0.1:8080/Sviat

Known issue:
Command Db.SQL<decimal>("SELECT AVG(h.Commission) FROM Home h WHERE h.Vendor = ?", this).First
fails when franchese contains such transactions: 75000, 75000, 75000, 65000, 65000, 60000. Expected average value 69166.666(6)
Exception:
System can not calculate AVG for such commissions: 75000, 75000, 75000, 65000, 65000, 60000
Expected value is 69166.666(6)
Aggregation fail with exception:
ScErrCLRDecToX6DecRangeError (SCERR4246): The CLR Decimal cannot be converted to a Starcounter X6 decimal without data loss. Range error.
Version: 2.3.1.6538.
Help page: https://github.com/Starcounter/Starcounter/wiki/SCERR4246.

at Starcounter.Internal.X6Decimal.ToRaw(Decimal value) in C:\TeamCity\BuildAgent\work\sc-5668\Level1\src\Starcounter.Internal\X6Decimal.cs:line 105
at Starcounter.Query.Execution.FilterKeyBuilder.PrecomputeBuffer(Nullable`1 value) in C:\TeamCity\BuildAgent\work\sc-5668\Level1\src\Starcounter\Query\Execution\Ranges\FilterKeyBuilder.cs:line 81
at Starcounter.Query.Execution.DecimalSetFunction.GetResult() in C:\TeamCity\BuildAgent\work\sc-5668\Level1\src\Starcounter\Query\Execution\SetFunctions\DecimalSetFunction.cs:line 99
at Starcounter.Query.Execution.Aggregation.CreateNewObject(Row compObject, Boolean produceOneResult) in C:\TeamCity\BuildAgent\work\sc-5668\Level1\src\Starcounter\Query\Execution\Enumerators\Aggregation.cs:line 252
at Starcounter.Query.Execution.Aggregation.MoveNext() in C:\TeamCity\BuildAgent\work\sc-5668\Level1\src\Starcounter\Query\Execution\Enumerators\Aggregation.cs:line 236
at Starcounter.QueryResultRows`1.get_First() in C:\TeamCity\BuildAgent\work\sc-5668\Level1\src\Starcounter\Query\SQL\SqlResultGeneric.cs:line 143
at Sviat.Database.Franchise.get_AvgCommission() in C:\Projects\test\SviatStarcounterTest\Sviat\Database\Franchise.cs:line 20