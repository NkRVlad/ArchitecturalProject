 SELECT E.Id, E.Name, E.Surname, 
		            H.Id, H.Name, H.EmployeesId,
		            A.Id, A.Description, A.HiringHistoriesId
                FROM Employees as E
                JOIN HiringHistories as H ON H.EmployeesId = E.Id
                JOIN Achievements as A ON A.HiringHistoriesId = H.Id