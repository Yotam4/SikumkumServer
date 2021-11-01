CREATE DATABASE DBSikumkum

CREATE TABLE "Users"(
    "UserID" INT NOT NULL Identity(1000,1),
    "Username" NVARCHAR(255) NOT NULL,
    "Email" NVARCHAR(255) NOT NULL,
    "IsAdmin" TINYINT NOT NULL,
    "NumUploads" INT NOT NULL,
    "Password" NVARCHAR(255) NOT NULL,
    "Rating" FLOAT NOT NULL
);
ALTER TABLE
    "Users" ADD CONSTRAINT "users_userid_primary" PRIMARY KEY("UserID");
CREATE UNIQUE INDEX "users_username_unique" ON
    "Users"("Username");
CREATE UNIQUE INDEX "users_email_unique" ON
    "Users"("Email");
CREATE TABLE "SikumFiles"(
    "FileID" INT NOT NULL Identity(25000,1),
    "Username" NVARCHAR(255) NOT NULL,
    "TypeID" INT NOT NULL,
    "YearID" INT NOT NULL,
    "Approved" TINYINT NOT NULL,
    "Headline" NVARCHAR(255) NOT NULL,
    "TextDesc" NVARCHAR(255) NOT NULL,
    "ChatBoxID" INT NOT NULL,
    "URL" NVARCHAR(255) NULL
);
ALTER TABLE
    "SikumFiles" ADD CONSTRAINT "sikumfiles_fileid_primary" PRIMARY KEY("FileID");
CREATE UNIQUE INDEX "sikumfiles_username_unique" ON
    "SikumFiles"("Username");
CREATE UNIQUE INDEX "sikumfiles_typeid_unique" ON
    "SikumFiles"("TypeID");
CREATE UNIQUE INDEX "sikumfiles_yearid_unique" ON
    "SikumFiles"("YearID");
CREATE TABLE "Subjects"(
    "SubjectID" INT NOT NULL Identity,
    "Subject" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Subjects" ADD CONSTRAINT "subjects_subjectid_primary" PRIMARY KEY("SubjectID");
CREATE TABLE "FileTypes"(
    "TypeID" INT NOT NULL Identity,
    "TypeName" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "FileTypes" ADD CONSTRAINT "filetypes_typeid_primary" PRIMARY KEY("TypeID");
CREATE TABLE "Chats"(
    "ChatBoxID" INT NOT NULL Identity,
    "ChatTitle" NVARCHAR(255) NOT NULL,
    "ChatDesc" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Chats" ADD CONSTRAINT "chats_chatboxid_primary" PRIMARY KEY("ChatBoxID");
CREATE TABLE "UserMessages"(
    "MessageID" INT NOT NULL Identity,
    "ChatBoxID" INT NOT NULL,
    "Username" NVARCHAR(255) NOT NULL,
    "TextMessage" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "UserMessages" ADD CONSTRAINT "usermessages_messageid_primary" PRIMARY KEY("MessageID");
CREATE UNIQUE INDEX "usermessages_chatboxid_unique" ON
    "UserMessages"("ChatBoxID");
CREATE UNIQUE INDEX "usermessages_username_unique" ON
    "UserMessages"("Username");
CREATE TABLE "StudyYear"(
    "YearID" INT NOT NULL,
    "YearName" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "StudyYear" ADD CONSTRAINT "studyyear_yearid_primary" PRIMARY KEY("YearID");
ALTER TABLE
    "SikumFiles" ADD CONSTRAINT "sikumfiles_chatboxid_foreign" FOREIGN KEY("ChatBoxID") REFERENCES "Chats"("ChatBoxID");
ALTER TABLE
    "SikumFiles" ADD CONSTRAINT "sikumfiles_typeid_foreign" FOREIGN KEY("TypeID") REFERENCES "FileTypes"("TypeID");
ALTER TABLE
    "UserMessages" ADD CONSTRAINT "usermessages_chatboxid_foreign" FOREIGN KEY("ChatBoxID") REFERENCES "Chats"("ChatBoxID");
ALTER TABLE
    "SikumFiles" ADD CONSTRAINT "sikumfiles_yearid_foreign" FOREIGN KEY("YearID") REFERENCES "StudyYear"("YearID");