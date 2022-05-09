use DBSikumkum
INSERT INTO Users(Username, Email, IsAdmin,NumUploads, Password, UserRating)
VALUES ('yotam', 'yotam@gmail.com', 1, 0, 'banana', 0);

INSERT INTO SUBJECTS(SubjectName) 
VALUES  ('מתמטיקה'),('אנגלית'), ('פיזיקה'), ('כימיה'), ('היסטוריה'),('תנ"ך'),('עברית'),('אזרחות'), ('ספרות'), ('מדעי המחשב')
INSERT INTO FileTypes(TypeName) 
VALUES ('סיכום'), ('מטלה'), ('תרגול')
INSERT INTO StudyYear(YearName)
VALUES  ('יסודי'), ('חטיבה'), ('תיכון'), ('אוניברסיטה')

--INSERT INTO FILETYPE (TypeName)
--VALUES ('Summary'), ('Practice'), ('Essay')

--INSERT INTO StudyYear (YearName)
--VALUES ('High School'), ('Middle School'), ('Elementary'), ('University')

--Scaffold:
--scaffold-dbcontext "Server=localhost\sqlexpress;Database=DBSikumkum;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models –force