SELECT * FROM Entry e LEFT JOIN FactorEntry fe ON e.Id = fe.EntryId LEFT JOIN Factor f ON f.Id = fe.FactorId WHERE e.Date BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE() ORDER BY e.Date; 
--WHERE e.Date BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE()
--WHERE e.Date BETWEEN DATEADD(YEAR, -1, GETDATE()) AND GETDATE()

