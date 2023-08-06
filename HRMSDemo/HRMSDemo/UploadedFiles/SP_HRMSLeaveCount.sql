USE [HRMS]
GO
/****** Object:  StoredProcedure [dbo].[USP_Sps_EmployeeLeaveByFromDateToDate]    Script Date: 20/06/2022 10:38:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Exec USP_Sps_EmployeeLeaveByFromDateToDate '2022-05-07', '2022-05-07','1,2,3,4,5'
ALTER PROCEDURE [dbo].[USP_Sps_EmployeeLeaveByFromDateToDate]
@Id int,
@FromDate Date,
@ToDate Date,
@Status varchar(20)
AS
BEGIN
	IF @Status = ''
		SET @Status = NULL

	IF @Status IS NOT NULL
		BEGIN
			Select DATEDIFF(DAY,El.FromDate,El.ToDate)+1 as Day2,
				CASE When El.IsHalfDay = 1 Then CAST((DATEDIFF(DAY,El.FromDate,El.ToDate)+1) as float)/ 2 ELSE DATEDIFF(DAY,El.FromDate,El.ToDate)+1 END Day,
				EL.ID,EL.LeaveType as LeaveType,
				CASE WHEN EL.LeaveType = 1 THEN 'Casual Leave' WHEN EL.LeaveType = 2 THEN 'LWP' END as LeaveTypeName,
				EL.EmployeeFK,E.FirstName + ' ' + E.LastName FirstName, EL.FromDate,EL.ToDate,
				EL.IsHalfDay,CASE WHEN EL.IsHalfDay = 0 THEN 'Full Day' ELSE 'Half Day' END as IsHalfDayName,
				EL.HalfDayType,CASE WHEN EL.IsHalfDay = 1 THEN 'Morning' WHEN EL.IsHalfDay = 2 THEN 'Afternoon' ELSE '' END as HalfDayTypeName,
				EL.Reason,S.Status 
			From EmployeeLeave(NOLOCK) EL
				Inner Join Employee(NOLOCK) E ON EL.EmployeeFK = E.Id
				Inner Join Status(NOLOCK) S ON EL.StatusFK = S.Id
				Inner Join AssignLeave(NOLOCK) AL ON AL.EmployeeFK = E.Id
			Where EL.EmployeeFK = @Id AND (@FromDate BetWeen FromDate and ToDate
				Or @ToDate Between FromDate and ToDate OR 
				FromDate >= @fromDate And ToDate <= @ToDate)
				AND EL.StatusFK IN (
						SELECT value
						FROM STRING_SPLIT(@Status, ',')
						WHERE RTRIM(value) <> ''
						)
		END
	ELSE
		BEGIN
			Select DATEDIFF(DAY,El.FromDate,El.ToDate)+1 as Day2, 
				CASE When El.IsHalfDay = 1 Then CAST((DATEDIFF(DAY,El.FromDate,El.ToDate)+1) as float)/ 2 ELSE DATEDIFF(DAY,El.FromDate,El.ToDate)+1 END Day,
				EL.ID,EL.LeaveType AS LeaveType,
				CASE WHEN EL.LeaveType = 1 THEN 'Casual Leave' WHEN EL.LeaveType = 2 THEN 'LWP' END as LeaveTypeName,
				EL.EmployeeFK,E.FirstName + ' ' + E.LastName FirstName,EL.FromDate,EL.ToDate,
				EL.IsHalfDay,CASE WHEN EL.IsHalfDay = 0 THEN 'Full Day' ELSE 'Half Day' END as IsHalfDayName,
				EL.HalfDayType,CASE WHEN EL.IsHalfDay = 1 THEN 'Morning' WHEN EL.IsHalfDay = 2 THEN 'Afternoon' ELSE '' END as HalfDayTypeName,
				EL.Reason,S.Status 
			From EmployeeLeave(NOLOCK) EL
				Inner Join Employee(NOLOCK) E ON EL.EmployeeFK = E.Id
				Inner Join Status(NOLOCK) S ON EL.StatusFK = S.Id
			Where EL.EmployeeFK = @Id AND (@FromDate BetWeen FromDate and ToDate
				Or @ToDate Between FromDate and ToDate OR 
				FromDate >= @fromDate And ToDate <= @ToDate)				
		END
END




GO
/****** Object:  StoredProcedure [dbo].[spS_LeaveCount]    Script Date: 20/06/2022 10:37:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Exec spS_LeaveCount 1
ALTER PROCEDURE [dbo].[spS_LeaveCount] 
(
	@Id int
)
AS
BEGIN
	select AL.CasualLeave - y.CLLeave casualLeaveBalance, Al.LWP-y.LWPLeave lwpLeaveBalance from(
		select Sum(CLLeave) CLLeave,Sum(LWPLeave) LWPLeave,EmployeeFK from(
			Select 
				Case When EL.LeaveType = 2 Then 0 else case when EL.IsHalfDay = 1 and EL.StatusFK Not IN (1,2) 
				Then cast((DATEDIFF(day,EL.FromDate,EL.ToDate)+1) as float)/2 else DATEDIFF(day,EL.FromDate,El.ToDate)+1 End 
				End as CLLeave,
				Case When EL.LeaveType = 1 Then 0 else case when EL.IsHalfDay = 1 and EL.StatusFK Not IN (1,2)
				Then cast((DATEDIFF(day,EL.FromDate,EL.ToDate)+1) as float)/2 else DATEDIFF(day,EL.FromDate,El.ToDate)+1 End 
				End as LWPLeave,EmployeeFK
			From EmployeeLeave EL
			Where EmployeeFK = @Id and EL.StatusFK Not In(1,2))x
		Group By EmployeeFK) Y
	Inner Join AssignLeave AL on Y.EmployeeFK = AL.EmployeeFK
END




GO
/****** Object:  StoredProcedure [dbo].[spS_LeaveBalance]    Script Date: 20/06/2022 10:37:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Exec spS_LeaveBalance 1,'2022/05/01','2022/05/10'
ALTER PROCEDURE [dbo].[spS_LeaveBalance]
(
	@LeaveType int,
	@FromDate DateTime,
	@ToDate DateTime
) 
AS
BEGIN
	WITH CTE_DatesTable
	AS
	(
		SELECT CAST(@FromDate as datetime) AS [date]
		UNION ALL
		SELECT DATEADD(day, 1, [date])
		FROM CTE_DatesTable
		WHERE DATEADD(day, 1, [date]) <= @ToDate
	)

	Select c.date LeaveDate,
		Convert(varchar(11), DATEADD(yy, DATEDIFF(yy, 0, GETDATE()), 0),103) + ' - ' + Convert(varchar(11), DATEADD(yy, DATEDIFF(yy, 0, GETDATE())+1, -1),103) AS LeavePeriod,
		Case When @LeaveType = 1 Then AL.CasualLeave - y.CLLeave Else Al.LWP-y.LWPLeave End IntialBalance,
		AL.CasualLeave - y.CLLeave casualLeaveBalance, Al.LWP-y.LWPLeave lwpLeaveBalance
		, (Case When @LeaveType = 1 Then AL.CasualLeave - y.CLLeave Else Al.LWP-y.LWPLeave End) 
			- ROW_NUMBER() OVER (ORDER BY C.date ASC) as AvailableBalance
	from(
		select Sum(CLLeave) CLLeave,Sum(LWPLeave) LWPLeave,EmployeeFK from(
			Select 
				Case When EL.LeaveType = 2 Then 0 else case when EL.IsHalfDay = 1 and EL.StatusFK Not IN (1,2) 
				Then cast((DATEDIFF(day,EL.FromDate,EL.ToDate)+1) as float)/2 else DATEDIFF(day,EL.FromDate,El.ToDate)+1 End 
				End as CLLeave,
				Case When EL.LeaveType = 1 Then 0 else case when EL.IsHalfDay = 1 and EL.StatusFK Not IN (1,2)
				Then cast((DATEDIFF(day,EL.FromDate,EL.ToDate)+1) as float)/2 else DATEDIFF(day,EL.FromDate,El.ToDate)+1 End 
				End as LWPLeave,EmployeeFK
			From EmployeeLeave EL
			Where EL.StatusFK Not In(1,2) )x --EmployeeFK = 1
		Group By EmployeeFK) Y
	Inner Join AssignLeave AL on Y.EmployeeFK = AL.EmployeeFK, CTE_DatesTable C	
	OPTION (MAXRECURSION 0);
END