# To Do
- create register page 
- create access and refresh token on register
- create and store refresh tokens
-  

# Authentication 
- hash passwords with SHA 256
- 32 bit salts
- user table will store refresh token, when it was created, and when it expires
- client will store a version of refresh token using cookie with HTTP only attribute
- when client needs a new token we will check their refresh token against the one in the database

# Trades
- create method in db context for adding trade