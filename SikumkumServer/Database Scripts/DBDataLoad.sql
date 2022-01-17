use DBSikumkum
INSERT INTO Users(Username, Email, IsAdmin,NumUploads, Password, Rating)
VALUES ('yotam', 'yotam@gmail.com', 0, 0, 'banana', 0);
INSERT INTO SUBJECT(SubjectName) 
VALUES  ('מתמטיקה'),('אנגלית'), ('פיזיקה'), ('כימיה'), ('היסטוריה'),('תנ"ך'),('עברית'),('אזרחות')
INSERT INTO FileTypes(TypeName) 
VALUES ('סיכום'), ('מטלה'), ('תרגול')
INSERT INTO StudyYear(YearName)
VALUES ('תיכון'), ('חטיבה'), ('יסודי'), ('אוניברסיטה')