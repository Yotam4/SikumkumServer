use DBSikumkum
CREATE TABLE "Users"(
    "UserID" INT NOT NULL Identity(1000,1),
    "Username" NVARCHAR(255) NOT NULL UNIQUE,
    "Email" NVARCHAR(255) NOT NULL UNIQUE,
    "IsAdmin" BIT NOT NULL,
    "NumUploads" INT NOT NULL,
    "Password" NVARCHAR(255) NOT NULL,
    "UserRating" FLOAT NOT NULL
);
ALTER TABLE
    "Users" ADD CONSTRAINT "users_userid_primary" PRIMARY KEY("UserID");
CREATE UNIQUE INDEX "users_username_unique" ON
    "Users"("Username");
CREATE UNIQUE INDEX "users_email_unique" ON
    "Users"("Email");
CREATE TABLE "SikumFiles"(
    "FileID" INT NOT NULL Identity(10000,1),
    "UserID" INT NOT NULL,
    "SubjectID" INT NOT NULL,
    "TypeID" INT NOT NULL,
    "YearID" INT NOT NULL,
    "Approved" BIT NOT NULL,
    "Headline" NVARCHAR(255) NOT NULL,
    "TextDesc" NVARCHAR(255) NOT NULL,
    "URL" NVARCHAR(255) NOT NULL,
	"Rating" FLOAT NOT NULL,
	"NumRated" INT NOT NULL
);
ALTER TABLE
    "SikumFiles" ADD CONSTRAINT "sikumfiles_fileid_primary" PRIMARY KEY("FileID");

CREATE TABLE "Subjects"(
    "SubjectID" INT NOT NULL Identity,
    "SubjectName" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Subjects" ADD CONSTRAINT "subjects_subjectid_primary" PRIMARY KEY("SubjectID");
CREATE TABLE "FileTypes"(
    "TypeID" INT NOT NULL Identity,
    "TypeName" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "FileTypes" ADD CONSTRAINT "filetypes_typeid_primary" PRIMARY KEY("TypeID");
CREATE TABLE "StudyYear"(
    "YearID" INT NOT NULL Identity,
    "YearName" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "StudyYear" ADD CONSTRAINT "studyyear_yearid_primary" PRIMARY KEY("YearID");

ALTER TABLE
    "SikumFiles" ADD CONSTRAINT "sikumfiles_userid_foreign" FOREIGN KEY("UserID") REFERENCES "Users"("UserID");
ALTER TABLE
    "SikumFiles" ADD CONSTRAINT "sikumfiles_subjectid_foreign" FOREIGN KEY("SubjectID") REFERENCES "Subjects"("SubjectID");
ALTER TABLE
    "SikumFiles" ADD CONSTRAINT "sikumfiles_typeid_foreign" FOREIGN KEY("TypeID") REFERENCES "FileTypes"("TypeID");
ALTER TABLE
    "SikumFiles" ADD CONSTRAINT "sikumfiles_yearid_foreign" FOREIGN KEY("YearID") REFERENCES "StudyYear"("YearID");