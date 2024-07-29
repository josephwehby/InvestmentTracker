# To Do
## Frontend
- [ ] create register page
- [ ] store jwt
## Backend
- [ ] create access and refresh token when either access token is expired and refresh is not
- [x] create and store refresh tokens on login
  - the field exists in the database just have to store it on creation
- [x] add guid to claims filed in jwt  
- [x] add user id column in each of the tables 
- [x] change code to query by userid and whatever data the user is updating or needs
  - something like `SELECT * FROM table WHERE userid = 12345`
- [x] create method in db context for adding trade
- [x] update code to pass userid to necessary services so it can query on that value
## Cron Job
- [x] update cron job to account for the new userid field
## Deployment
- [ ] deploy to raspberry pi or cheap hosting provider
- [ ] deploy app using Docker
- [ ] deploy inside internal network
- [ ] VPN to allow for internal network access

# Authentication Notes
- hash passwords with SHA 256
- 32 bit salts
- user table will store refresh token, when it was created, and when it expires
- client will store a version of refresh token using cookie with HTTP only attribute
- when client needs a new token we will check their refresh token against the one in the database
  - if the refresh token is expired then they need to login again
  - if not then issue new jwt and refresh token for the user

# Database Notes
- to truncate the table and set the auto id back to 1 user `TRUNCATE TABLE your_table_name RESTART IDENTITY;`