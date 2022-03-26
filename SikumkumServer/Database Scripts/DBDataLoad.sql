use DBSikumkum
INSERT INTO Users(Username, Email, IsAdmin,NumUploads, Password, UserRating)
VALUES ('yotam', 'yotam@gmail.com', 1, 0, 'banana', 0);

INSERT INTO SUBJECTS(SubjectName) 
VALUES  ('מתמטיקה'),('אנגלית'), ('פיזיקה'), ('כימיה'), ('היסטוריה'),('תנ"ך'),('עברית'),('אזרחות')
INSERT INTO FileTypes(TypeName) 
VALUES ('סיכום'), ('מטלה'), ('תרגול')
INSERT INTO StudyYear(YearName)
VALUES ('תיכון'), ('חטיבה'), ('יסודי'), ('אוניברסיטה')

--INSERT INTO FILETYPE (TypeName)
--VALUES ('Summary'), ('Practice'), ('Essay')

--INSERT INTO StudyYear (YearName)
--VALUES ('High School'), ('Middle School'), ('Elementary'), ('University')