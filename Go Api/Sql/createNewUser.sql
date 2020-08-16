USE WhoseTurnDb;


CREATE LOGIN "kevomacartney" WITH PASSWORD = 'root123';
create user "whose_turn_kevomacartney" for login "kevomacartney"

GRANT ALL PRIVILEGES ON DATABASE::WhoseTurnDb TO whose_turn_kevomacartney;
GO
