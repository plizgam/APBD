CREATE PROCEDURE UpdateStudent
  @Semester int,
  @Studies varchar(100)
AS

DECLARE
@IdEnroll int,
@IdStudy int

SET @IdStudy = (SELECT IdStudy FROM Studies WHERE Name = @Studies) 

SET @IdEnroll = (SELECT IdEnrollment FROM Enrollment WHERE Semester = (@Semester + 1) AND IdStudy = @IdStudy)


IF (IsNull(@IdEnroll, 0) = 0)
BEGIN
	INSERT INTO Enrollment VALUES((@Semester+1), @IdStudy, GETDATE())
	SET @IdEnroll = (SELECT IdEnrollment FROM Enrollment WHERE Semester = (@Semester + 1) AND IdStudy = @IdStudy)
END

UPDATE Student 
SET IdEnrollment = @IdEnroll
WHERE IdEnrollment = (SELECT IdEnrollment FROM Enrollment WHERE Semester = (@Semester) AND IdStudy = @IdStudy)

SELECT * FROM Enrollment WHERE IdEnrollment = @IdEnroll


DROP PROCEDURE UpdateStudent;