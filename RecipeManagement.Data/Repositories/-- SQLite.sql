-- SQLite
select * from Users

INSERT INTO Users (UserId,UserName, Email, PasswordHash, Bio, StatusId, CreatedAt, UpdatedAt, LastModifiedUserId, CreatedBy)
VALUES (1,'JohnDoe', 'john.doe@example.com', 'hashedpassword123', 'This is a bio.', 1, '2025-02-12 14:30:00.000', 12, 12, 1);




UPDATE Users
SET 
    UpdatedAt = '2025-02-02 08:15:45'
WHERE UserId = 1;