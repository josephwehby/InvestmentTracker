# InvestmentTracker (need new name as this one is atrocious)
## Basic Idea and Tools
- I own shares of stock in multiple brokerage accounts (3) so why not aggreate the data to one spot
    - I currently just use google sheets to manage all of them
- Goal of this project is to get better at ASP.net, React, and PostgreSQL and to solve a problem
    - might use a managed database if I can find a decent free one
    - also need to interface with some sort of api to get stock quotes
    - with Google Sheets they allow you to use google finance to pull stock quotes so I will need something similar
    - they dont need to be live but updated every 15min or so would be ideal
- This will only be accessible through my internal network so I most likely wont do any authentication
    - If I do any authentication I might just write it myself for fun/practice
- This will all be hosted on my RaspberryPi which I have had for 3 years and have yet to find a use for


### To Do
- get stock price api
- add mutual tls authentication 
- create table to store historic pnl of portfolio
- create cron job which will run at end of each day to calculate portfolio pnl which will be displayed in graph