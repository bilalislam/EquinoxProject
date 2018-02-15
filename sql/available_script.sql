SELECT
  t.TableId,
  convert(CHAR(5), t.Time, 108) AS ScheduleTime,
  CASE WHEN ISNULL(r.TableId, 0) > 0
    THEN 0
  ELSE 1 END                    AS Status
FROM Schedules t
  LEFT JOIN Reservations r ON convert(CHAR(5), r.StartDate, 108) = convert(CHAR(5), t.Time, 108)
                              AND r.StartDate >= '2018-02-15 00:00'
                              AND r.EndDate <= '2018-02-16 00:00'
                              AND t.TableId = r.TableId

SELECT
  t.TableId,
  convert(CHAR(5), t.Time, 108) AS ScheduleTime,
  CASE WHEN ISNULL(r.TableId, 0) > 0
    THEN 0
  ELSE 1 END                    AS Status
FROM Schedules t
  LEFT JOIN Reservations r ON convert(CHAR(5), r.StartDate, 108) = convert(CHAR(5), t.Time, 108)
                              AND r.StartDate >= '2018-02-16 00:00'
                              AND r.EndDate <= '2018-02-17 00:00'
                              AND t.TableId = r.TableId
WHERE r.TableId IS NULL